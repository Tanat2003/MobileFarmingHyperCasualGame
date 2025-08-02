using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSFX : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;
    [SerializeField] private float volume = .3f;
    [SerializeField] private float maxPitch = 1.1f;
    [SerializeField] private float minPitch = .85f;


    private void OnEnable()
    {
        PlaySFX();
    }
    private void PlaySFX()
    {
        if (sfx == null)
            return;
        float pitch = Random.Range(minPitch, maxPitch);

        sfx.pitch = pitch;
        sfx.volume = volume;
        sfx.Play();

    }
}
