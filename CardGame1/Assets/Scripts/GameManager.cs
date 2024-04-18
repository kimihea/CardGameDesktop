using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endTxt;
    public Text resultTxt;
    public Text flipCountTxt;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip audioClipFail;

    public GameObject stageSelect;

    public int cardCount = 0;
    float time = 0.0f;

    bool isPlaying = true;
    bool isWarnPlayed = false;

    int flipCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        time = 23.0f;
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time > 30.0f && isPlaying)
        {
            endTxt.SetActive(true);
            stageSelect.SetActive(true);
            Time.timeScale = 0.0f;
            isPlaying = false;

            EndGame();
        }

        if (time > 25.0f && !isWarnPlayed) // 
        {
            timeTxt.color = new Color(255f, 0f, 0f, 255f);
            AudioManager.Instance.PlayAudioWarning();
            isWarnPlayed = true;
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            
            audioSource.PlayOneShot(clip);
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;

            resultTxt.text = "<color=#5070f9></color>";
            switch (secondCard.idx)
            {            //  
                case 1:
                case 2:
                    resultTxt.text = "김희환";
                    break;
                case 3:
                case 4:
                    resultTxt.text = "탁혁재";
                    break;
                case 5:
                case 6:
                    resultTxt.text = "고예준";
                    break;
                case 7:
                case 8:
                    resultTxt.text = "문병준";
                    break;
                case 9:
                case 10:
                    resultTxt.text = "박도현";
                    break;
            }

            if (cardCount == 0)
            {
                endTxt.SetActive(true);
                stageSelect.SetActive(true);
                Time.timeScale = 0.0f;
                EndGame();
            }

        }
        else
        {
            // 
            firstCard.CloseCard();
            secondCard.CloseCard();
            resultTxt.text = "<color=#FF0000>실패</color>";
            audioSource.PlayOneShot(audioClipFail);
        }
        resultTxt.gameObject.SetActive(true);
        StartCoroutine(SetActiveFalse());           //SetActiveFalse

        flipCount++;
        firstCard = null;
        secondCard = null;
    }

    IEnumerator SetActiveFalse()             //Coroutine
    {
        yield return new WaitForSeconds(0.3f);  //0.3
        resultTxt.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        StageSelect stage = stageSelect.GetComponent<StageSelect>();
        stage.ApplyScore(flipCount);
    }
}

