using UnityEngine;

namespace GameWorkstore.ProtocolAudio
{
    [CreateAssetMenu(fileName=nameof(AudioName),menuName="ProtocolAudio/"+nameof(AudioName))]
    public class AudioName : ScriptableObject
    {
        public int Hash => Animator.StringToHash(name);
    }
}