using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //�����ʹ������öSerialized �� Json��
public class Inventory_Item
{
    //�����Ţͧ����
    public CropType cropType;
    public int amount;

    public Inventory_Item(CropType cropType, int amount)
    {
        this.cropType = cropType;
        this.amount = amount;
    }

    
}
