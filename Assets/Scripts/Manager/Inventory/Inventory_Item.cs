using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //ให้คลาสนี้สามารถSerialized โดย Jsonได้
public class Inventory_Item
{
    //ข้อมูลของไอเทม
    public CropType cropType;
    public int amount;

    public Inventory_Item(CropType cropType, int amount)
    {
        this.cropType = cropType;
        this.amount = amount;
    }

    
}
