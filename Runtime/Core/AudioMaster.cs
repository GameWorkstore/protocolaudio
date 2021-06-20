using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.Audio;

namespace GameWorkstore.ProtocolAudio
{
    public class AudioMaster : MonoBehaviour
    {
        public AudioMixer Master;

        private void Awake()
        {
            ServiceProvider.GetService<AudioService>().SetCurrentMaster(Master);
        }
    }
}