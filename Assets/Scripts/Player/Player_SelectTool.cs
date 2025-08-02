using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player_SelectTool : MonoBehaviour
{
    public enum Tool { None, Sow, Water, Harvest }
    private Tool activetool;

    [Header("Elements")]
    [SerializeField] private Image[] toolImages;

    [Header("Setting")]
    [SerializeField] private Color toolSelectedColor;

    [Header("Actions")]
    public static Action<Tool> onToolSelected;

    private void Start()
    {
        SelectTool(0);
    }
    public void SelectTool(int toolIndex)
    {
        activetool = (Tool)toolIndex; // Enum(index)
        for (int i = 0; i < toolImages.Length; i++)
            toolImages[i].color = i == toolIndex ? toolSelectedColor : Color.white;

        
        onToolSelected?.Invoke(activetool);
    }
    #region CanDothingMethod
    public bool CanSow() => activetool == Tool.Sow;
    public bool CanWater() => activetool == Tool.Water;
    public bool CanHarvest() => activetool == Tool.Harvest;
    #endregion
}
