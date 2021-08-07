namespace GameWorkstore.ProtocolAudio
{
    public class AudioAwakePlayer : AudioPlayer
    {
        private void Start()
        {
            _audioService.Play2D(_audioNameHash);
        }
    }
}