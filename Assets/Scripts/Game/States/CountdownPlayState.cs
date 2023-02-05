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

        private bool _calledForExit = false;

        private GamePlayManager _cachedGamePlayManager;
        private AudioSource _cachedAudioSource;


        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Countdown State...");

            _cachedGamePlayManager = gpm;
            _cachedAudioSource = gpm.GetComponent<AudioSource>();


            _timer = gpm.gameObject.AddComponent<TimerBehaviour>();
            _countdownTime = gpm.countdownToStart;

            _timer.CreateTimer(_countdownTime, isCountDown: true);

            // UI dependencies
            // ---------------

            _countdownText = gpm.countdownText;
            _goText = gpm.goText;
            _cancelButton = gpm.cancelButton;
            _completeCountdownHideElements = gpm.completeCountdownHideElements;

            VisibleDependentElements(true); // ensure elements are active
        }

        void VisibleDependentElements(bool active)
        {
            foreach (GameObject elem in _completeCountdownHideElements)
            {
                elem.SetActive(active);
            }
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
                VisibleDependentElements(false);
                gpm.ChangeState(Constants.PlayStateValues.Playing);
            }
        }

        private void UpdateUI()
        {
            bool timerHalfway = _timer.TimeInteger <= _countdownTime / 2;
            if (_timer.TimeInteger == 3)
            {
                // _countdownText.color = new Color(1, 1, 0, 0.8f);
                _goText.SetText("READY!");
                _countdownText.SetText(_timer.TimeString);
                // PlayCountDownSound();
            }
            if (_timer.TimeInteger == 2)
            {
                // _countdownText.color = new Color(1, 0.5f, 0, 0.8f);
                _goText.SetText("READY!");
                _countdownText.SetText(_timer.TimeString);
                // PlayCountDownSound();

            }
            else if (_timer.TimeInteger == 1)
            {
                // _countdownText.color = new Color(1, 0.25f, 0, 0.8f);
                _goText.SetText("SET");
                _countdownText.SetText(_timer.TimeString);
                // PlayCountDownSound();

            }
            else if (_timer.TimeInteger <= 0)
            {
                _countdownText.color = new Color32(0x7f, 0x8f, 0xa6, 0xFF);
                _cancelButton.enabled = false;
                _goText.SetText("START");
                _countdownText.SetText("GO!");
                PlayCountDownSound();

            }
            else
            {
                _goText.SetText("STARTING...");
            }

        }

        void PlayCountDownSound()
        {
            _cachedAudioSource.PlayOneShot(_cachedGamePlayManager.countdownSound);

        }

        internal override bool ExitState(GamePlayManager gpm)
        {
            bool alreadyCalledForExit = _calledForExit;

            if (alreadyCalledForExit)
            {
                bool timerElementExists = _timer != null;

                if (timerElementExists)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            _timer.DestroyComponent();

            _calledForExit = true;
            return false;
        }

    }
}