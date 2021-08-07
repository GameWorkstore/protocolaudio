using GameWorkstore.Patterns;
using System;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioPack : MonoBehaviour
    {
        [Tooltip("Should play AudioSources Randomly or not.")]
        public bool Random;
        public int AdditionalVoices = 0;

        private int SourceTracker;
        private AudioSource[] Sources;

        public float OriginalVolume { get; private set; }

        private void Awake()
        {
            ServiceProvider.GetService<AudioService>().RegisterAudioPack(this);

            Sources = GetComponentsInChildren<AudioSource>();
            int originals = Sources.Length;

            //clear audios
            for(int i = 0; i < originals; i++)
            {
                Sources[i].playOnAwake = false;
                Sources[i].Stop();
                OriginalVolume = Sources[i].volume;
            }

            Array.Resize(ref Sources, Sources.Length + AdditionalVoices);
            for(int i = originals; i < Sources.Length; i++)
            {
                Sources[i] = Instantiate(Sources[i % originals], transform);
            }
        }

        private void OnDestroy()
        {
            ServiceProvider.GetService<AudioService>().UnregisterAudioPack(this);
        }

        public AudioSource Play2D()
        {
            if (Random)
            {
                SourceTracker = UnityEngine.Random.Range(0, Sources.Length);
            }
            else
            {
                SourceTracker = ++SourceTracker % Sources.Length;
            }
            Sources[SourceTracker].spatialBlend = 0;
            Sources[SourceTracker].Play();
            return Sources[SourceTracker];
        }

        public AudioSource Play3D(Vector3 position)
        {
            if (Random)
            {
                SourceTracker = UnityEngine.Random.Range(0, Sources.Length);
            }
            else
            {
                SourceTracker = ++SourceTracker % Sources.Length;
            }
            Sources[SourceTracker].transform.position = position;
            Sources[SourceTracker].spatialBlend = 1;
            Sources[SourceTracker].Play();
            return Sources[SourceTracker];
        }

        public void Stop()
        {
            foreach(var source in Sources)
            {
                if (source.isPlaying) source.Stop();
            }
        }
    }
}