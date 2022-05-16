using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameWorkstore.ProtocolAudio;
using GameWorkstore.Patterns;

public class TriggerEnd : MonoBehaviour
{
    private static int Audio_TriggerEnd = Animator.StringToHash("trigger_end_game");

    private AudioService _audioService;

    private void Awake()
    {
        _audioService = ServiceProvider.GetService<AudioService>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _audioService.Play3D(Audio_TriggerEnd, transform.position);
    }
}
