using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Text flipCountTxt;
    public Text bestTimeTxt;

    public Button normalButton, hardButton;
    public GameObject normalLock, hardLock;

    int isClear = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("isClear"))
            isClear = PlayerPrefs.GetInt("isClear");

        if (isClear >= 2)    //Normal을 클리어하면
        {
            hardButton.interactable = true;
            hardLock.SetActive(false);
        }
        if (isClear >= 1)    //Easy를 클리어하면
        {
            normalButton.interactable = true;
            normalLock.SetActive(false);
        }
    }
    public void ExitMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // 테스트용
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ApplyScore(int count)
    {
        flipCountTxt.text = count.ToString();
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void GotoMenuBtn()
    {
        SceneManager.LoadScene(0);
    }
}
