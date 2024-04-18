using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip audioStart;
    public AudioClip audioWarning;

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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.Play();
    }

    public void StartButton()   //버튼컴포넌트 연결
    {
        audioSource.PlayOneShot(audioStart);
    }

    public void PlayAudioWarning()
    {
        audioSource.PlayOneShot(audioWarning);
    }
}
