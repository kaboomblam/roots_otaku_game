using UnityEngine;

namespace OtakuGameJam
{
    class CountdownPlayState : PlayState
    {
        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Countdown State...");

            // gpm.ChangeState(Constants.PlayStateValues.Playing);
        }

        internal override void UpdateState(GamePlayManager gpm)
        {
            gpm.DEBUG_StateText.SetText("Countdown...");
        }

        internal override void ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Countdown State...");
        }
    }
}