using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class MenuManager : MonoBehaviour
    {
        public AudioClip buttonHoverSound;
        public AudioClip buttonClickSound;

        private void Start()
        {
            GameManager.OnGameStateChange += (state) => Debug.Log($"Game State Changed! {state}");
        }

        public void ShowCarSelection()
        {

        }

        public void ShowSettings()
        {
            // TODO: Show settings ui
            Debug.Log("Showing settings...");
        }

        public void ShowHighScore()
        {
            // TODO: Show high score ui
            Debug.Log("Showing high score...");
        }

        private void OnDestroy()
        {
            // TODO: Unsubscribe from events
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
