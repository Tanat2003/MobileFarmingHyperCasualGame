using UnityEngine;

public class Inventory_Display : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform uiCropContainerParent;
    [SerializeField] private UI_CropContainer uiCropContainerPrefab;




    public void Configue(Inventory inventory)
    {
        Inventory_Item[] items = inventory.GetInventoryItems();
        for (int i = 0; i < items.Length; i++)
        {
            UI_CropContainer cropContainer = Instantiate(uiCropContainerPrefab, uiCropContainerParent);


            Sprite cropIcon = DataManager.Instance.GetCropSpriteFromCropType(items[i].cropType);

            cropContainer.Configue(cropIcon, items[i].amount);


        }

    }

    public void UpdateDisplay(Inventory inventory)
    {
        Inventory_Item[] items = inventory.GetInventoryItems();
        for (int i = 0; i < items.Length; i++)
        {
            UI_CropContainer containerInstance;
            if (i < uiCropContainerParent.childCount)
            {
                containerInstance = 
                    uiCropContainerParent.GetChild(i).GetComponent<UI_CropContainer>();
                containerInstance.gameObject.SetActive(true);

            }else
            {
                containerInstance = Instantiate(uiCropContainerPrefab, uiCropContainerParent);

            }
            Sprite cropIcon = DataManager.Instance.GetCropSpriteFromCropType(items[i].cropType);
            containerInstance.Configue(cropIcon, items[i].amount);
        }
        int remainingContainer = uiCropContainerParent.childCount - items.Length;
        if (remainingContainer <= 0)
            return;
        for(int i = 0;i < remainingContainer;i++)
        {
            uiCropContainerParent.GetChild(items.Length + i).gameObject.SetActive(false);

        }




    }
    
}
