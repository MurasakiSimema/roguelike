using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] BGM;
    private AudioSource currentBGM;

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

    public void PlayBGM(int idx)
    {
        foreach (var bgm in BGM)
            bgm.Stop();

        BGM[idx].Play();
    }

    public void PlayGameOver()
    {
        PlayBGM(2);
    }

    public void PlayVictory()
    {
        PlayBGM(1);
    }
}
