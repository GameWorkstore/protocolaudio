using GameWorkstore.Patterns;
using System;
using System.Linq;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class BGMPack : MonoBehaviour
    {
        public AudioName AudioName;
        public int Layer = BGMService.NotSharedLayer;

        private AudioSource _audioSource;
        private AudioSource _introSource;
        private float _introDuration;
        private bool _hasPlayedIntro;
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

        [Space]
#pragma warning disable IDE0052 // Remove unread private members
        [SerializeField] private HelpBox _info = new HelpBox("BGMPacks supports intro music clips. To use it, add an additional object with 'AudioComponent' and 'BGMIntro' as children");
#pragma warning restore IDE0052 // Remove unread private members

        private void Awake()
        {
            var audios = GetComponentsInChildren<AudioSource>();
            _audioSource = audios.FirstOrDefault(IsNotStart);
            _nameHash = AudioName.Hash;
            _isPlaying = false;
            _originalVolume = _audioSource.volume;
            _audioSource.volume = 0;

            _introSource = audios.FirstOrDefault(IsIntro);
            if(_introSource != null)
            {
                _introSource.volume = 0;
                _hasPlayedIntro = false;
                _introDuration = _introSource.GetComponent<BGMIntro>().Duration;
            }

            _bgmService = ServiceProvider.GetService<BGMService>();
            _bgmService.RegisterBGM(AudioName, Layer);

            ServiceProvider.GetService<EventService>().Update.Register(UpdatePack);
        }

        private bool IsIntro(AudioSource source)
        {
            return source.GetComponent<BGMIntro>() != null;
        }

        private bool IsNotStart(AudioSource source)
        {
            return !IsIntro(source);
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
                    CopyVolumeToIntro();
                }
                _time = Time.time;
                _isPlaying = isPlaying;
            }

            // Fade pass
            if(_currentFadeCurve != null)
            {
                var delta = Time.time - _time;
                _audioSource.volume = _originalVolume * _currentFadeVolume * _currentFadeCurve.Evaluate(delta);
                CopyVolumeToIntro();
                if (delta > _currentFadeCurve.length)
                {
                    _currentFadeCurve = null;
                }
            }

            bool isNotPlaying = _audioSource.volume < 0.02f;
            if (isNotPlaying)
            {
                if (_audioSource.isPlaying || _introSource != null && _introSource.isPlaying)
                {
                    if (_introSource != null)
                    {
                        _introSource.Stop();
                        _introSource.time = 0;
                        _introSource.volume = 0;
                        _hasPlayedIntro = false;
                    }
                    _audioSource.Stop();
                    _audioSource.time = 0;
                    _audioSource.volume = 0;
                }
            }
            else
            {
                if (!_audioSource.isPlaying)
                {
                    if(_introSource != null)
                    {
                        if (!_hasPlayedIntro)
                        {
                            _introSource.Play();
                            _hasPlayedIntro = true;
                        }
                        else if (_introSource.isPlaying && _introSource.time > _introDuration)
                        {
                            _introSource.Stop();
                            _audioSource.Play();
                        }
                    }
                    else
                    {
                        _audioSource.Play();
                    }
                }
            }
        }

        private void CopyVolumeToIntro()
        {
            if (_introSource != null)
            {
                _introSource.volume = _audioSource.volume;
            }
        }
    }
}
