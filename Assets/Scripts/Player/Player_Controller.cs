using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Controller : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private MobileJoyStick joyStick;
    private CharacterController characterController;
    private Player_Animator playerAnimator;
    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    private bool canControll;

    public static Player_Controller instance;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Player_Animator>();
        instance = this;
        
    }
    private void Start()
    {
        canControll = true;
    }
    private void Update()
    {
        if (!canControll)
            return;
        ManageMovement();
    }

    private void ManageMovement()
    {
        Vector3 moveVector = joyStick.GetMouseVector()*moveSpeed*Time.deltaTime/Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;


        characterController.Move(moveVector);
        playerAnimator.ManageAnimations(moveVector);
    }
    public void SetCanPlayerControl(bool activate)
    {
        canControll = activate;
    }
}
