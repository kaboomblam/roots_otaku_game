using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtakuGameJam.Constants;

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
        public float currentTime = 0f;

        [Header("Time Settings")]
        [Range(0, 20)]
        public int _countdownToStart = 3;

        [Header("Debug Settings")]
        [SerializeField]
        private GamePlayState _playState = GamePlayState.Countdown;

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

        #region GamePlayState

        public void UpdateGamePlayState(GamePlayState newState)
        {
            switch (newState)
            {
                case GamePlayState.Countdown:
                    StartCoroutine(countdownToStartTimer());
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


        IEnumerator countdownToStartTimer()
        {
            while (_countdownToStart > 0)
            {
                yield return new WaitForSeconds(1f);
                _countdownToStart--;
                Debug.Log($"Countdown: {_countdownToStart}");
            }
        }
    }
}
