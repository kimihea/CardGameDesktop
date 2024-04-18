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
    public Text scoreTxt;
    public Text bestTimeTxt;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip audioClipFail;

    public GameObject stageSelect;

    public int cardCount = 0;
    float time = 0.0f;
    float countTime = 0.0f;

    bool isPlaying = true;
    bool isWarnPlayed = false;
    bool firstSelected = false;

    float firstSelectedTime = 0.0f;

    int flipCount = 0;

    float timePenalty = 0f;

    int nowDiff = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        GetDiff();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        isPlaying = true;
        
        bestTimeTxt.text = PlayerPrefs.GetFloat("BestTime" + nowDiff).ToString("N2");
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying) 
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }

        if (time > countTime && isPlaying)
        {
            endTxt.SetActive(true);
            stageSelect.SetActive(true);
            Time.timeScale = 0.0f;
            isPlaying = false;

            EndGame();
        }

        if (time > countTime - 5.0f && !isWarnPlayed) // 25초가 넘어가면 빨간색으로 전환
        {
            timeTxt.color = new Color(255f, 0f, 0f, 255f);
            AudioManager.Instance.PlayAudioWarning();
            isWarnPlayed = true;
        }

        if (time - firstSelectedTime > 5 && firstSelected)
        {
            firstCard.CloseCard();
            firstCard = null;
            firstSelected = false;
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            firstSelected = false;
            // 파괴해라.
            audioSource.PlayOneShot(clip);
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;

            resultTxt.text = "<color=#5070f9></color>";
            switch (secondCard.idx)
            {            //  idx 숫자마다 text의 내용 변경    
                case 0:
                case 1:
                    resultTxt.text = "김희환";
                    break;
                case 2:
                case 3:
                    resultTxt.text = "탁혁재";
                    break;
                case 4:
                case 5:
                    resultTxt.text = "고예준";
                    break;
                case 6:
                case 7:
                    resultTxt.text = "문병준";
                    break;
                case 8:
                case 9:
                    resultTxt.text = "박도현";
                    break;
            }

            if (cardCount == 0)
            {
                endTxt.SetActive(true);
                stageSelect.SetActive(true);
                Time.timeScale = 0.0f;
                isPlaying = false;
                EndGame();

                if(PlayerPrefs.HasKey("BestTime" + nowDiff))
                {
                    if(PlayerPrefs.GetFloat("BestTime"+nowDiff) > time)
                    {
                        PlayerPrefs.SetFloat("BestTime" + nowDiff, time);
                    }
                }
                else
                {
                    PlayerPrefs.SetFloat("BestTime" + nowDiff, time);
                }
            }

        }
        else
        {
            time += timePenalty;
            // 닫아라.
            firstCard.CloseCard();
            secondCard.CloseCard();
            resultTxt.text = "<color=#FF0000>실패</color>";
            audioSource.PlayOneShot(audioClipFail);
        }
        resultTxt.gameObject.SetActive(true);
        StartCoroutine(SetActiveFalse());           //SetActiveFalse코루틴 실행

        flipCount++;
        firstCard = null;
        secondCard = null;
    }

    IEnumerator SetActiveFalse()             //Coroutine을 이용한 지연처리
    {
        yield return new WaitForSeconds(0.3f);  //0.3초후 텍스트 비활성화
        resultTxt.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        StageSelect stage = stageSelect.GetComponent<StageSelect>();
        stage.ApplyScore(flipCount);
        Rank();
        scoreTxt.text = (-(cardCount - 20) + (countTime - (int)time) - flipCount).ToString();
    }

    public void SetFirstCardTime()
    {
        firstSelectedTime = time;
        firstSelected = true;
    }

    void Rank()
    {
        //추가적으로 난이도를 클리어했는지 검사
        if (PlayerPrefs.HasKey("isClear"))
        {
            int getDiff = PlayerPrefs.GetInt("isClear");
            if ((nowDiff >= getDiff)) //현재 난이도가 isClear의 있는 난이도보다 높으면
                PlayerPrefs.SetInt("isClear", nowDiff);
        }
        else
        {
            PlayerPrefs.SetInt("isClear", nowDiff);
        }
    }
    void GetDiff()  //난이도 설정
    {
        if (PlayerPrefs.HasKey("Difficality"))
        {
            nowDiff = PlayerPrefs.GetInt("Difficality");
        }

        switch (nowDiff)
        {
            case 1:
                countTime = 60f;
                break;
            case 2:
                countTime = 60f;
                timePenalty = 0.5f;
                break;
            case 3:
                countTime = 45f;
                timePenalty = 0.5f;
                break;
        }
    }
}

