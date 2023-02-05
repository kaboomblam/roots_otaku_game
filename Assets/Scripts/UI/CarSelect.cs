using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace OtakuGameJam
{
    public class CarSelect : MonoBehaviour, IPointerDownHandler
    {
        [Header("Dependencies")]
        public CarSelectSO carSelectSO;
        public MenuManager menuManager;

        [Header("UI Elements")]

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
        [SerializeField]
        Image _cardBackground;

        [Header("Card BG States")]
        [SerializeField]
        Sprite _defaultBG;
        [SerializeField]
        Sprite _selectedBG;


        // Start is called before the first frame update
        void Start()
        {
            _carName.text = carSelectSO.carName;
            _carDescription.text = carSelectSO.description;
            _carImageSprite.sprite = carSelectSO.carSprite;
            _defenseFill.fillAmount = carSelectSO.defenseStrength / 100;
            _nitrousFill.fillAmount = carSelectSO.nitrousStrength / 100;

        }

        private void Awake()
        {
            menuManager.carSelectOptions.Add(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SelectCard();
        }

        void SelectCard()
        {
            GetComponent<Image>().sprite = _selectedBG;
            menuManager.SelectCar(this);
        }

        public void DeselectCard()
        {
            GetComponent<Image>().sprite = _defaultBG;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
