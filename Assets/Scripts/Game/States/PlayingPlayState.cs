using UnityEngine;

namespace OtakuGameJam
{
    public class PlayingPlayState : PlayState
    {
        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Playing State...");
        }

        internal override void UpdateState(GamePlayManager gpm)
        {
            gpm.DEBUG_StateText.SetText("Playing State...");
        }

        internal override void ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Playing State...");

        }
    }
}