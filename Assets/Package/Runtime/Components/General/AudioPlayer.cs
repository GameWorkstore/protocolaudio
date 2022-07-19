using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioName AudioName;
        protected AudioService _audioService;

        private void Awake()
        {
            _audioService = ServiceProvider.GetService<AudioService>();
        }

        public void Play2D()
        {
            _audioService.Play2D(AudioName);
        }
    }
}