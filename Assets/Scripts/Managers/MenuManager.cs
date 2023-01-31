using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class MenuManager : MonoBehaviour
    {

        private void Start()
        {
            GameManager.OnGameStateChange += (state) => Debug.Log($"Game State Changed! {state}");
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
