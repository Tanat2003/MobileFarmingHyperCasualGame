using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_AnimationEvent : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private ParticleSystem waterParticle;

    [Header("Event")]
    [SerializeField] private UnityEvent startHarvestEvent;
    [SerializeField] private UnityEvent stopHarvestEvent;


    #region Particle Method
    private void PlaySeedParticle() => seedParticle.Play();


    private void PlayWaterParticle()
    {
        AudioManager.instance.PlaySFX(3);
        waterParticle.Play();
    }
    #endregion

    #region Harvest Method
    private void StartHarvestCallback()
    {
        AudioManager.instance.PlaySFX(2);
        startHarvestEvent?.Invoke();
    }
    private void StopHarvestCallback()
    {
        AudioManager.instance.PlaySFX(2);
        startHarvestEvent?.Invoke();
    }
    #endregion
}
