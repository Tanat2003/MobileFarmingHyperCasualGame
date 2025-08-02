using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SowAbility : MonoBehaviour
{
    [Header("Elements")]
    private Player_Animator playerAnimator;
    private Player_SelectTool playerSelectTool;
    [SerializeField] private List<CropFiled> cropFields;


    [Header("Setting")]
    private CropFiled currentField;
    

    private void Awake()
    {
        playerAnimator = GetComponent<Player_Animator>();
        playerSelectTool = GetComponent<Player_SelectTool>();
        
    }
    private void Start()
    {
        SeedParticles.onSeedCollided += SeedColliededCallback;
        CropFiled.onFullySown += CropFiledFullySownCallback;
        Player_SelectTool.onToolSelected += ToolSelectedCallback;
        
    }
   
    
    private void SeedColliededCallback(Vector3[] seedPosition) //µ”·ÀπËßcurrentField
    {
        if (currentField == null)
            return;
        currentField.SeedCollidedCallback(seedPosition);


    }
    private void OnDestroy() //µÕπ√’‚À≈¥´’πµÈÕßunSub¡—π‰¡Ëß—ÈπµÕπ‡√‘Ë¡Start¡“„À¡Ë¡—π®–À“‡¡∏Õ¥∑’Ë®–sub‰¡Ë‰¥È‡æ√“–™◊ËÕ´È”°—π
    {
        SeedParticles.onSeedCollided -= SeedColliededCallback;
        CropFiled.onFullySown -= CropFiledFullySownCallback;
        Player_SelectTool.onToolSelected -= ToolSelectedCallback;
    }

    private void ToolSelectedCallback(Player_SelectTool.Tool selectedTool)
    {
        if(!playerSelectTool.CanSow())
        {
            playerAnimator.StopSowAnimation();
        }
    }
    private void CropFiledFullySownCallback(CropFiled cropField)
    {
        if (cropField == currentField)
            playerAnimator.StopSowAnimation();

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropFiled") && other.GetComponent<CropFiled>().IsEmpty())
        {
            currentField = other.GetComponent<CropFiled>();
            EnterCropField(currentField);
        }
    }
    private void EnterCropField(CropFiled cropField)
    {
        if(playerSelectTool.CanSow())
            playerAnimator.PlaySowAnimation();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropFiled"))
        {
            playerAnimator.StopSowAnimation();
            currentField = null;
        }

    }



}
