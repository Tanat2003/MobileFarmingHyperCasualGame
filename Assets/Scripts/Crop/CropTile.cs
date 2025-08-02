using System.Collections;
using UnityEngine;
using System;

public class CropTile : MonoBehaviour
{
    //�ת1���͡����1�ŧ
    public TileFieldState state;

    [Header("Elements")]
    [SerializeField] private Transform cropParent; //���˹觷��ת���
    [SerializeField] private MeshRenderer tileRenderer;
    private Crop crop;
    private CropData cropData;

    [Header("Event")]
    public static Action<CropType> onCropHarvested;

    private void Start()
    {
        state = TileFieldState.Empty;
    }
    public bool IsEmpty() => state == TileFieldState.Empty;


    
    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;

        //Quaternion������ʹ������Data���Rotation�ͧ�����obj������
        crop =
            Instantiate(cropData.prefab, transform.position, Quaternion.identity, cropParent);
        this.cropData = cropData;

    }

    public bool IsSown() => state == TileFieldState.Sown;

    public void Water()
    {
        state = TileFieldState.Watered;
        
        tileRenderer.gameObject.LeanColor(Color.white * .5f, 1);
        crop.ScaleUp();
       

    }

    public void Harvest()
    {
        state = TileFieldState.Empty;
        tileRenderer.gameObject.LeanColor(Color.white, 1);
        crop.ScaleDown();
        onCropHarvested?.Invoke(cropData.type);
    }


}
