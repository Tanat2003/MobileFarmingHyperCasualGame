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
            //คำนวนเงินที่ผู้เล่นจะได้
            int itemPrice =DataManager.Instance.GetCropPriceFromCropType(items[i].cropType);
            coinEarned += itemPrice * items[i].amount;


        }
        //ให้เงินผู้เล่น
        TransactionEffectManager.instance.PlayCoinParticles(coinEarned);


        //โละของในกระเป๋าผู้เล่น
        inventoryManager.ClearInventory();

    }


}
