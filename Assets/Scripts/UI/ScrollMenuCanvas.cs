using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OtakuGameJam
{
    public class ScrollMenuCanvas : MonoBehaviour
    {
        [SerializeField]
        private RawImage _img;
        [SerializeField]
        private float _speed;

        // Update is called once per frame
        void Update()
        {
            _img.uvRect = new Rect(_img.uvRect.x + _speed * Time.deltaTime, 0, 1, 1);
        }
    }
}
