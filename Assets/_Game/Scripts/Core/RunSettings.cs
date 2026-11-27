using UnityEngine;

namespace Tower.Core
{
    [CreateAssetMenu(fileName = "RunSettings", menuName = "Tower/Run Settings")]
    public sealed class RunSettings : ScriptableObject
    {
        [SerializeField, Min(1)] private int _startingFloor = 1;

        public int StartingFloor => Mathf.Max(1, _startingFloor);

        private void OnValidate()
        {
            _startingFloor = Mathf.Max(1, _startingFloor);
        }
    }
}
