using System;
using System.Collections;
using System.Collections.Generic;
using OtakuGameJam.Attributes;
using UnityEngine;

namespace OtakuGameJam
{
    public class TimerBehaviour : MonoBehaviour
    {
        [SerializeField]
        [DisableProperty]
        private int _time = 0;
        private bool _isCountdownTimer = false;
        private bool _isRunning = false;
        private bool _isComplete = false;

        public string TimeString
        {
            get => GetTimeString();
        }

        public int TimeInteger
        {
            get => _time;
        }

        private Coroutine _timerCoroutine;

        public bool Running { get => _isRunning; }

        public bool Complete { get => _isComplete; }

        public void CreateTimer(int time, bool isCountDown = false)
        {
            this._isCountdownTimer = isCountDown;
            this._time = time;
        }

        private string GetTimeString()
        {
            string countdownTimeString = _time.ToString();
            string elapsedTimeString = TimeSpan.FromSeconds(_time).ToString(@"mm\:ss\:ff");

            string timeString = _isCountdownTimer ? countdownTimeString : elapsedTimeString;

            return timeString;
        }

        IEnumerator TimerCoroutine()
        {
            while (_isRunning && !_isComplete)
            {
                yield return new WaitForSeconds(1f);
                if (_isCountdownTimer) DecreaseTime();
                else IncreaseTime();
            }

        }

        void DecreaseTime()
        {
            if (_time > 0) _time--;
            else
            {
                _isRunning = false;
                _isComplete = true;
            }
        }

        void IncreaseTime() => _time++;

        public void RunTimer()
        {
            _isRunning = true;
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }

        public void StopTimer()
        {
            _isRunning = false;
            StopCoroutine(_timerCoroutine);
        }

        public void DestroyComponent()
        {
            Destroy(this);
        }

        private void OnDestroy()
        {
            Debug.Log($"Destroying timer...");
        }
    }
}
