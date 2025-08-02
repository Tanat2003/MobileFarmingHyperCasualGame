using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumeNextAndPreviousHandle_UI : MonoBehaviour
{
    [Header("Hat Button")]
    [SerializeField] private GameObject hatParent;
    [SerializeField] private Button hat_NextButton;
    [SerializeField] private Button hat_PreviousButton;
    [SerializeField] private List<CostumeItem> hatItems;

    private int currentHatIndex;

    [Space]
    [Header("Top Button")]
    [SerializeField] private GameObject topParent;
    [SerializeField] private Button top_NextButton;
    [SerializeField] private Button top_PreviousButton;
    [SerializeField] private List<CostumeItem> topItems;
    private int currentTopIndex;

    [Space]
    [Header("Buttom Button")]
    [SerializeField] private GameObject buttomParent;
    [SerializeField] private Button buttom_NextButton;
    [SerializeField] private Button buttom_PreviousButton;
    [SerializeField] private List<CostumeItem> buttomItems;

    [Space]
    [Header("PlayerCharactor")]
    [SerializeField] private GameObject playerCharactor;
    private CostumeItem[] allItem;
    private int currentButtomIndex;

    private bool previousBool = false;

    private void Awake()
    {
        allItem = playerCharactor.GetComponentsInChildren<CostumeItem>(true);

        AddingMethodToAllButton();

    }
    private void Start()
    {

        AddingAllICostumeItemToList();
        currentHatIndex = 0;
        currentTopIndex = 0;
        currentButtomIndex = 0;

        hatItems[0].gameObject.SetActive(true);




    }
    private void AddingAllICostumeItemToList()
    {
        hatItems.Clear();
        topItems.Clear();
        buttomItems.Clear();



        foreach (CostumeItem item in allItem)
        {
            item.UpdatePlayerSelectedItem(false); //unSelected every item
            switch (item.GetThisCostumeType())
            {

                case CostumePiece.Hat:
                    hatItems.Add(item);
                    
                    break;
                case CostumePiece.Top:
                    topItems.Add(item);
                    
                    break;
                case CostumePiece.Buttom:
                    buttomItems.Add(item);
                    
                    break;
            }
        }
    }




    private void TurnOffEveryModel()
    {
        foreach (CostumeItem item in hatItems)
        {
            item.gameObject.SetActive(false);
        }
        foreach (CostumeItem item in topItems)
        {
            item.gameObject.SetActive(false);
        }
        foreach (CostumeItem item in buttomItems)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void ShowingNextOrPreviousItem(bool isNext, CostumePiece piece)
    {


        switch (piece)
        {
            case CostumePiece.Hat:
                ShowItem(hatItems, ref currentHatIndex, isNext);
                break;
            case CostumePiece.Top:
                ShowItem(topItems, ref currentTopIndex, isNext);
                break;
            case CostumePiece.Buttom:
                ShowItem(buttomItems, ref currentButtomIndex, isNext);
                break;
        }
    }

    private void ShowItem(List<CostumeItem> items, ref int index, bool isNext)
    {
        items[index].gameObject.SetActive(false);
        items[index].UpdatePlayerSelectedItem(false); //Update for previous Item

        //Handle The index
        if (isNext)
        {
            index = (index + 1) % items.Count;


        }
        else
        {
            index = (index - 1 + items.Count) % items.Count;

        }


        //Show Current Item
        items[index].gameObject.SetActive(true);

        //If not buy
        if (!items[index].IsThisItemBuy())
            items[index].ShowBuyButton();

        //If Already buy
        else
        {
            items[index].HideBuyButton();
            items[index].UpdatePlayerSelectedItem(true);

        }
    }

    private void AddingMethodToAllButton()
    {
        hat_NextButton.onClick.AddListener(() =>
        {
            ShowingNextOrPreviousItem(true, CostumePiece.Hat);
        });
        hat_PreviousButton.onClick.AddListener(() =>
        {
            ShowingNextOrPreviousItem(false, CostumePiece.Hat);
        });
        top_NextButton.onClick.AddListener(() =>
        {
            ShowingNextOrPreviousItem(true, CostumePiece.Top);


        });
        top_PreviousButton.onClick.AddListener(() =>
        {
            ShowingNextOrPreviousItem(false, CostumePiece.Top);

        });
        buttom_NextButton.onClick.AddListener(() =>
        {
            ShowingNextOrPreviousItem(true, CostumePiece.Buttom);


        });
        buttom_PreviousButton.onClick.AddListener(() =>
        {
            ShowingNextOrPreviousItem(false, CostumePiece.Buttom);


        });
    }
}
