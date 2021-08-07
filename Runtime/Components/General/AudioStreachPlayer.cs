/*using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioStreachPlayer : MonoBehaviour
    {
        public string AudioFire;
        protected int _audioFireHash;
        protected AudioService _audioService;
        public bool Bypass;
        public float MinimumAmountOfTimeBetweenFireCalls = 0.1f;

        private float _lastStreachCall;
        private float _lastFireCall;

        private void Awake()
        {
            _audioFireHash = Animator.StringToHash(AudioFire);
            _audioService = ServiceProvider.GetService<AudioService>();
        }

        public void PlayBowFire()
        {
            if (Bypass) return;
            if (Time.realtimeSinceStartup < _lastFireCall + MinimumAmountOfTimeBetweenFireCalls) return;
            _lastFireCall = Time.realtimeSinceStartup;
            _audioService.Play3D(_audioFireHash, transform.position);
        }
    }
}*/