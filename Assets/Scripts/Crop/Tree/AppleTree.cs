using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject treeCamera;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Transform appleParent;
    private AppleTree_Manager treeManager;
    
    [Header("Settings")]
    [SerializeField] private float maxShakeMagnitude;//‡¢¬Ë“·√ß‰¥È·§Ë‰Àπ
    [SerializeField] private float shakeIncrement;
    private float shakeValue;
    private float shakeMagnitude;
    private bool isShaking;

    [Header("Actions")]
    public static Action<CropType> onAppleCollect;


    public void DisableTreeCam()
    {
        treeCamera.SetActive(false);
    }   
    public void EnableTreeCam()
    {
        treeCamera.SetActive(true);
    }

    public void Shake()
    {
        isShaking = true;
        TweenShake(maxShakeMagnitude);
        UpdateShakeSlider();


    }

    private void UpdateShakeSlider()
    {
        shakeValue += shakeIncrement;
        treeManager.UpdateShakeSlider(shakeValue);

        for (int i = 0; i < appleParent.childCount; i++)
        {
            float applePercent = (float)i / appleParent.childCount;
            Apple currentApple =appleParent.GetChild(i).GetComponent<Apple>();
            if(shakeValue > applePercent && !currentApple.IsFree())
            {
                ReleaseApple(currentApple);
            }

        }
        if (shakeValue >= 1)
            ExitTreeMode();
    }

    private void ExitTreeMode()
    {
        treeManager.EndedTreeMode();
        DisableTreeCam();
        TweenShake(0);
        ResetApple();
    }

    private void ResetApple()
    {
        for (int i = 0; i < appleParent.childCount; i++)
        {
            appleParent.GetChild(i).GetComponent<Apple>().Reset();
        }
    }

    private void ReleaseApple(Apple apple)
    {
        apple.Release();
        onAppleCollect?.Invoke(CropType.Apple);
    }

    public void StopShaking()
    {
        if (!isShaking)
            return;

        isShaking = false;

        TweenShake(0);
    }
    private void TweenShake(float targetMagnitude)
    {
        LeanTween.cancel(renderer.gameObject);
        LeanTween.value(renderer.gameObject, UpdateShakeMagnitude, shakeMagnitude, targetMagnitude, 1f);
    }

    private void UpdateShakeMagnitude(float value)
    {
        shakeMagnitude = value;
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        foreach (Material mat in renderer.materials)
            mat.SetFloat("_Magnitude", shakeMagnitude);
                
        foreach(Transform appleTree in appleParent)
        {
            Apple apple = appleTree.GetComponent<Apple>();
            if (apple.IsFree())
                continue;

            apple.Shake(shakeMagnitude);
        }
    }

    public void Initialize(AppleTree_Manager appleTree_Manager)
    {
        EnableTreeCam();
        shakeValue = 0;
        treeManager = appleTree_Manager;
    }

    public bool IsReady()
    {
       for (int i = 0; i< appleParent.childCount; i++)
            if (!appleParent.GetChild(i).GetComponent<Apple>().IsReady())
                return false;
       return true;
 
    }
}
