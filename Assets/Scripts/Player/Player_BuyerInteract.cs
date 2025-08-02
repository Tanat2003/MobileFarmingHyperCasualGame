using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BuyerInteract : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Inventory_Manager inventoryManager; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buyer"))
            SellCrops();
    }

    private void SellCrops()
    {
        Inventory inventory = inventoryManager.GetInventory();
        Inventory_Item[] items = inventory.GetInventoryItems();
        int coinEarned = 0;
        for (int i = 0; i < items.Length; i++)
        {
            //�ӹǹ�Թ�������蹨���
            int itemPrice =DataManager.Instance.GetCropPriceFromCropType(items[i].cropType);
            coinEarned += itemPrice * items[i].amount;


        }
        //����Թ������
        TransactionEffectManager.instance.PlayCoinParticles(coinEarned);


        //��Тͧ㹡����Ҽ�����
        inventoryManager.ClearInventory();

    }


}
