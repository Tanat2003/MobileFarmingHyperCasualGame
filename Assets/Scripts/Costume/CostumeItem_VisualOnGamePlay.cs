using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeItem_VisualOnGamePlay : MonoBehaviour
{

    [Header("Item")]
    [SerializeField] private List<CostumeItem> hatItems;
    [SerializeField] private List <CostumeItem> topItems;
    [SerializeField] private List<CostumeItem> buttomItems;
    [Space]
    [Header("PlayerCharactor")]
    [SerializeField] private GameObject playerCharactor;
    private CostumeItem[] allItems;

    private void Awake()
    {
        allItems = playerCharactor.GetComponentsInChildren<CostumeItem>(true);
        
    }
    private void Start()
    {
        SetActiveForUsingItem();
    }

    private void SetActiveForUsingItem()
    {
        foreach (CostumeItem item in allItems)
        {
            switch (item.IsThisItemUsing())
            {
                case true:
                    item.gameObject.SetActive(true);

                    break;
                case false:
                    item.gameObject.SetActive(false);
                    break;

            }
        }
    }

}
