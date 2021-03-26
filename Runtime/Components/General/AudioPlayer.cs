using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioPlayer : MonoBehaviour
    {
        public string AudioName;
        protected int _audioNameHash;
        protected AudioService _audioService;

        private void Awake()
        {
            _audioNameHash = Animator.StringToHash(AudioName);
            _audioService = ServiceProvider.GetService<AudioService>();
        }

        public void Play2D()
        {
            _audioService.Play2D(_audioNameHash);
        }

        public void Play2DRandom()
        {
            _audioService.Play2DRandom(_audioNameHash);
        }
    }
}