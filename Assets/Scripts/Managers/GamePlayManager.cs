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

        [Header("Debug")]
        [SerializeField]
        private bool _useGlobalSettings;
        [DisablePropertyControl("_useGlobalSettings", true)]
        [SerializeField]
        private PlayStateValues _playState = PlayStateValues.Countdown;

        [DisablePropertyControl("_useGlobalSettings", true)]
        [Range(3, 20)]
        public int countdownToStart = 3;

        // Finite State Machine
        // --------------------
        PlayState currentState;
        PlayState CountdownState = new CountdownPlayState();
        PlayState PlayingState = new PlayingPlayState();
        PlayState PausedState = new PausedPlayState();
        PlayState GameOverState = new GameOverPlayState();

        void Start()
        {
            currentState = GetStateFromEnum(_playState);

            currentState.EnterState(this);
        }

        void Update()
        {
            var currentPlayStateValue = GetStateFromEnum(_playState);
            var stateHasBeenChangedManually = currentPlayStateValue != currentState;

            if (stateHasBeenChangedManually) ChangeState(_playState);

            // ---

            currentState.UpdateState(this);
        }

        public void ChangeState(PlayStateValues value)
        {
            PlayState newState = GetStateFromEnum(value);

            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
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
    }
}
