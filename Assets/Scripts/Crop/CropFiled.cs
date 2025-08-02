using System;
using System.Collections.Generic;
using UnityEngine;

public class CropFiled : MonoBehaviour
{
    //���1�ŧ
    [Header("Elements")]
    [SerializeField] private Transform tileParent;
    private List<CropTile> cropTiles = new List<CropTile>();
    private int tileSown;
    private int tilesWatered;
    private int tileHarvested;
    [Header("Setting")]
    [SerializeField] private CropData cropData; //�ת����١��������
    private TileFieldState state;
    

    [Header("Actions")]
    public static Action<CropFiled> onFullySown;
    public static Action<CropFiled> onFullyWatered;
    public static Action<CropFiled> onFullyHarvested;
    private void Start()
    {
        state = TileFieldState.Empty;
        StoreTile();
        
    }


    #region StoreTile&GetClosetTile
    private void StoreTile()
    {
        for (int i = 0; i < tileParent.childCount; i++)
        {
            cropTiles.Add(tileParent.GetChild(i).GetComponent<CropTile>());
        }
    }
    private CropTile GetClosestCropTile(Vector3 seedPosition)
    {
        //��CropTile�������ش���ԧ�ҧ���зҧ croptile �֧ ����
        float minDistance = 5000;
        int closestCropTileIndex = -1;
        for (int i = 0; i < cropTiles.Count; i++)
        {
            CropTile cropTile = cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position, seedPosition);

            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }

        }
        if (closestCropTileIndex == -1)
            
            return null;
        return cropTiles[closestCropTileIndex];

    }
    #endregion


    #region SowMethod
    public void SeedCollidedCallback(Vector3[] seedPosition) //��CropTile�������ش����sow
    {

        for (int i = 0; i < seedPosition.Length; i++)
        {

            CropTile closestCropTile = GetClosestCropTile(seedPosition[i]);
            if (closestCropTile == null)
                continue;
            if (!closestCropTile.IsEmpty())
            {
                continue;
            }
            Sow(closestCropTile);

        }

    }
    private void Sow(CropTile cropTile)
    {
        cropTile.Sow(cropData);
        tileSown++;
        if (tileSown == cropTiles.Count)
        {
            FieldFullySow();
            onFullySown?.Invoke(this); //�������Action������¶����������¡���CropField���

        }



    }

    private void FieldFullySow() //Field����١�������
    {
        state = TileFieldState.Sown;
    }
    #endregion

    #region WaterMethod
    public void WaterCollidedCallback(Vector3[] waterPosition)
    {
        for (int i = 0; i < waterPosition.Length; i++)
        {
            CropTile closestTile = GetClosestCropTile(waterPosition[i]);
            if (closestTile == null)
                continue;
            if (!closestTile.IsSown())
                continue;
            Water(closestTile);

        }
    }

    private void Water(CropTile cropTile)
    {
        cropTile.Water();
        tilesWatered++;
        if (tilesWatered == cropTiles.Count)
        {
            FieldFullyWatered();
        }
    }

    private void FieldFullyWatered()
    {
        state = TileFieldState.Watered;

        onFullyWatered?.Invoke(this);

    }
    #endregion


    #region AutoSow&AutoWater
    [NaughtyAttributes.Button]
    public void AutoFullySowCropFiled()
    {
        for (int i = 0; i < cropTiles.Count; i++)
            Sow(cropTiles[i]);
    }
    [NaughtyAttributes.Button]
    private void AutoWaterCropField()
    {
        for(int i = 0;i < cropTiles.Count;i++)
            Water(cropTiles[i]);
    }
    #endregion


    #region ReturnCropFieldStateMethod
    public bool IsEmpty() => state == TileFieldState.Empty;

    public bool IsSown() => state == TileFieldState.Sown;
    public bool IsWatered() => state == TileFieldState.Watered;
    #endregion

    #region HarvestMethod
    public void Harvest(Transform harvestRadius)
    {
        float sphereRadius = harvestRadius.localScale.x;
        //loop��CropTile���������Empty�����ⴹHarvest���� Continue ���ǡ��红��������зҧ�����ҧharvestRadius�֧CropTile
        //������зҧ����纹��¡���harvestRadius ������������CropTile���
        
        for (int i = 0; i <  cropTiles.Count; i++)
        {
            if(cropTiles[i].IsEmpty())
                continue;
            float distanceCropTileSphere = 
                Vector3.Distance(harvestRadius.position, cropTiles[i].transform.position);
            if(distanceCropTileSphere <= sphereRadius)
                HarvestTile(cropTiles[i]);
            

        }
    }

    private void HarvestTile(CropTile cropTile)
    {
        cropTile.Harvest();

        tileHarvested++;
        if (tileHarvested == cropTiles.Count)
            FieldFullyHarvested();
    }
    private void FieldFullyHarvested()
    {

        tileHarvested = 0;
        tileSown = 0;
        tilesWatered = 0;
        

        state = TileFieldState.Empty;

        onFullyHarvested?.Invoke(this);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 5);

        Gizmos.color = new Color(0,0,0,0);
        Gizmos.DrawCube(transform.position, Vector3.one * 5);

    }
}
