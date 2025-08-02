using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    //�����Item�红����Ţͧ���� ��� Inventory�繤��ʷ��������Itemŧ����ʵ�
    //�ҡ��������InventoryManager �����红�����Inventoryŧ JsonFile�ա��



    //Inventory�ͧPlayer
    [SerializeField] private List<Inventory_Item> items = new List<Inventory_Item>();

    public void CropHarvestedCallback(CropType cropType) //Add Item To List
    {
        bool cropFound = false;
        //���������ʵ���Croptype
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

        //���ҧ���������������ʵ���CropType
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
