using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform rendererTransform;
    [SerializeField] private ParticleSystem waterParticle;
    private Animator animator;
    [Header("Settings")]
    private float moveSpeedMutiplier = 40;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void ManageAnimations(Vector3 moveVector)
    {
        if(moveVector.magnitude > 0)
        {
            animator.SetFloat("moveSpeed", moveVector.magnitude * moveSpeedMutiplier);
            PlayRunAnimation();
            rendererTransform.forward = moveVector.normalized; //กำหนดตำแหน่งหันหน้า ให้เท่ากับทิศทางที่JoyStickKnobเคลื่อนไป
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        animator.Play("Idle");   
    }

    private void PlayRunAnimation()
    {
        
        animator.Play("Run");
    }
    public void PlaySowAnimation()
    {
        animator.SetLayerWeight(1,1);
        animator.Play("Sow");
    }
    public void StopSowAnimation()
    {
        animator.SetLayerWeight(1,0);
        
        
    }
    public void StopWaterAnimation()
    {
        animator.SetLayerWeight(2, 0);
        waterParticle.Stop();
    }
    public void PlayWaterAnimation()
    {
        animator.SetLayerWeight(2, 1);
        animator.Play("Water");

        
    }

    public void StopHarvestAnimation()
    {
        animator.SetLayerWeight(3,0);
        
    }
    public void PlayHarvestAnimation()
    {
        animator.SetLayerWeight(3,1);
        animator.Play("Harvest");
    }

    public void PlayShakeTreeAnimation()
    {

        animator.SetLayerWeight(4, 1);
        animator.Play("Shake Tree");
    }

    public void StopShakeTreeAnimation()
    {
        animator.SetLayerWeight(4, 0);
        
    }
}
