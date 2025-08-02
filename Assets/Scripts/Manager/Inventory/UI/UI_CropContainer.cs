using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CropContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image cropIcon;
    [SerializeField] private TextMeshProUGUI amountText;


    public void Configue(Sprite icon,int amount) //‰«È· ¥ß¢ÈÕ¡Ÿ≈„πCropContainer
    {
        cropIcon.sprite = icon;
        amountText.text = amount.ToString();



    }
    public void UpdateDisplay(int amount)
    {
        amountText.text = amount.ToString();
    }

}
