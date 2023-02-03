using UnityEngine;

namespace OtakuGameJam
{
    class PausedPlayState : PlayState
    {
        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Paused State...");
        }

        internal override void UpdateState(GamePlayManager gpm)
        {
            gpm.DEBUG_StateText.SetText("Paused State...");
        }

        internal override bool ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Paused State !!!!...");
            return true;
        }
    }
}