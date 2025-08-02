using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CropData",menuName ="Scriptable Objects/Crop Data")]
public class CropData : ScriptableObject
{

    [Header("Setting")]
    public Crop prefab;
    public CropType type;
    public Sprite icon;
    public int price;

}
