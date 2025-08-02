using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject treeButton;
    [SerializeField] private GameObject toolButtonContainer;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject treeModePanel;

    private void Awake()
    {
        Player_DetectChunk.onEnterTreeZone += EnterTreeZoneCallback;
        Player_DetectChunk.onExitTreeZone += ExitTreeZoneCallback;
        AppleTree_Manager.onTreeModeEnded += SetGameMode;
        AppleTree_Manager.onTreeModeStart += SetTreeMode;
    }
    private void Start()
    {
        SetGameMode();
    }
    private void OnDestroy()
    {
        Player_DetectChunk.onEnterTreeZone -= EnterTreeZoneCallback;
        Player_DetectChunk.onExitTreeZone -= ExitTreeZoneCallback;
        AppleTree_Manager.onTreeModeEnded -= SetGameMode;

        AppleTree_Manager.onTreeModeStart -= SetTreeMode;
    }
    private void EnterTreeZoneCallback(AppleTree tree)
    {
        treeButton.SetActive(true);
        toolButtonContainer.SetActive(false);
    }
    private void ExitTreeZoneCallback(AppleTree tree)
    {
        treeButton.SetActive(false);
        toolButtonContainer.SetActive(true);

    }

    private void SetGameMode()
    {
        treeModePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    private void SetTreeMode(AppleTree tree)
    {
        gamePanel.SetActive(false);
        treeModePanel.SetActive(true);
    }
   
}
