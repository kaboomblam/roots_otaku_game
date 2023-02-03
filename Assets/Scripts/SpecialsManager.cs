using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class SpecialsManager : MonoBehaviour
    {
        public int _specials;
        public int _maxSpecials;

        public float _specialTimeout;

        [SerializeField]
        private GameObject specialPrefab;

        public void TriggerSpecial()
        {
            if (_specials > 0)
            {

                _specials--;

                InstantiateSpecial();
            }
        }

        private void InstantiateSpecial()
        {

            GameObject newSpecial = Instantiate(
                specialPrefab, transform.position, transform.rotation);

            Destroy(newSpecial, _specialTimeout);

        }

        public void AddToSpecial()
        {
            if (_specials < _maxSpecials)
            {

                _specials++;

            }
        }
    }
}
