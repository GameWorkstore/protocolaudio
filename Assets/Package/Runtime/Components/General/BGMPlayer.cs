using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class BGMPlayer : MonoBehaviour
    {
        public AudioName AudioName;

        public void Play()
        {
            ServiceProvider.GetService<BGMService>().Play(AudioName);
        }
    }
}
