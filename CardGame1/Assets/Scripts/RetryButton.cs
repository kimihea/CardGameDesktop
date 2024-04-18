using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void DifficalitySet(int d)
    {
        PlayerPrefs.SetInt("Difficality", d);   
        //"Difficality"라는 PlayerPrefs의 아이디를 통해 d라는 데이터를 저장

        Retry();
    }
}
