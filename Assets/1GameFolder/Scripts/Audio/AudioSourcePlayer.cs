using Cysharp.Threading.Tasks;
using GameFolder.Audio;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _playOnAwake;
    [SerializeField] private float _delay;

    private void Awake()
    {
        if (_playOnAwake)
        {
            if (_delay <= 0)
            {
                AudioController.PlayEvent(_audioSource);
            }
            else
            {
                PlayAfterDelay(_delay).Forget();
            }
        }
    }

    private async UniTaskVoid PlayAfterDelay(float delay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay));
        AudioController.PlayEvent(_audioSource);
    }
}
