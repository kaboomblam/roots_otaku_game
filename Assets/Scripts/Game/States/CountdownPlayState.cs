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
            timer.CreateTimer(gpm.countdownToStart, isCountDown: true);
        }


        internal override void UpdateState(GamePlayManager gpm)
        {
            bool timerHasNotStarted = !timer.Running && !timer.Complete;
            bool timerHasNotCompleted = !timer.Complete;

            if (timerHasNotStarted) timer.RunTimer();
            else if (timerHasNotCompleted)
            {
                gpm.countdownText.SetText(timer.TimeString);
            }
        }

        internal override void ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Countdown State...");
            timer.DestroyComponent();
        }

    }
}