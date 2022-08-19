using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioButtonPlayer : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, ISubmitHandler, ISelectHandler
    {
        [Tooltip("OnSelection of the element - Better for Console Games.")]
        public AudioName AudioNameSelected;
        [Tooltip("OnClicked - After Release button.")]
        public AudioName AudioNameClicked;

        [Header("Advanced")]
        [Tooltip("OnPressed - We cannot distinguish between click or drag.")]
        public AudioName AudioNamePressed;

        private AudioService _audioService;

        private void Awake()
        {
            _audioService = ServiceProvider.GetService<AudioService>();
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            if (AudioNameSelected) _audioService.Play2D(AudioNameSelected);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (AudioNamePressed) _audioService.Play2D(AudioNamePressed);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if(AudioNameClicked) _audioService.Play2D(AudioNameClicked);
        }

        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            if(AudioNameClicked) _audioService.Play2D(AudioNameClicked);
        }

        
    }
}