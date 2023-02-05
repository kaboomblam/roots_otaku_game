using UnityEngine;

namespace OtakuGameJam
{
    public class PlayingPlayState : PlayState
    {
        private TimerBehaviour _lapTimer;
        private bool _ensureTimerInitialized;

        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Playing State...");

            _lapTimer = gpm.gameObject.AddComponent<TimerBehaviour>();

            _lapTimer.CreateTimer(0, false);

            _lapTimer.RunTimer();
        }

        internal override void UpdateState(GamePlayManager gpm)
        {
            if (_lapTimer.Complete)
            {
                gpm.ChangeState(Constants.PlayStateValues.Paused);
            }
            else if (_lapTimer.Running)
            {
                gpm.hudLapTimeText.SetText(_lapTimer.TimeString);
            }

            // check paused

            bool paused = Input.GetKey(KeyCode.Escape);

            if (paused)
            {
                gpm.ChangeState(Constants.PlayStateValues.Paused);
            }
        }

        internal override bool ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Playing State...");

            return true;
        }
    }
}