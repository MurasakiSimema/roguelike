using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource victory;
    public AudioSource gameOver;
    public AudioSource[] BGM;

    public AudioSource[] SFX;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(int idx)
    {
        if (idx < SFX.Length)
        {
            SFX[idx].Stop();
            SFX[idx].Play();
        }
        else
            Debug.LogError("SFX N° " + idx + " don't exist");
    }
    public void PlayBGM(int idx)
    {
        StopBGM();

        if (idx < BGM.Length)
            BGM[idx].Play();
        else
            Debug.LogError("BGM N° " + idx + " don't exist");
    }
    public void StopBGM()
    {
        foreach (var bgm in BGM)
            bgm.Stop();
    }
    public void PlayGameOver()
    {
        StopBGM();
        gameOver.Play();
    }

    public void PlayVictory()
    {
        StopBGM();
        victory.Play();
    }
}
