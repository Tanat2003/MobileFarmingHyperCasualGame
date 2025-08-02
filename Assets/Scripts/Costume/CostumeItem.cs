using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostumeItem : MonoBehaviour
{
    [SerializeField] private CostumeItemData costumeItemData;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private CostumePiece costumePieece;
   
   
    private void Start()
    {

        //This is mean we are in other Scenes that be able to buy item 
        if(UI_CashManagerOnOtherScenes.instance != null)
        {
            
            
            buyButton.onClick.AddListener(() =>
            {

                UI_CashManagerOnOtherScenes.instance.DecreaseCoin(costumeItemData.price);
                
                costumeItemData.unlocked = true;
                costumeItemData.isUsing = true;
                HideBuyButton();
            });
        }
       



    }   


    public void UpdatePlayerSelectedItem(bool active)
    {
        costumeItemData.isUsing = active;
    }
    public void ShowBuyButton()
    {
        buyButton.gameObject.SetActive(true);
        priceText.text = "-" + costumeItemData.price.ToString() + " $";
    }
    public void HideBuyButton()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.gameObject.SetActive(false);
    }
    public bool IsThisItemBuy() => costumeItemData.unlocked;
    public bool IsThisItemUsing() => costumeItemData.isUsing;
    public CostumePiece GetThisCostumeType() => costumePieece;

}
