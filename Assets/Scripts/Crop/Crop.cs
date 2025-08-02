using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform cropRenderer;
    [SerializeField] private ParticleSystem harvestParticle;


    #region SetScaleMethod
    public void ScaleUp()
    {
        cropRenderer.gameObject.LeanScale(Vector3.one, 1).setEase(LeanTweenType.easeOutSine);
    }
    public void ScaleDown()
    {
        cropRenderer.gameObject.
            LeanScale(Vector3.zero, 1).setEase(LeanTweenType.easeOutSine).setOnComplete(DestroyCrop);

        harvestParticle.transform.parent = null;
        harvestParticle.gameObject.SetActive(true);
        harvestParticle.Play();
    }
    #endregion



    private void DestroyCrop()
    {
        Destroy(gameObject);
    }
}
