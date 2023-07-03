using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource BackgroundMusic;
    public AudioSource AudioMusic;
    public AudioClip AudioRocket;
    public GameObject checkSound;
    public GameObject checkMusic;
    public bool isToggle = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        /*else if(Instance != this) 
        {
            Destroy(gameObject);
        }*/
    }
    void Start()
    {
    }
    void Update()
    {
    }
    public void OnSound()
    {
        if (BackgroundMusic.isPlaying)
        {
            checkSound.SetActive(false);
            BackgroundMusic.Pause();
        }
        else
        {
            checkSound.SetActive(true);
            BackgroundMusic.Play();
        }
    }
    public void OnMusic()
    {
        isToggle = !isToggle;
        if (!isToggle && checkMusic)
        {
            checkMusic.SetActive(false);
        }
        else if (isToggle && checkMusic)
        {
            checkMusic.SetActive(true);
        }
    }
}
