using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCostumeData", menuName = "Scriptable Objects/CostumeData")]
public class CostumeItemData : ScriptableObject
{
    public bool unlocked;
    public int price;
    public bool isUsing;
}
