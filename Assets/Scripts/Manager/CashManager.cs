using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CashManager : MonoBehaviour
{
    // We use method that have GameObbject[] parameters for Other gamescence that's have it own CashContainer
    public static CashManager Instance;

    [Header("Settings")]
    private int coins;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadData();
       
        
    }



    #region Add&Reset&DecreaseCoinMethod

    [NaughtyAttributes.Button]
    private void ResetCoins()
    {
        coins = 0;
        SaveData();
    }

    [NaughtyAttributes.Button]
    private void Add500Coins()
    {
        AddCoins(500);
        SaveData();
    }
    public void DecreaseCoins(int amount)
    {
        AddCoins(-amount);
    }


    public void DecreaseCoinsOnOtherScene(int amount, GameObject[] coinContainers)
    {
        Debug.Log("Decrease " + amount + " Coins");
        AddCoins(-amount);
        UpdataeCoinContainers(coinContainers);
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        SaveData();
    }

    public void AddCoins(int amount, GameObject[] coinContainers)
    {
        coins += amount;
        SaveData(coinContainers);
    }
    #endregion


    #region Load&SaveMethod
    private void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins");
        UpdataeCoinContainers();
    }
    public void LoadData(GameObject[] coinContainers)
    {
        coins = PlayerPrefs.GetInt("Coins");
        UpdataeCoinContainers(coinContainers);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins",coins);
        UpdataeCoinContainers();
    }
    public  void SaveData(GameObject[] coinContainers)
    {
        PlayerPrefs.SetInt("Coins", coins);
        UpdataeCoinContainers();
    }
    #endregion

    private void UpdataeCoinContainers()
    {
        GameObject[] coinsContainers = GameObject.FindGameObjectsWithTag("CoinAmount");
        
        foreach(GameObject coinContainer in coinsContainers)
        {
            coinContainer.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        }



    }
    public void UpdataeCoinContainers(GameObject[] coinContainers)
    {
       

        foreach (GameObject coinContainer in coinContainers)
        {
            coinContainer.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        }



    }
    public int GetCoin() => coins;
    
}
