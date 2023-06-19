namespace GameWorkstore.ProtocolAudio
{
    public class AudioOnActivePlayer : AudioPlayer
    {
        private void OnEnable()
        {
            _audioService.Play2D(AudioName);
        }

        private void OnDisable()
        {
            _audioService.Stop(AudioName);
        }
    }
}