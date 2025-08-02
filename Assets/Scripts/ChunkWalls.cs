using UnityEngine;

public class ChunkWalls : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject leftWall;
    public void Configure(int configuration)
    {
        frontWall.SetActive(IsKthBitSet(configuration, 0));
        rightWall.SetActive(IsKthBitSet(configuration, 0));
        backWall.SetActive(IsKthBitSet(configuration, 0));
        leftWall.SetActive(IsKthBitSet(configuration, 0));

        

    }

    public bool IsKthBitSet(int configuration,int index)
    {
        if ((configuration & (1 << index)) > 0)
            return false;
        else
            return true;
    }
}
