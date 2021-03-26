using GameWorkstore.Patterns;
using System;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioPack : MonoBehaviour
    {
        public AudioSource[] Sources;
        public int AdditionalVoices = 0;
        private int SourceTracker;

        private void Awake()
        {
            ServiceProvider.GetService<AudioService>().RegisterAudioPack(this);

            int originals = Sources.Length;
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

        internal AudioSource Play2DRandom()
        {
            SourceTracker = UnityEngine.Random.Range(0, Sources.Length);
            Sources[SourceTracker].spatialBlend = 0;
            Sources[SourceTracker].Play();
            return Sources[SourceTracker];
        }

        internal AudioSource Play2D()
        {
            SourceTracker = ++SourceTracker % Sources.Length;
            //int rng = Random.Range(0, Sources.Length);
            Sources[SourceTracker].spatialBlend = 0;
            Sources[SourceTracker].Play();
            return Sources[SourceTracker];
        }

        internal AudioSource Play3D(Vector3 position)
        {
            SourceTracker = ++SourceTracker % Sources.Length;
            //int rng = Random.Range(0, Sources.Length);
            Sources[SourceTracker].transform.position = position;
            Sources[SourceTracker].spatialBlend = 1;
            Sources[SourceTracker].Play();
            return Sources[SourceTracker];
        }
    }
}