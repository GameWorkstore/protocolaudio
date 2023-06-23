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
            var hash = audioPack.AudioName.Hash;
            if (!_audioDictionary.ContainsKey(hash))
            {
                _audioDictionary.Add(hash, audioPack);
            }
        }

        internal void UnregisterAudioPack(AudioPack audioPack)
        {
            var hash = audioPack.AudioName.Hash;
            if (_audioDictionary.ContainsKey(hash))
            {
                _audioDictionary.Remove(hash);
            }
        }

        public AudioMixer GetCurrentMaster()
        {
            return _master;
        }

        internal void SetCurrentMaster(AudioMixer master)
        {
            _master = master;
        }

        public AudioSource Play2D(AudioName audioName)
        {
            if (_audioDictionary.TryGetValue(audioName.Hash, out AudioPack pack))
                return pack.Play2D();
            return null;
        }

        public AudioSource Play3D(AudioName audioName, Vector3 position)
        {
            if (_audioDictionary.TryGetValue(audioName.Hash, out AudioPack pack))
                return pack.Play3D(position);
            return null;
        }

        public void Stop(AudioName audioName)
        {
            if (!_audioDictionary.TryGetValue(audioName.Hash, out AudioPack pack)) return;
            pack.Stop();
        }
    }
}
