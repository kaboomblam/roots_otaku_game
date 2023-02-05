using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OtakuGameJam
{
    public class CarSelect : MonoBehaviour
    {
        public CarSelectSO carSelectSO;

        [SerializeField]
        TMPro.TextMeshProUGUI _carName;
        [SerializeField]
        TMPro.TextMeshProUGUI _carDescription;
        [SerializeField]
        Image _carImageSprite;
        [SerializeField]
        Image _defenseFill;
        [SerializeField]
        Image _nitrousFill;

        // Start is called before the first frame update
        void Start()
        {
            _carName.text = carSelectSO.carName;
            _carDescription.text = carSelectSO.description;
            _carImageSprite.sprite = carSelectSO.carSprite;
            _defenseFill.fillAmount = carSelectSO.defenseStrength / 100;
            _nitrousFill.fillAmount = carSelectSO.nitrousStrength / 100;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
