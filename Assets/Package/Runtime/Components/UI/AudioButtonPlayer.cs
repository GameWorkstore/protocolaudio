using System;
using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioButtonPlayer : MonoBehaviour, IPointerClickHandler, ISubmitHandler, ISelectHandler
    {
        public string AudioNameSelected;
        public string AudioNameClicked;

        private int _audioNameSelectedHash;
        private int _audioNameClickedHash;
        private AudioService _audioService;

        private void Awake()
        {
            _audioNameSelectedHash = Animator.StringToHash(AudioNameSelected);
            _audioNameClickedHash = Animator.StringToHash(AudioNameClicked);
            _audioService = ServiceProvider.GetService<AudioService>();
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            _audioService.Play2D(_audioNameSelectedHash);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            _audioService.Play2D(_audioNameClickedHash);
        }

        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            _audioService.Play2D(_audioNameClickedHash);
        }
    }
}