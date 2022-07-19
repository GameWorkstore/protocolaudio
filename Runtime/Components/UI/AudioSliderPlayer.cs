using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioSlider : MonoBehaviour, ISelectHandler
    {
        public AudioName AudioNameSelected;
        public AudioName AudioNameValueChanged;

        private AudioService _audioService;

        private void Awake()
        {
            _audioService = ServiceProvider.GetService<AudioService>();

            GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _audioService.Play2D(AudioNameValueChanged);
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            _audioService.Play2D(AudioNameSelected);
        }
    }
}