using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CropInteract : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Material[] materials;


    private void Update()
    {
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("_PlayerPosition",transform.position);
        }
    }
}
