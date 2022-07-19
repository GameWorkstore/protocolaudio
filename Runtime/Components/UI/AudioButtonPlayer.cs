using System;
using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioButtonPlayer : MonoBehaviour, IPointerClickHandler, ISubmitHandler, ISelectHandler
    {
        public AudioName AudioNameSelected;
        public AudioName AudioNameClicked;

        private AudioService _audioService;

        private void Awake()
        {
            _audioService = ServiceProvider.GetService<AudioService>();
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            _audioService.Play2D(AudioNameSelected);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            _audioService.Play2D(AudioNameClicked);
        }

        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            _audioService.Play2D(AudioNameClicked);
        }
    }
}