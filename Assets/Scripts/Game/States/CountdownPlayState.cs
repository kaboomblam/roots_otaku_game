using System;
using UnityEngine;
using UnityEngine.UI;

namespace OtakuGameJam
{
    class CountdownPlayState : PlayState
    {
        private TimerBehaviour _timer;

        private int _countdownTime;

        private TMPro.TextMeshProUGUI _countdownText;
        private TMPro.TextMeshProUGUI _goText;
        private Button _cancelButton;

        private GameObject[] _completeCountdownHideElements;

        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Countdown State...");

            _timer = gpm.gameObject.AddComponent<TimerBehaviour>();
            _countdownTime = gpm.countdownToStart;

            _timer.CreateTimer(_countdownTime, isCountDown: true);

            // UI dependencies
            // ---------------

            _countdownText = gpm.countdownText;
            _goText = gpm.goText;
            _cancelButton = gpm.cancelButton;
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
                UpdateUI();
            }
            else if (timerCompleted)
            {
                foreach (GameObject elem in _completeCountdownHideElements)
                {
                    elem.SetActive(false);
                }
            }
        }

        private void UpdateUI()
        {
            bool timerHalfway = _timer.TimeInteger <= _countdownTime / 2;
            if (_timer.TimeInteger == 2)
            {
                _countdownText.color = new Color(1, 0.5f, 0, 0.8f);
                _goText.SetText("READY");
            }
            else if (_timer.TimeInteger == 1)
            {
                _countdownText.color = new Color(1, 0.25f, 0, 0.8f);
                _goText.SetText("SET");
            }
            else if (_timer.TimeInteger <= 0)
            {
                _countdownText.color = new Color32(0xf3, 0x9c, 0x12, 0xFF);
                _cancelButton.enabled = false;
                _goText.SetText("GO!");
            }
            else
            {
                _goText.SetText("Starting Race...");
            }

            _countdownText.SetText(_timer.TimeString);
        }

        internal override void ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Countdown State...");
            _timer.DestroyComponent();
        }

    }
}