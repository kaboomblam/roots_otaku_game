using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class TimerBehaviour : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator StartTimer()
        {
            yield return null;
        }

        public void DestroyComponent()
        {
            Destroy(this);
        }

        private void OnDestroy()
        {
            Debug.Log($"Destroying timer...");
        }
    }
}
