using UnityEngine;

namespace OtakuGameJam
{
    public class PlayingPlayState : PlayState
    {
        private TimerBehaviour _lapTimer;

        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Playing State...");
            _lapTimer = gpm.gameObject.AddComponent<TimerBehaviour>();
            _lapTimer.CreateTimer(10, true);
            _lapTimer.RunTimer();

        }

        internal override void UpdateState(GamePlayManager gpm)
        {
            if (_lapTimer.Complete) gpm.ChangeState(Constants.PlayStateValues.GameOver);
        }

        internal override bool ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Playing State...");

            return true;
        }
    }
}