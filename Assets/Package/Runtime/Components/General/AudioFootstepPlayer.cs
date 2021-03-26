using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioFootstepPlayer : MonoBehaviour
    {
        public string AudioName;
        protected int _audioNameHash;
        protected AudioService _audioService;
        public bool Bypass;
        public float MinimumAmountOfTimeBetweenCalls = 0.1f;

        private float _lastCall;

        private void Awake()
        {
            Preprocess();
        }

        public void Preprocess()
        {
            _audioNameHash = Animator.StringToHash(AudioName);
            _audioService = ServiceProvider.GetService<AudioService>();
        }

        public void PlayFootstep()
        {
            if (Bypass) return;
            if (Time.realtimeSinceStartup < _lastCall + MinimumAmountOfTimeBetweenCalls) return;
            _lastCall = Time.realtimeSinceStartup;
            _audioService.Play3D(_audioNameHash, transform.position);
        }
    }
}