using System.Collections;
using OtakuGameJam.Attributes;
using OtakuGameJam.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace OtakuGameJam
{
    public class GamePlayManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        [Space]
        public TMPro.TextMeshProUGUI DEBUG_StateText;
        [Header("Countdown")]
        public TMPro.TextMeshProUGUI countdownText;
        public TMPro.TextMeshProUGUI goText;
        public Button cancelButton;
        public GameObject[] completeCountdownHideElements;

        [Space]
        [Header("HUD")]
        public TMPro.TextMeshProUGUI hudLapTimeText;

        [Header("Music")]
        public AudioClip gameAudio;


        [Space]

        [Header("Debug")]
        [SerializeField]
        private bool _useGlobalSettings;
        [DisablePropertyControl("_useGlobalSettings", true)]
        [SerializeField]
        private PlayStateValues _beginningState = PlayStateValues.Countdown;

        [DisablePropertyControl("_useGlobalSettings", true)]
        [Range(3, 20)]
        public int countdownToStart = 3;

        [Header("Audio")]
        public AudioClip countdownSound;

        [Header("UIs")]
        public GameObject pauseUI;
        public GameObject gameOverUI;


        // Finite State Machine
        // --------------------
        PlayState CurrentState;
        PlayState CountdownState = new CountdownPlayState();
        PlayState PlayingState = new PlayingPlayState();
        PlayState PausedState = new PausedPlayState();
        PlayState GameOverState = new GameOverPlayState();

        void Start()
        {
            CurrentState = GetStateFromEnum(_beginningState);

            CurrentState.EnterState(this);

            // FadeAudioIn();
        }

        void Update()
        {
            CurrentState.UpdateState(this);
        }

        public void ChangeState(PlayStateValues value)
        {
            bool isStateReadyToExit = CurrentState.ExitState(this);

            PlayState nextState = GetStateFromEnum(value);

            if (isStateReadyToExit)
            {
                CurrentState = nextState;

                PlayStateValues currPlayStateEnum = _beginningState = GetEnumFromState(CurrentState);

                CurrentState.EnterState(this);
            }
        }

        private PlayState GetStateFromEnum(PlayStateValues value)
        {
            switch (value)
            {
                case PlayStateValues.Countdown:
                    return CountdownState;
                case PlayStateValues.Playing:
                    return PlayingState;
                case PlayStateValues.Paused:
                    return PausedState;
                case PlayStateValues.GameOver:
                    return GameOverState;
                default:
                    return null;
            }
        }

        public void GoToMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        public void UnpauseGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        }


        private PlayStateValues GetEnumFromState(PlayState state)
        {
            if (state == CountdownState) return PlayStateValues.Countdown;
            else if (state == PlayingState) return PlayStateValues.Playing;
            else if (state == PausedState) return PlayStateValues.Paused;
            else if (state == GameOverState) return PlayStateValues.GameOver;
            else return PlayStateValues.Countdown;
        }

        // IEnumerable FadeAudioIn()
        // {
        //     // float currentTime = 0;
        //     // float duration = 30000;
        //     AudioSource audioSource = GetComponent<AudioSource>();
        //     // float currentVolume = audioSource.volume;
        //     float volume = audioSource.volume = 0;
        //     while (currentTime < duration)
        //     {
        //         currentTime += Time.deltaTime;
        //         audioSource.volume = Mathf.Lerp(0, 1, currentTime / duration);
        //         yield return null;
        //     }
        //     yield break;
        // }

    }
}
