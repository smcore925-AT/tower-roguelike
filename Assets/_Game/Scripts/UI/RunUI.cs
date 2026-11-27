using TMPro;
using Tower.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.UI
{
    [DisallowMultipleComponent]
    public sealed class RunUI : MonoBehaviour
    {
        [Header("Run")]
        [SerializeField] private RunManager _runManager;

        [Header("Panels")]
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _runningPanel;
        [SerializeField] private GameObject _runOverPanel;

        [Header("Text")]
        [SerializeField] private TMP_Text _floorText;
        [SerializeField] private TMP_Text _floorReachedText;
        [SerializeField] private string _floorLabel = "Floor";
        [SerializeField] private string _floorReachedLabel = "Floor reached";

        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _nextFloorButton;
        [SerializeField] private Button _dieButton;
        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            if (!HasRequiredReferences())
            {
                Debug.LogError("RunUI needs its manager, three panels, two text fields, and four buttons assigned.", this);
                enabled = false;
                return;
            }

            _runManager.Changed += Refresh;
            _startButton.onClick.AddListener(StartRun);
            _nextFloorButton.onClick.AddListener(AdvanceFloor);
            _dieButton.onClick.AddListener(Die);
            _restartButton.onClick.AddListener(RestartRun);
            Refresh();
        }

        private void OnDisable()
        {
            if (_runManager != null)
            {
                _runManager.Changed -= Refresh;
            }

            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(StartRun);
            }

            if (_nextFloorButton != null)
            {
                _nextFloorButton.onClick.RemoveListener(AdvanceFloor);
            }

            if (_dieButton != null)
            {
                _dieButton.onClick.RemoveListener(Die);
            }

            if (_restartButton != null)
            {
                _restartButton.onClick.RemoveListener(RestartRun);
            }
        }

        private bool HasRequiredReferences()
        {
            return _runManager != null
                && _startPanel != null
                && _runningPanel != null
                && _runOverPanel != null
                && _floorText != null
                && _floorReachedText != null
                && _startButton != null
                && _nextFloorButton != null
                && _dieButton != null
                && _restartButton != null;
        }

        private void Refresh()
        {
            bool notStarted = _runManager.State == RunState.NotStarted;
            bool running = _runManager.State == RunState.Running;
            bool runOver = _runManager.State == RunState.RunOver;

            _floorText.text = $"{_floorLabel} {_runManager.CurrentFloor}";
            _floorReachedText.text = $"{_floorReachedLabel} {_runManager.CurrentFloor}";

            _startButton.interactable = notStarted;
            _nextFloorButton.interactable = running;
            _dieButton.interactable = running;
            _restartButton.interactable = runOver;

            _startPanel.SetActive(notStarted);
            _runningPanel.SetActive(running);
            _runOverPanel.SetActive(runOver);
        }

        private void StartRun()
        {
            _runManager.StartRun();
        }

        private void AdvanceFloor()
        {
            _runManager.AdvanceFloor();
        }

        private void Die()
        {
            _runManager.Die();
        }

        private void RestartRun()
        {
            _runManager.RestartRun();
        }
    }
}
