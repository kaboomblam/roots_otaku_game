using System.Collections;
using System.Collections.Generic;
using OtakuGameJam.Attributes;
using UnityEngine;

namespace OtakuGameJam
{
    public class TimerBehaviour : MonoBehaviour
    {
        [SerializeField]
        [DisableProperty]
        private float _time = 0f;
        private bool _isCountingDown = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateTimer(float time, bool isCountingDown = false)
        {
            this._isCountingDown = isCountingDown;
            this._time = time;
        }

        public void RunTimer()
        {

        }

        public void StopTimer()
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
