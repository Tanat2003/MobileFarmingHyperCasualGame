using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;
    [Header("Data")]
    [SerializeField] private CropData[] cropData;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetCropSpriteFromCropType(CropType cropType)
    {
        for (int i = 0; i < cropData.Length; i++)
        {
            if (cropData[i].type == cropType)
                return cropData[i].icon;
        }
        Debug.LogError("No CropDaata found that Type");
        return null;
    }
    public  int GetCropPriceFromCropType(CropType cropType)
    {

        for (int i = 0; i < cropData.Length; i++)
        {
            if (cropData[i].type == cropType)
                return cropData[i].price;
        }
        Debug.LogError("No CropDaata found that Type");
        return 0;
    }
}
