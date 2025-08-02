using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HarvestAbility : MonoBehaviour
{
    [Header("Elements")]
    private Player_Animator playerAnimator;
    private Player_SelectTool playerSelectTool;
    [SerializeField] private Transform harvestRadius;
    private bool canHarvest;

    [Header("Setting")]
    private CropFiled currentField;

    private void Awake()
    {
        playerAnimator = GetComponent<Player_Animator>();
        playerSelectTool = GetComponent<Player_SelectTool>();

    }
    private void Start()
    {
       
        CropFiled.onFullyHarvested += CropFiledFullyHarvestedCallback;
        Player_SelectTool.onToolSelected += ToolSelectedCallback;
    }

    
    private void OnDestroy() //ตอนรีโหลดซีนต้องunSubมันไม่งั้นตอนเริ่มStartมาใหม่มันจะหาเมธอดที่จะsubไม่ได้เพราะชื่อซ้ำกัน
    {
        
        CropFiled.onFullyHarvested -= CropFiledFullyHarvestedCallback;
        Player_SelectTool.onToolSelected -= ToolSelectedCallback;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropFiled") && other.GetComponent<CropFiled>().IsWatered())
        {
            currentField = other.GetComponent<CropFiled>();
            EnterCropField(currentField);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropFiled"))
        {
            playerAnimator.StopHarvestAnimation();
            currentField = null;
        }

    }

    private void ToolSelectedCallback(Player_SelectTool.Tool selectedTool)
    {
        if (!playerSelectTool.CanHarvest())
        {
            playerAnimator.StopHarvestAnimation();
        }
    }
    private void CropFiledFullyHarvestedCallback(CropFiled cropField)
    {
        if (cropField == currentField)
            playerAnimator.StopHarvestAnimation();


    }
    private void EnterCropField(CropFiled cropField)
    {
        if (playerSelectTool.CanHarvest())
        {
            playerAnimator.PlayHarvestAnimation();
            if (canHarvest == true)
                currentField.Harvest(harvestRadius);
        }

        
    }



    public void HarvestStartCallback() => canHarvest = true;


    public void HarvestStopCallback()
    {
        canHarvest = false;
        
    } 

}
