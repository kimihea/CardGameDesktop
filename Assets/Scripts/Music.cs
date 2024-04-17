using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance;
    AudioSource audioSource;
    public AudioClip audioclip;
    public AudioClip audioStart;
    float time;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioclip;
        audioSource.Play();
        audioSource.playOnAwake= true;
        audioSource.loop=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton()
    {
        audioSource.PlayOneShot(audioStart);
    }
    public void stopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }
}
