using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private AudioSource[] sfxsound;
    private bool playBGM;
    private int bgmindex;


    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        PlayBGM(Random.Range(0, bgm.Length));
    }
    private void Update()
    {
        if (playBGM == false && BGMIsPlaying())
        {
            StopAllBGM();
        }
        if (playBGM && bgm[bgmindex].isPlaying == false)
        {
            PlayBGM(Random.Range(0, bgm.Length));
        }
    }
    public void PlaySFX(int index)
    {
       
        sfxsound[index].Play();

    }
    public void PlayBGM(int index)
    {
        if (bgm[index] == null)
            return;
        StopAllBGM();

        bgmindex = index;
        playBGM = true;
        bgm[index].Play();
    }

    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    private bool BGMIsPlaying()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i].isPlaying)
                return true;
        }
        return false;
    }
}
