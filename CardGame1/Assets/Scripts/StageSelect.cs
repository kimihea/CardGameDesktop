using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Text flipCountTxt;
    public Text bestTimeTxt;

    public void ExitMenu()
    {
        gameObject.SetActive(false);
    }

   
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ApplyScore(int count)
    {
        flipCountTxt.text = count.ToString();
    }
}
