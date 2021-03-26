using System;
using System.Collections.Generic;
using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.Audio;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioService : IService
    {
        private AudioMixer _master;
        private readonly Dictionary<int, AudioPack> _audioDictionary = new Dictionary<int, AudioPack>();

        public override void Preprocess()
        {
        }

        public override void Postprocess()
        {
        }

        internal void RegisterAudioPack(AudioPack audioPack)
        {
            int hash = Animator.StringToHash(audioPack.name);
            if (!_audioDictionary.ContainsKey(hash))
            {
                _audioDictionary.Add(hash, audioPack);
            }
        }

        internal AudioSource Play2DRandom(int audioNameHash)
        {
            if (_audioDictionary.ContainsKey(audioNameHash))
                return _audioDictionary[audioNameHash].Play2DRandom();
            return null;
        }

        internal AudioSource Play2D(int audioNameHash)
        {
            if (_audioDictionary.ContainsKey(audioNameHash))
                return _audioDictionary[audioNameHash].Play2D();
            return null;
        }

        internal AudioSource Play3D(int audioNameHash, Vector3 position)
        {
            if (_audioDictionary.ContainsKey(audioNameHash))
                return _audioDictionary[audioNameHash].Play3D(position);
            return null;
        }

        internal void UnregisterAudioPack(AudioPack audioPack)
        {
            int hash = Animator.StringToHash(audioPack.name);
            if (_audioDictionary.ContainsKey(hash))
            {
                _audioDictionary.Remove(hash);
            }
        }

        internal void SetCurrentMaster(AudioMixer master)
        {
            _master = master;
        }

        public AudioMixer GetCurrentMaster()
        {
            return _master;
        }
    }
}
