using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class BGMPack : MonoBehaviour
    {
        public AudioName AudioName;
        public int Layer = BGMService.NotSharedLayer;

        private AudioSource _audioSource;
        private BGMService _bgmService;
        private int _nameHash;
        private bool _isPlaying;
        private float _originalVolume;

        [Header("FadeIn")]
        public bool UseFadeIn = true;
        public AnimationCurve FadeInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("FadeOut")]
        public bool UseFadeOut = true;
        public AnimationCurve FadeOutCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        private float _time;
        private AnimationCurve _currentFadeCurve;
        private float _currentFadeVolume;

        private void Awake()
        {
            _audioSource = GetComponentInChildren<AudioSource>();
            _nameHash = AudioName.Hash;
            _isPlaying = false;
            _originalVolume = _audioSource.volume;
            _audioSource.volume = 0;

            _bgmService = ServiceProvider.GetService<BGMService>();
            _bgmService.RegisterBGM(AudioName, Layer);

            ServiceProvider.GetService<EventService>().Update.Register(UpdatePack);
        }

        private void OnDestroy()
        {
            ServiceProvider.GetService<EventService>().Update.Unregister(UpdatePack);
        }

        private void UpdatePack()
        {
            bool isPlaying = _bgmService.IsPlaying(_nameHash);
            if (_isPlaying != isPlaying)
            {
                if (isPlaying && UseFadeIn)
                {
                    _currentFadeCurve = FadeInCurve;
                    _currentFadeVolume = 1;
                }
                else if (!isPlaying && UseFadeOut)
                {
                    _currentFadeCurve = FadeOutCurve;
                    _currentFadeVolume = _audioSource.volume;
                }
                else
                {
                    _currentFadeCurve = null;
                    _audioSource.volume = isPlaying ? 1 : 0;
                }
                _time = Time.time;
                _isPlaying = isPlaying;
            }

            // Fade pass
            if(_currentFadeCurve != null)
            {
                var delta = Time.time - _time;
                _audioSource.volume = _originalVolume * _currentFadeVolume * _currentFadeCurve.Evaluate(delta);
                if(delta > _currentFadeCurve.length)
                {
                    _currentFadeCurve = null;
                }
            }

            bool isNotPlaying = Mathf.Approximately(_audioSource.volume, 0);
            if (isNotPlaying)
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
            }
            else
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
        }
    }
}
