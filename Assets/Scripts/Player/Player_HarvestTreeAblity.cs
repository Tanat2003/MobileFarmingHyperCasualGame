using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HarvestTreeAblity : MonoBehaviour
{
    [Header("Elements")]
    private AppleTree currentTree;
    private Player_Animator playerAnimator;
    private bool isActive;


    [Header("Setting")]
    [SerializeField] private float distanceToTree;
    private Vector2 previousMousePosition;
    private bool isShaking;
    [Range(0f, 1f)]
    [SerializeField] private float shakeThreshold; //ให้หน้าจอเริ่มสั่นตอนไหน
    private void Awake()
    {
        AppleTree_Manager.onTreeModeStart += TreeModeStartCallback;
        playerAnimator = GetComponent<Player_Animator>();
        AppleTree_Manager.onTreeModeEnded += TreeModeEndedCallback;
    }


    private void Update()
    {
        if(isActive && !isShaking)
            ManageTreeShaking();
    }
    private void OnDestroy()
    {
        AppleTree_Manager.onTreeModeStart -= TreeModeStartCallback;
        AppleTree_Manager.onTreeModeEnded -= TreeModeEndedCallback;

    }

    private void TreeModeStartCallback(AppleTree tree)
    {
        currentTree = tree;
        isActive = true;
        MoveToWardTree();
    }

    private void MoveToWardTree()
    {
        Vector3 treePos = currentTree.transform.position;
        Vector3 dir =transform.position - treePos;

        Vector3 flatDir = dir;

        flatDir.y = 0;

        Vector3 targetPosition = treePos + flatDir.normalized*distanceToTree;

        playerAnimator.ManageAnimations(-flatDir);

        LeanTween.move(gameObject, targetPosition,.5f);
    }
    private void ManageTreeShaking()
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButton(0))
        {
            currentTree.StopShaking();  
            return;
        }

        float shakeMagnitude = Vector2.Distance(Input.mousePosition, previousMousePosition);
        if (ShouldShake(shakeMagnitude))
            Shake();
        else
            currentTree.StopShaking();

        previousMousePosition = Input.mousePosition;


#else
         if(Input.touchCount == 0)
        {
            currentTree.StopShaking();
            return;
        }

        float shakeMagnitudeMobile = Vector2.Distance(Input.GetTouch(0).position, previousMousePosition);
        if (ShouldShake(shakeMagnitudeMobile))
            Shake();
        else
            currentTree.StopShaking();

        previousMousePosition = Input.GetTouch(0).position;

#endif
    }

    private bool ShouldShake(float shakeMagnitude)
    {
        float screenPercent = shakeMagnitude / Screen.width;
        return screenPercent >= shakeThreshold;

    }

    private void Shake()
    {
        isShaking = true;
        currentTree.Shake();
        playerAnimator.PlayShakeTreeAnimation();
        LeanTween.delayedCall(.1f, ()=> isShaking = false); //isshaking = false หลังจากรอ.5fวิ
    }
    private void TreeModeEndedCallback()
    {
        currentTree = null;
        isActive = false;
        isShaking = false;
        LeanTween.delayedCall(.1f, () => playerAnimator.StopShakeTreeAnimation());
    }
}
