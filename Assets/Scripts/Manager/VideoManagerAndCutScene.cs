using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManagerAndCutScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer cutScene;
    [SerializeField] private GameObject allUI;
    public static VideoManagerAndCutScene instance;
    private  bool finishedPlayCutScene;
    private void Start()
    {

        cutScene.loopPointReached += CutScene_loopPointReached;
    }

    private void CutScene_loopPointReached(VideoPlayer source)
    {
        finishedPlayCutScene = true;
    }

    private void Awake()
    {
        instance = this;
      
    }
    public void PlayCutScene(string sceneName)
    {
       allUI.SetActive(false);
        Player_Controller.instance.SetCanPlayerControl(false);
        StartCoroutine(PlayCutSceneCo(sceneName));
        cutScene.Play();

        
        
    }
    private IEnumerator PlayCutSceneCo(string sceneName)
    {
        
        yield return new WaitUntil(()=> finishedPlayCutScene);
        SceneManager.LoadScene(sceneName);
    }
}
