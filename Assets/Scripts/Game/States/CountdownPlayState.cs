using System;
using UnityEngine;

namespace OtakuGameJam
{
    class CountdownPlayState : PlayState
    {
        private TimerBehaviour _timer;

        private TMPro.TextMeshProUGUI _countdownText;

        private GameObject[] _completeCountdownHideElements;

        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Countdown State...");

            _timer = gpm.gameObject.AddComponent<TimerBehaviour>();

            _timer.CreateTimer(gpm.countdownToStart, isCountDown: true);

            _countdownText = gpm.countdownText;
            _completeCountdownHideElements = gpm.completeCountdownHideElements;
        }


        internal override void UpdateState(GamePlayManager gpm)
        {
            bool timerHasNotStarted = !_timer.Running && !_timer.Complete;
            bool timerHasNotCompleted = !_timer.Complete;
            bool timerCompleted = _timer.Complete;

            if (timerHasNotStarted) _timer.RunTimer();
            else if (timerHasNotCompleted)
            {
                _countdownText.SetText(_timer.TimeString);
            }
            else if (timerCompleted)
            {
                foreach (GameObject elem in _completeCountdownHideElements)
                {
                    elem.SetActive(false);
                }
            }
        }

        internal override void ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Countdown State...");
            _timer.DestroyComponent();
        }

    }
}