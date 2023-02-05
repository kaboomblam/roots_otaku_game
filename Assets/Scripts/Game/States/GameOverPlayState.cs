using UnityEngine;

namespace OtakuGameJam
{
    class GameOverPlayState : PlayState
    {
        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered GameOver State...");

        }

        internal override void UpdateState(GamePlayManager gpm)
        {
            gpm.DEBUG_StateText.SetText("Game Over...");

        }

        internal override void ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited GameOver State...");
        }
    }
}