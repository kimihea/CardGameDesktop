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

        if (time > 25.0f && !isWarnPlayed) // 25�ʰ� �Ѿ�� ���������� ��ȯ
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
            // �ı��ض�.
            audioSource.PlayOneShot(clip);
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;

            resultTxt.text = "<color=#5070f9></color>";
            switch (secondCard.idx)
            {            //  idx ���ڸ��� text�� ���� ����    
                case 1:
                case 2:
                    resultTxt.text = "����ȯ";
                    break;
                case 3:
                case 4:
                    resultTxt.text = "Ź����";
                    break;
                case 5:
                case 6:
                    resultTxt.text = "����";
                    break;
                case 7:
                case 8:
                    resultTxt.text = "������";
                    break;
                case 9:
                case 10:
                    resultTxt.text = "�ڵ���";
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
            // �ݾƶ�.
            firstCard.CloseCard();
            secondCard.CloseCard();
            resultTxt.text = "<color=#FF0000>����</color>";
            audioSource.PlayOneShot(audioClipFail);
        }
        resultTxt.gameObject.SetActive(true);
        StartCoroutine(SetActiveFalse());           //SetActiveFalse�ڷ�ƾ ����

        flipCount++;
        firstCard = null;
        secondCard = null;
    }

    IEnumerator SetActiveFalse()             //Coroutine�� �̿��� ����ó��
    {
        yield return new WaitForSeconds(0.3f);  //0.3���� �ؽ�Ʈ ��Ȱ��ȭ
        resultTxt.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        StageSelect stage = stageSelect.GetComponent<StageSelect>();
        stage.ApplyScore(flipCount);
    }
}

