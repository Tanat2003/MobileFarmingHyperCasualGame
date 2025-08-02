using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DetectChunk : MonoBehaviour
{
    [Header("Actions")]
    public static Action<AppleTree> onEnterTreeZone;
    public static Action<AppleTree> onExitTreeZone;


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("ChunkTrigger"))
        {
            Chunk chunk = other.GetComponentInParent<Chunk>();
            chunk.TryUnlock();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out AppleTree tree))
            TriggerAppleTree(tree);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AppleTree tree))
            ExitAppleTree(tree);



    }

    private void ExitAppleTree(AppleTree tree)
    {
       onExitTreeZone?.Invoke(tree);
    }

    private void TriggerAppleTree(AppleTree tree)
    {
        onEnterTreeZone?.Invoke(tree);
    }
}
