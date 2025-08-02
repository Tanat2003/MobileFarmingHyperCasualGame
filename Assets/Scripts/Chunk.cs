using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ChunkWalls))]
public class Chunk : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private MeshFilter chunkFilter;
    private ChunkWalls chunkWalls;

    [Header("Settings")]
    [SerializeField] private int initialPrice; //HowmanyPriceToUnlockChunk
    private int currentPrice;
    private bool unlocked;

    [Header("Action")]
    public static Action onUnlocked;
    public static Action onPriceChanged;

    private void Awake()
    {
        chunkWalls = GetComponent<ChunkWalls>();
    }
    public void TryUnlock()
    {
        if (CashManager.Instance.GetCoin() <= 0)
            return;

        AudioManager.instance.PlaySFX(1);
        currentPrice--;
        CashManager.Instance.DecreaseCoins(1);
        

        onPriceChanged?.Invoke();

        priceText.text = currentPrice.ToString();
        if (currentPrice <= 0)
            Unlock();
       
    }

    private void Unlock(bool triggerAction = true)
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);

        unlocked = true;

        if(triggerAction) //Action àÁ×èÍ¼ØéàÅè¹»Å´ÅçÍ¡Chunk¤ÃÑé§áÃ¡ã¹TryUnlockMethod
            onUnlocked?.Invoke();
    }
    public bool IsUnlocked() => unlocked;

    public int GetInitialPrice()
    {
        return initialPrice;
    }
    public int GetCurrentPrice() => currentPrice;

    public void Initialize(int price)
    {
        
        currentPrice = price;
        priceText.text = currentPrice.ToString();

        if(currentPrice <= 0)
            Unlock(false);



    }

    public void UpdateWalls(int configuration)
    {
        chunkWalls.Configure(configuration);
    }

    public void DisplayLockedElement()
    {
        lockedElements.SetActive(true);

       
    }

    public void SetRenderer(Mesh chunkMesh,int rotation = 0)
    {
        chunkFilter.mesh = chunkMesh;
        chunkFilter.transform.localRotation = Quaternion.Euler(0, rotation, 0);


    }
}
