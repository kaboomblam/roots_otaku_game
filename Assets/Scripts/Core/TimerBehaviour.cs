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

        private const int MIN_TIME_FOR_COUNTDOWN = 3;

        private Coroutine _timerCoroutine;

        public bool Running { get => _isRunning; }

        public bool Complete { get => _isComplete; }

        public void CreateTimer(int time, bool isCountDown = false)
        {
            this._isCountdownTimer = isCountDown;
            this._time = time;
        }

        public string TimeString
        {
            get => GetTimeString();
        }

        public int TimeInteger
        {
            get => _time;
        }

        private string GetTimeString()
        {
            string countdownTimeStringFormat = _time.ToString();

            string elapsedTimeStringFormat = TimeSpan.FromSeconds(_time).ToString(@"mm\:ss\:ff");

            string timeFormatReturned = _isCountdownTimer ? countdownTimeStringFormat : elapsedTimeStringFormat;

            return timeFormatReturned;
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

        IEnumerator TimerCoroutine()
        {
            bool timeIsMinAllowed = _time < MIN_TIME_FOR_COUNTDOWN;

            bool inputTooLowForCountdown = _isCountdownTimer && timeIsMinAllowed;

            if (inputTooLowForCountdown)
            {
                Debug.Log($"Current timer input: {_time}, too low for a Countdown! Must be at least {MIN_TIME_FOR_COUNTDOWN}. Is Complete {_isComplete}");
            }
            else
            {
                bool timerNotComplete = _isRunning && !_isComplete;

                while (timerNotComplete)
                {
                    yield return new WaitForSeconds(1f);
                    if (_isCountdownTimer)
                    {
                        if (_isCountdownTimer) DecreaseTime();
                        else IncreaseTime();
                    }
                    else IncreaseTime();
                }
            }

            yield return null;
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
