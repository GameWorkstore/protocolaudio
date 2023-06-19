namespace GameWorkstore.ProtocolAudio
{
    public class AudioOnEnablePlayer : AudioPlayer
    {
        private void OnEnable()
        {
            _audioService.Play2D(AudioName);
        }
    }
}