using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    public class BGMPlayerOnEnable : MonoBehaviour
    {
        public AudioName AudioName;

        private void OnEnable()
        {
            ServiceProvider.GetService<BGMService>().Play(AudioName);
        }
    }
}
