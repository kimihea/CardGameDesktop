using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerStart : MonoBehaviour
{
    public GameObject normalLevelLock; //노말잠금
    public GameObject hardLevelLock;   //하드잠금
    public GameObject levelImage;      //level창 오브젝트
    int maxLevel;
    void Start()
    {
        
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("LoadStage") == 1) //LoadStage이 1일때 levelImage활성, 게임 종료후 LoadLevel이 1이면 게임 다시킬시 스테이지가 먼저나옴 어떻게 해결?
        {                                         //->스테이지창 닫는 버튼 추가
            levelImage.SetActive(true);
        }
        else
        {
            levelImage.SetActive(false);
        }

        if(PlayerPrefs.GetInt("bestStage") >= 1)   //bestStage에 따라 잠금해제 쉬움(기본)0, 1->노말 2->하드
        {
            normalLevelLock.SetActive(false);
            if (PlayerPrefs.GetInt("bestStage") >= 2) //이중if문
            {
                hardLevelLock.SetActive(false);
            }
        }
        else
        {                                       //bestStage가 0일때 락 다시 활성화
            normalLevelLock.SetActive(true);  
            hardLevelLock.SetActive(true);
        }
    }
}
