using OtakuGameJam.Attributes;
using OtakuGameJam.Constants;
using UnityEngine;

namespace OtakuGameJam
{
    public class GamePlayManager : MonoBehaviour
    {

        [Header("Test & Debug")]
        [SerializeField]
        public TMPro.TextMeshProUGUI DEBUG_StateText;
        [SerializeField]
        private bool _useGlobalSettings;

        [DisableProperty("_useGlobalSettings", true)]
        [SerializeField]
        private PlayStateValues _playState = PlayStateValues.Countdown;

        [DisableProperty("_useGlobalSettings", true)]
        [Range(0, 20)]
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
            var stateHasBeenChangedManually = GetStateFromEnum(_playState) != currentState;
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
