using System;
using UnityEngine;
using UnityEngine.UI;



public class AppleTree_Manager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Slider treeSlider;
    

    [Header("Settings")]
    private AppleTree lastTriggerAppleTree;

    [Header("Actions")]
    public static Action<AppleTree> onTreeModeStart;
    public static Action onTreeModeEnded; 

    private void Awake()
    {
        Player_DetectChunk.onEnterTreeZone += EnterTreeZoneCallback;
    }

    private void OnDestroy()
    {
        Player_DetectChunk.onEnterTreeZone -= EnterTreeZoneCallback;
    }
    private void EnterTreeZoneCallback(AppleTree tree)
    {
        lastTriggerAppleTree = tree;
    }

    public void TreeButtonCallBack()
    {

        Handheld.Vibrate();

        if (!lastTriggerAppleTree.IsReady())
            return;
        lastTriggerAppleTree.Initialize(this);


        onTreeModeStart?.Invoke(lastTriggerAppleTree);
        //Initialize slider
        UpdateShakeSlider(0);

        

    }
    public void UpdateShakeSlider(float value)
    {

        treeSlider.value = value;
    }

    public void EndedTreeMode()
    {
        onTreeModeEnded?.Invoke();

    }
}
