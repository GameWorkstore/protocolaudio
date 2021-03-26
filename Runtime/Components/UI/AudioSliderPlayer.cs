using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioSlider : MonoBehaviour, ISelectHandler
    {
        public string AudioNameSelected;
        public string AudioNameValueChanged;

        private int _audioNameSelectedHash;
        private int _audioNameValueChangedHash;
        private AudioService _audioService;

        private void Awake()
        {
            _audioNameSelectedHash = Animator.StringToHash(AudioNameSelected);
            _audioNameValueChangedHash = Animator.StringToHash(AudioNameValueChanged);
            _audioService = ServiceProvider.GetService<AudioService>();

            GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _audioService.Play2D(_audioNameValueChangedHash);
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            _audioService.Play2D(_audioNameSelectedHash);
        }
    }
}