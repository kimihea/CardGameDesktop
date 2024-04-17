using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    public GameObject levelImage;
    public void loadLevel() //레벨선택단계로 가기
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StartScene");
        PlayerPrefs.SetInt("LoadStage", 1); //levelImage 활성화 로직 GameManagerStart에 있음
    }
    public void loadMenu() //메뉴선택단계로가기
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StartScene");
        PlayerPrefs.SetInt("LoadStage", 0);
    }
    public void goLevel()           //시작창에서 스테이지 켜줄때 사용
    {
        PlayerPrefs.SetInt("LoadStage", 1);
    }
    public void setEasy()           //이지 변수값 설정
    {
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("cardNumber", 12);
    }
    public void setNormal()         //노말 변수값 설정
    {
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("cardNumber", 20);
    }
    public void setHard()           //하드 변수값 설정
    {
        PlayerPrefs.SetInt("level", 2);
        PlayerPrefs.SetInt("cardNumber", 20);
    }
    public void newGame()           //bestStage 
    {
        PlayerPrefs.SetInt("bestStage", 0);
    }
    public void killMenu() //스테이지창 꺼주기
    {
        PlayerPrefs.SetInt("LoadStage", 0);
    }
}
