using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    [CreateAssetMenu(fileName = "CarSelectSO", menuName = "OtakuGameJam/CarSelectSO", order = 0)]
    public class CarSelectSO : ScriptableObject
    {
        public string carName;
        public string description;
        public Sprite carSprite;
        [Range(0, 100)]
        public float defenseStrength = 70;
        [Range(0, 100)]
        public float nitrousStrength = 85;
    }
}
