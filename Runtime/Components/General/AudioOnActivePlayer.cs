namespace GameWorkstore.ProtocolAudio
{
    public class AudioOnActivePlayer : AudioPlayer
    {
        private void OnEnable()
        {
            _audioService.Play2D(_audioNameHash);
        }

        private void OnDisable()
        {
            _audioService.Stop(_audioNameHash);
        }
    }
}