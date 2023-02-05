using UnityEngine;

namespace OtakuGameJam
{
    class PausedPlayState : PlayState
    {
        internal override void EnterState(GamePlayManager gpm)
        {
            Debug.Log("Entered Paused State...");
            gpm.pauseUI.SetActive(true);

            Time.timeScale = 0;
        }

        internal override void UpdateState(GamePlayManager gpm)
        {
            gpm.DEBUG_StateText.SetText("Paused State...");

            bool paused = Input.GetKey(KeyCode.Tab);

            if (paused)
            {
                gpm.ChangeState(Constants.PlayStateValues.Playing);
            }
        }

        internal override bool ExitState(GamePlayManager gpm)
        {
            Debug.Log("Exited Paused State !!!!...");
            gpm.pauseUI.SetActive(false);
            Time.timeScale = 1;
            return true;
        }
    }
}