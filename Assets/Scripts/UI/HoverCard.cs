using UnityEngine;
using UnityEngine.EventSystems;


namespace OtakuGameJam
{
    public class HoverCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private CanvasGroup _card;
        private AudioSource _audioSource;

        private MenuManager _menuManager;


        private void Start()
        {
            _card = GetComponent<CanvasGroup>();
            _card.alpha = 0.9f;

            _audioSource = GetComponent<AudioSource>();
            _menuManager = GetComponent<CarSelect>().menuManager;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.LeanScale(Vector3.one * 1.025f, 0.2f).setEaseOutBack();
            _card.LeanAlpha(1f, 0.2f).setEaseOutBack();

            _audioSource.PlayOneShot(_menuManager.buttonHoverSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.LeanScale(Vector3.one, 0.2f).setEaseOutBack();
            _card.LeanAlpha(0.9f, 0.2f).setEaseOutBack();

            // _audioSource.PlayOneShot(_menuManager.bu);
        }

        private void Selected()
        {
            _audioSource.PlayOneShot(_menuManager.buttonClickSound);
            transform.LeanScale(Vector3.one * .9f, 0.1f).setEaseInOutBounce().setLoopPingPong(1);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Selected();
        }
    }
}
