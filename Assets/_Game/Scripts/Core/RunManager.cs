using System;
using UnityEngine;

namespace Tower.Core
{
    [DisallowMultipleComponent]
    public sealed class RunManager : MonoBehaviour
    {
        [SerializeField] private RunSettings _settings;

        private RunState _state = RunState.NotStarted;
        private int _currentFloor;

        public RunState State => _state;
        public int CurrentFloor => _currentFloor;

        // Raised after both State and CurrentFloor are ready to read.
        public event Action Changed;

        [ContextMenu("Start Run")]
        public void StartRun()
        {
            if (!Application.isPlaying || !isActiveAndEnabled || _state != RunState.NotStarted)
            {
                return;
            }

            BeginRun();
        }

        [ContextMenu("Advance Floor")]
        public void AdvanceFloor()
        {
            if (!Application.isPlaying || !isActiveAndEnabled || _state != RunState.Running)
            {
                return;
            }

            if (_currentFloor == int.MaxValue)
            {
                Debug.LogWarning("The floor counter has reached its numeric limit.", this);
                return;
            }

            _currentFloor++;
            Changed?.Invoke();
        }

        [ContextMenu("Die")]
        public void Die()
        {
            if (!Application.isPlaying || !isActiveAndEnabled || _state != RunState.Running)
            {
                return;
            }

            _state = RunState.RunOver;
            Changed?.Invoke();
        }

        [ContextMenu("Restart Run")]
        public void RestartRun()
        {
            if (!Application.isPlaying || !isActiveAndEnabled || _state != RunState.RunOver)
            {
                return;
            }

            BeginRun();
        }

        private void BeginRun()
        {
            if (_settings == null)
            {
                Debug.LogError("RunManager needs a RunSettings asset. Assign Settings in the Inspector.", this);
                return;
            }

            _currentFloor = _settings.StartingFloor;
            _state = RunState.Running;
            Changed?.Invoke();
        }
    }
}
