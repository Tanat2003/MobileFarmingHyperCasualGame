using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CashManagerOnOtherScenes : MonoBehaviour
{
     [SerializeField] private GameObject[] coinContainers;
    [SerializeField] private Button backToGameButton;
    public static UI_CashManagerOnOtherScenes instance;

    private void Awake()
    {
        instance = this;
        backToGameButton.onClick.AddListener(() =>
        {
            VideoManagerAndCutScene.instance.PlayCutScene("GameScenes");
        });
    }
    private void Start()
    {
        CashManager.Instance.LoadData(coinContainers);
    }

    public void DecreaseCoin(int amount)
    {
        CashManager.Instance.DecreaseCoinsOnOtherScene(amount, coinContainers);
    }

    public void AddCoin(int amount)
    {
        CashManager.Instance.AddCoins(amount, coinContainers);
    }


}
