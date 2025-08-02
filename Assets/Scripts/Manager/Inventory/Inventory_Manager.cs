using System;
using System.IO;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    //�繤����红����������ŴJsonFile ��͸Ժ���Ҿ��������Inventory 


    private Inventory inventory;
    private string dataPath;
    private Inventory_Display inventoryDisplay;
    private void Awake()
    {
        CropTile.onCropHarvested += CropHarvestedCallback;
        AppleTree.onAppleCollect += CropHarvestedCallback;

    }
    private void Start()
    {
        inventoryDisplay = GetComponent<Inventory_Display>();

        dataPath = Application.persistentDataPath + "/inventoryData.txt"; //�͹Polsih����¹��persistentDataPath


        dataPath = Application.dataPath + "/inventoryData.txt"; //�͹Polsih����¹��persistentDataPath
        //�͹����dataPath

        LoadInventory();
        inventoryDisplay.Configue(inventory);


        
    }

    #region InventoryMethod
    private void LoadInventory()
    {
         

        if (File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath); //��ҹ���txt�������path����������data
            inventory = JsonUtility.FromJson<Inventory>(data); //convert string data �� inventoryType �������inventory

            if (inventory == null)
            {
                inventory = new Inventory();
            }
        }
        else
        {
            File.Create(dataPath);
            inventory = new Inventory();
        }
        


    }



    private void OnDestroy()
    {
        CropTile.onCropHarvested -= CropHarvestedCallback;
        AppleTree.onAppleCollect -= CropHarvestedCallback;
    }

    private void SaveInventory()
    {
        string data =JsonUtility.ToJson(inventory,true);
        File.WriteAllText(dataPath,data);


    }
    [NaughtyAttributes.Button]
    public void ClearInventory()
    {
        inventory.Clear();
        inventoryDisplay.UpdateDisplay(inventory);
        SaveInventory();
    }
    public Inventory GetInventory() => inventory;
    #endregion
    private void CropHarvestedCallback(CropType cropType)
    {
        //Update our inventory
        inventory.CropHarvestedCallback(cropType);
        inventoryDisplay.UpdateDisplay(inventory);
        SaveInventory();
    }
    
}
