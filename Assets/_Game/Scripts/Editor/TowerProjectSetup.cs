using System;
using System.IO;
using TMPro;
using Tower.Core;
using Tower.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Tower.EditorTools
{
    public static class TowerProjectSetup
    {
        private const string _scenePath = "Assets/_Game/Scenes/RunLoopTest.unity";
        private const string _settingsPath = "Assets/_Game/Data/Core/DefaultRunSettings.asset";
        private const string _fontPath = "Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset";
        private static bool _importingTextResources;

        [MenuItem("Tools/Tower/Import TMP Essential Resources")]
        public static void ImportTextResources()
        {
            if (AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(_fontPath) != null)
            {
                Debug.Log("TOWER_TMP_READY");
                if (Application.isBatchMode)
                {
                    EditorApplication.Exit(0);
                }
                return;
            }

            if (_importingTextResources)
            {
                return;
            }

            PackageInfo package = PackageInfo.FindForAssembly(typeof(TMP_Text).Assembly);
            if (package == null)
            {
                throw new InvalidOperationException("Install Unity UI before importing TMP Essential Resources.");
            }

            string[] archives = Directory.GetFiles(package.resolvedPath, "TMP Essential Resources.unitypackage", SearchOption.AllDirectories);
            if (archives.Length != 1)
            {
                throw new InvalidOperationException("Could not find the TMP Essential Resources archive in Unity UI.");
            }

            _importingTextResources = true;
            AssetDatabase.importPackageCompleted += OnTextResourcesImported;
            AssetDatabase.importPackageFailed += OnTextResourcesFailed;
            AssetDatabase.importPackageCancelled += OnTextResourcesCancelled;
            try
            {
                AssetDatabase.ImportPackage(archives[0], false);
            }
            catch
            {
                ClearImportCallbacks();
                throw;
            }
        }

        private static void OnTextResourcesImported(string packageName)
        {
            ClearImportCallbacks();
            EditorApplication.delayCall += FinishTextResourcesImport;
        }

        private static void FinishTextResourcesImport()
        {
            bool ready = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(_fontPath) != null;
            if (ready)
            {
                AssetDatabase.SaveAssets();
                Debug.Log("TOWER_TMP_READY");
            }
            else
            {
                Debug.LogError("TMP import finished without the required LiberationSans SDF font.");
            }

            if (Application.isBatchMode)
            {
                EditorApplication.Exit(ready ? 0 : 1);
            }
        }

        private static void OnTextResourcesFailed(string packageName, string error)
        {
            ClearImportCallbacks();
            Debug.LogError("TMP Essential Resources import failed: " + error);
            if (Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        private static void OnTextResourcesCancelled(string packageName)
        {
            OnTextResourcesFailed(packageName, "Import cancelled.");
        }

        private static void ClearImportCallbacks()
        {
            _importingTextResources = false;
            AssetDatabase.importPackageCompleted -= OnTextResourcesImported;
            AssetDatabase.importPackageFailed -= OnTextResourcesFailed;
            AssetDatabase.importPackageCancelled -= OnTextResourcesCancelled;
        }

        [MenuItem("Tools/Tower/Create Run Loop Test Scene")]
        public static void CreateTestScene()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                throw new InvalidOperationException("Stop Play Mode before creating the test scene.");
            }

            if (!Application.isBatchMode)
            {
                if (File.Exists(_scenePath) && !EditorUtility.DisplayDialog("Recreate test scene?",
                    "This replaces RunLoopTest with the original placeholder scene. Other scenes and run settings are preserved.",
                    "Recreate", "Cancel"))
                {
                    return;
                }

                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    return;
                }
            }

            string[] folders = { "Scripts/Core", "Scripts/Combat", "Scripts/Floors", "Scripts/Loot", "Scripts/UI", "Scripts/Editor", "Prefabs", "Data/Core", "Art", "Audio", "Scenes" };
            foreach (string folder in folders)
            {
                string path = "Assets/_Game/" + folder;
                Directory.CreateDirectory(path);
                if (Directory.GetFileSystemEntries(path).Length == 0)
                {
                    File.WriteAllText(Path.Combine(path, ".gitkeep"), string.Empty);
                }
            }

            AssetDatabase.Refresh();

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            RunSettings settings = AssetDatabase.LoadAssetAtPath<RunSettings>(_settingsPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<RunSettings>();
                AssetDatabase.CreateAsset(settings, _settingsPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(_settingsPath, ImportAssetOptions.ForceSynchronousImport);
            settings = AssetDatabase.LoadAssetAtPath<RunSettings>(_settingsPath);
            if (settings == null)
            {
                throw new InvalidOperationException("Could not load the saved run settings asset.");
            }

            TMP_FontAsset font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(_fontPath);
            if (font == null)
            {
                throw new InvalidOperationException("Import TMP Essential Resources before creating the test scene.");
            }

            GameObject cameraObject = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
            cameraObject.tag = "MainCamera";
            Camera camera = cameraObject.GetComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.035f, 0.05f, 0.075f);
            camera.transform.position = new Vector3(0f, 8f, -8f);
            camera.transform.rotation = Quaternion.Euler(45f, 0f, 0f);

            GameObject lightObject = new GameObject("Directional Light", typeof(Light));
            Light light = lightObject.GetComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1f;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            RunManager manager = new GameObject("RunManager").AddComponent<RunManager>();
            Assign(manager, "_settings", settings);

            GameObject canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            Canvas canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            GameObject startPanel = CreatePanel("StartPanel", canvasObject.transform);
            GameObject runningPanel = CreatePanel("RunningPanel", canvasObject.transform);
            GameObject overPanel = CreatePanel("RunOverPanel", canvasObject.transform);
            CreateText("Title", startPanel.transform, font, "Tower Run", 110f);
            Button startButton = CreateButton("StartButton", startPanel.transform, font, "Start Run", 0f);
            TMP_Text floorText = CreateText("FloorText", runningPanel.transform, font, "Floor 1", 130f);
            Button nextButton = CreateButton("NextFloorButton", runningPanel.transform, font, "Next Floor", 20f);
            Button dieButton = CreateButton("DieButton", runningPanel.transform, font, "Die (Test)", -70f);
            CreateText("Title", overPanel.transform, font, "Run Over", 150f);
            TMP_Text reachedText = CreateText("FloorReachedText", overPanel.transform, font, "Floor reached", 50f);
            Button restartButton = CreateButton("RestartButton", overPanel.transform, font, "Restart", -60f);

            RunUI ui = canvasObject.AddComponent<RunUI>();
            Assign(ui, "_runManager", manager);
            Assign(ui, "_startPanel", startPanel);
            Assign(ui, "_runningPanel", runningPanel);
            Assign(ui, "_runOverPanel", overPanel);
            Assign(ui, "_floorText", floorText);
            Assign(ui, "_floorReachedText", reachedText);
            Assign(ui, "_startButton", startButton);
            Assign(ui, "_nextFloorButton", nextButton);
            Assign(ui, "_dieButton", dieButton);
            Assign(ui, "_restartButton", restartButton);
            runningPanel.SetActive(false);
            overPanel.SetActive(false);

            GameObject eventObject = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventObject.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();

            if (!EditorSceneManager.SaveScene(scene, _scenePath))
            {
                throw new InvalidOperationException("Could not save RunLoopTest.");
            }

            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(_scenePath, true) };
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("TOWER_SCENE_READY: " + _scenePath + " | Editor " + Application.unityVersion);
        }

        private static GameObject CreatePanel(string name, Transform parent)
        {
            GameObject panel = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            RectTransform rect = panel.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            panel.GetComponent<Image>().color = new Color32(21, 28, 37, 255);
            return panel;
        }

        private static TMP_Text CreateText(string name, Transform parent, TMP_FontAsset font, string label, float y)
        {
            GameObject textObject = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            SetRect(textObject.GetComponent<RectTransform>(), parent, y, 700f, 80f);
            TMP_Text text = textObject.GetComponent<TMP_Text>();
            text.font = font;
            text.text = label;
            text.fontSize = 36f;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;
            text.raycastTarget = false;
            return text;
        }

        private static Button CreateButton(string name, Transform parent, TMP_FontAsset font, string label, float y)
        {
            GameObject buttonObject = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            SetRect(buttonObject.GetComponent<RectTransform>(), parent, y, 320f, 64f);
            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color32(226, 232, 240, 255);
            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;
            TMP_Text text = CreateText("Label", buttonObject.transform, font, label, 0f);
            text.fontSize = 28f;
            text.color = new Color32(21, 28, 37, 255);
            RectTransform rect = text.rectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return button;
        }

        private static void SetRect(RectTransform rect, Transform parent, float y, float width, float height)
        {
            rect.SetParent(parent, false);
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0f, y);
            rect.sizeDelta = new Vector2(width, height);
        }

        private static void Assign(UnityEngine.Object target, string property, UnityEngine.Object value)
        {
            if (value == null)
            {
                throw new InvalidOperationException("Cannot assign a missing reference: " + property);
            }

            SerializedObject serialized = new SerializedObject(target);
            SerializedProperty field = serialized.FindProperty(property);
            if (field == null)
            {
                throw new InvalidOperationException("Missing serialized field: " + property);
            }

            field.objectReferenceValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
