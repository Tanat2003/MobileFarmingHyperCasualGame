using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    //เรามีItemเก็บข้อมูลของไอเทม และ Inventoryเป็นคลาสที่คอยเพิ่มItemลงไปในลิสต์
    //จากนั้นเราใช้InventoryManager เพื่อเก็บข้อมูลInventoryลง JsonFileอีกที



    //InventoryของPlayer
    [SerializeField] private List<Inventory_Item> items = new List<Inventory_Item>();

    public void CropHarvestedCallback(CropType cropType) //Add Item To List
    {
        bool cropFound = false;
        //หาไอเทมในลิสต์ตามCroptype
        for (int i = 0; i < items.Count; i++)
        {
            Inventory_Item item = items[i];

            if(item.cropType == cropType)
            {
                item.amount++;
                cropFound = true;
                break;
            }
        }
        
        if (cropFound)
            return;

        //สร้างไอเทมใหม่ขึ้นมาในลิสต์ตามCropType
        items.Add(new Inventory_Item(cropType, 1));




    }
    

    public void Clear()
    {
        items.Clear();
    }

    internal Inventory_Item[] GetInventoryItems()
    {
        return items.ToArray();
    }
}
