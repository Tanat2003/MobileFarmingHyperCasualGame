using System;
using System.IO;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    //‡ªÁπ§≈“ ‡°Á∫¢ÈÕ¡Ÿ≈·≈–‚À≈¥JsonFile §”Õ∏‘∫“¬¿“æ√«¡Õ¬ŸË„πInventory 


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

        dataPath = Application.persistentDataPath + "/inventoryData.txt"; //µÕπPolsih‡ª≈’Ë¬π‡ªÁπpersistentDataPath


        dataPath = Application.dataPath + "/inventoryData.txt"; //µÕπPolsih‡ª≈’Ë¬π‡ªÁπpersistentDataPath
        //µÕπ‡∑ ‡ªÁπdataPath

        LoadInventory();
        inventoryDisplay.Configue(inventory);


        
    }

    #region InventoryMethod
    private void LoadInventory()
    {
         

        if (File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath); //ÕË“π‰ø≈Ïtxt∑—ÈßÀ¡¥„πpath·≈È«‡°Á∫‰«È„πdata
            inventory = JsonUtility.FromJson<Inventory>(data); //convert string data ‡ªÁπ inventoryType ·≈È«‡°Á∫„πinventory

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
