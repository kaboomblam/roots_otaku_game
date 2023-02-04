using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OtakuGameJam
{

    public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        // get audio source
        private AudioSource _audioSource;
        [SerializeField]
        MenuManager _menuManager;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("OnPointerEnter");
            _audioSource.PlayOneShot(_menuManager.buttonHoverSound);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Debug.Log("OnPointerDown");
            _audioSource.PlayOneShot(_menuManager.buttonClickSound);
        }
    }
}