using UnityEngine;

namespace OtakuGameJam
{
    class CountdownPlayState : PlayState
    {
        TimerBehaviour timer;
        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Countdown State...");

            timer = gpm.gameObject.AddComponent<TimerBehaviour>();
            timer.CreateTimer(gpm.countdownToStart, isCountingDown: true);
        }


        internal override void UpdateState(GamePlayManager gpm)
        {
            gpm.DEBUG_StateText.SetText("Countdown...");
        }

        internal override void ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Countdown State...");
            timer.DestroyComponent();
        }

    }
}