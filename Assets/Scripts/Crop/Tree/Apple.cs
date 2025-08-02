using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Apple : MonoBehaviour
{
    enum State { Ready,Growing}
    private State state;
    [Header("Elements")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private float shakeMultiplier;
    private Rigidbody rb;
    private Vector3 initialPos;
    private Quaternion initialRot;




    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        initialRot = transform.rotation;
    }
    private void Start()
    {
        state = State.Growing;
    }
    public void Shake(float shakeMagnitude)
    {
        float realShakeMagnitude = shakeMagnitude * shakeMagnitude;
        renderer.material.SetFloat("_Magnitude", realShakeMagnitude); //setfloatã¹µÑÇá»Ãã¹Shader Material
       
    }

    public bool IsFree() => !rb.isKinematic;
    

    public void Release()
    {
        rb.isKinematic = false;
        state = State.Growing;
        renderer.material.SetFloat("_Magnitude", 0);
    }

    public void Reset()
    {
        //à«ç·scale¢Í§appleã¹1ÇÔ ËÅÑ§¨Ò¡¹Ñé¹2 ForecReset
        LeanTween.scale(gameObject, Vector3.zero, 1).setDelay(2).setOnComplete(ForecReset);
    }

    private void ForecReset()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
        rb.isKinematic = true;
        
        //ScaleUp
        float randomScaleTime = UnityEngine.Random.Range(30f, 60f);
        LeanTween.scale(gameObject, Vector3.one, randomScaleTime).setOnComplete(SetReady);
    }

    private void SetReady()
    {
        state = State.Ready;
    }
    public bool IsReady() => state == State.Ready;
}
