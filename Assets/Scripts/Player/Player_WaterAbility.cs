using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WaterAbility : MonoBehaviour
{
    [Header("Elements")]
    private Player_Animator playerAnimator;
    private Player_SelectTool playerSelectTool;


    [Header("Setting")]
    private CropFiled currentField;

    private void Awake()
    {
        playerAnimator = GetComponent<Player_Animator>();
        playerSelectTool = GetComponent<Player_SelectTool>();

    }
    private void Start()
    {
        WaterParticle.onWaterCollided += WaterColliededCallback;
        CropFiled.onFullyWatered += CropFiledFullyWaterCallback;
        Player_SelectTool.onToolSelected += ToolSelectedCallback;
    }

    private void WaterColliededCallback(Vector3[] waterPosition) //µ”·ÀπËßcurrentField
    {
        if (currentField == null)
            return;

        currentField.WaterCollidedCallback(waterPosition);
        


    }
    private void OnDestroy() //µÕπ√’‚À≈¥´’πµÈÕßunSub¡—π‰¡Ëß—ÈπµÕπ‡√‘Ë¡Start¡“„À¡Ë¡—π®–À“‡¡∏Õ¥∑’Ë®–sub‰¡Ë‰¥È‡æ√“–™◊ËÕ´È”°—π
    {
        WaterParticle.onWaterCollided -= WaterColliededCallback;
        CropFiled.onFullyWatered -= CropFiledFullyWaterCallback;
        Player_SelectTool.onToolSelected -= ToolSelectedCallback;
    }

    private void ToolSelectedCallback(Player_SelectTool.Tool selectedTool)
    {
        if (!playerSelectTool.CanWater())
        {
            playerAnimator.StopWaterAnimation();
        }
    }
    private void CropFiledFullyWaterCallback(CropFiled cropField)
    {
        if (cropField == currentField)
            playerAnimator.StopWaterAnimation();


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropFiled") && other.GetComponent<CropFiled>().IsSown())
        {
            currentField = other.GetComponent<CropFiled>();
            EnterCropField(currentField);
        }
    }
    private void EnterCropField(CropFiled cropField)
    {
        if (playerSelectTool.CanWater())
            playerAnimator.PlayWaterAnimation();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropFiled"))
        {
            playerAnimator.StopWaterAnimation();
            currentField = null;
        }

    }
}
