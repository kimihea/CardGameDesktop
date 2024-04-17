using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public Card firstCard;
    public Card secondCard;

    static float time;
    int flipCount = 0;

    public GameObject EndPanel;
    public GameObject PausePanel;
    public Text TimeTxt;
    public Text finalScoreTxt;
    public Text ResultTxt; //결과텍스트 추가
    public Text flipCountTxt;

    AudioSource audioSource;
    public AudioClip audioclip;
    public AudioClip audioClipFail;
    public AudioClip audioClipWarning;


    bool warning = true;
    // Start is called before the first frame update
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        time = 0.0f;
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        TimeTxt.text = time.ToString("N2");

        if ((time > 60.0f) & (PlayerPrefs.GetInt("level") != 2))
        {
            Time.timeScale = 0.0f;
            flipCountTxt.text = flipCount.ToString();
            finalScoreTxt.text = "0";
            Invoke("Stage", 1.0f);
            EndPanel.SetActive(true);
        }
        else if ((PlayerPrefs.GetInt("level") == 2) & (time > 45.0f))
        {
            Time.timeScale = 0.0f;
            flipCountTxt.text = flipCount.ToString();
            finalScoreTxt.text = "0";
            Invoke("Stage", 1.0f);
            EndPanel.SetActive(true);
        }

        if (time > 25.0f & warning) // 25초일때  빨간색으로 전환 
        {
            TimeTxt.color = new Color(255f, 0f, 0f, 255f);
            audioSource.PlayOneShot(audioClipWarning);
            warning = false;
        }

    }
    public void Matched()       //카드 두장이 뒤집혔을 때 실행
    {
        if (firstCard.idx == secondCard.idx)  //카드 두장이 동일
        {
            audioSource.volume = 1f;
            audioSource.PlayOneShot(audioclip);
            firstCard.DestroyCard();            //카드삭제
            secondCard.DestroyCard();
            ResultTxt.text = "<color=#5070f9></color>";
            switch (secondCard.idx)
            {            //  idx 숫자마다 text의 내용 변경    
                case 0:
                case 1:
                    ResultTxt.text = "김희환";
                    break;
                case 2:
                case 3:
                    ResultTxt.text = "탁혁재";
                    break;
                case 4:
                case 5:
                    ResultTxt.text = "고예준";
                    break;
                case 6:
                case 7:
                    ResultTxt.text = "문병준";
                    break;
                case 8:
                case 9:
                    ResultTxt.text = "박도현";
                    break;

            }//이름출력하는 case문

            PlayerPrefs.SetInt("cardNumber", PlayerPrefs.GetInt("cardNumber") - 2); //총카드수 PlayerPrefs.GetInt("cardNumber") 에서 맞출 때 마다 2씩 뺀다
            if (PlayerPrefs.GetInt("cardNumber") == 0)
            {
                Invoke("GameEnd", 0.0f);
            }
        }
        else                            //카드 두장이 다를 때
        {
            if (PlayerPrefs.GetInt("level") != 0) //easy난이도가 아니라면
            { time += 0.5f; }
            audioSource.volume = 0.2f;
            audioSource.PlayOneShot(audioClipFail);
            firstCard.CloseCard();              //카드 뒤편으로 뒤집기
            secondCard.CloseCard();
            ResultTxt.text = "<color=#FF0000>실패</color>";


        }
        //공통실행부분
        flipCount++;
        ResultTxt.gameObject.SetActive(true); //맞았는지 틀렸는지 체크 맞다면 이름, 틀리면 실패
        StartCoroutine(SetActiveFalse());
        firstCard = null;
        secondCard = null;

    }
    IEnumerator SetActiveFalse()             //0.3초후에 결과창 꺼주는Coroutine을 이용한 지연처리
    {
        yield return new WaitForSeconds(0.3f);  //0.3초후 텍스트 비활성화
        ResultTxt.gameObject.SetActive(false);
    }
    public void GameEnd() //한 스테이지 성공했을때 
    {
        Time.timeScale = 0.0f;
        int defaultScore = 0; //주어진기본점수
        float givenTime = 0f; //주어진시간
        int cardNumber = PlayerPrefs.GetInt("cardNumber"); // 총카드수
        flipCountTxt.text = flipCount.ToString(); //뒤집은 횟수 
        if (PlayerPrefs.GetInt("level") == 0)   //이지난이도
        {
            defaultScore = 30;
            givenTime = 60.0f;
        }
        if (PlayerPrefs.GetInt("level") == 1)//보통난이도
        {
            defaultScore = 50;
            givenTime = 60.0f;
        }
        if (PlayerPrefs.GetInt("level") == 2)//하드난이도
        {
            defaultScore = 100;
            givenTime = 45.0f;
        }
        finalScoreTxt.text = ((defaultScore + (int)(givenTime - time) - PlayerPrefs.GetInt("cardNumber") * 2 - (20 - flipCount)).ToString());
        //최종점수는((기본점수 + (전체시간 - 시간) + (맞춘카드 - 전체카드)) * (배수))-(20-뒤집은 횟수))//
        EndPanel.SetActive(true);       //결과창 활성화
        Invoke("nextStage", 0f);
        Debug.Log(PlayerPrefs.GetInt("level") + 1); //확인용
    }
    public void nextStage() //다음 스테이지로 PlayerPrefs넘기는 함수
    {
        if (finalScoreTxt.text != "0")
        { //최종점수가 0이아니라면 실행 ,스테이지 못 깨면 최종점수가 0이기 때문이다
            int stage = PlayerPrefs.GetInt("bestStage");
            int nextlevel = PlayerPrefs.GetInt("level") + 1;
            if (stage < PlayerPrefs.GetInt("level") | stage == 0) //스테이지가 레벨보다 낮거나 스테이지가 0이면
                PlayerPrefs.SetInt("bestStage", nextlevel);
        }
    }//
    public void keepGoing() //일시정지해제
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
    public void pauseGame() //일시정지
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void stopMusic()
    {
        Music.Instance.stopMusic();
    }
}
//별의미없지만 git commit실험하기 위해 적어둔 주석