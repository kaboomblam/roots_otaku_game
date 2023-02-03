using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtakuGameJam.Constants;
using System;
using TMPro;

namespace OtakuGameJam
{
    public class GamePlayManager : MonoBehaviour
    {
        [Tooltip("Allows for GamePlay Manager to use Game wide Settings or Defaults")]
        [SerializeField]
        private bool _useGlobalSettings = false;

        [Space]

        [Header("Game Settings")]
        [Range(1, 5)]
        [SerializeField]
        private int _laps = 3;

        [HideInInspector]
        public int currentLap = 0;


        [HideInInspector]
        public float currentTime = 0f;

        [Header("Time Settings")]
        [Range(0, 20)]
        public int _countdownToStart = 3;

        [Header("Debug Settings")]
        [SerializeField]
        private GamePlayState _playState = GamePlayState.Countdown;

        [SerializeField]
        private TextMeshProUGUI _timeText;

        [SerializeField]
        private TextMeshProUGUI _lapText;

        public GamePlayState playState
        {
            get { return _playState; }
            private set
            {
                _playState = value;
                UpdateGamePlayState(_playState);
            }
        }

        private void Start()
        {
            if (_useGlobalSettings)
            {
                _laps = SettingsData.laps;

                _countdownToStart = SettingsData.countDownToStart;

                UpdateGamePlayState(GamePlayState.Countdown);
            }
            else
            {
                UpdateGamePlayState(_playState);
            }
        }

        private void Update()
        {
            UpdateElapsedTime();
            var t = TimeSpan.FromSeconds(currentTime);
            // Debug.Log($"Current Time: {t.Minutes:D2}:{t.Seconds:D2}:{t.Milliseconds:D2}");
            // Debug.Log($"Current Time: {currentTime:C2}");

            _timeText.SetText($"Time: {t.Minutes:D2}:{t.Seconds:D2}:{t.Milliseconds:D2}");
            _lapText.SetText($"Lap: {currentLap}/{_laps}");
        }

        #region GamePlayState

        public void UpdateGamePlayState(GamePlayState newState)
        {
            switch (newState)
            {
                case GamePlayState.Countdown:
                    StartCoroutine(CountdownToStartTimer());
                    break;
                case GamePlayState.Playing:
                    break;
                case GamePlayState.Paused:
                    break;
                case GamePlayState.GameOver:
                    break;
            }
        }

        #endregion


        #region Time
        IEnumerator CountdownToStartTimer()
        {
            while (_countdownToStart > 0)
            {
                yield return new WaitForSeconds(1f);
                _countdownToStart--;
                Debug.Log($"Countdown: {_countdownToStart}");
            }
        }

        void UpdateElapsedTime() => currentTime += Time.deltaTime;


        #endregion
    }
}
