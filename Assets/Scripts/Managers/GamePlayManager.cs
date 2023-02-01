using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class GamePlayManager : MonoBehaviour
    {
        [Tooltip("Allows for GamePlay Manager to use Game wide Settings or Defaults instead of the inspector settings")]
        [SerializeField]
        private bool _useGlobalSettings = false;

        [Range(1, 5)]
        [SerializeField]
        private int _laps = 3;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
