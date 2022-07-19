using UnityEngine;
using GameWorkstore.ProtocolAudio;
using GameWorkstore.Patterns;

public class TriggerEnd : MonoBehaviour
{
    public AudioName AudioTriggerEnd;

    private AudioService _audioService;

    private void Awake()
    {
        _audioService = ServiceProvider.GetService<AudioService>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _audioService.Play3D(AudioTriggerEnd, transform.position);
    }
}
