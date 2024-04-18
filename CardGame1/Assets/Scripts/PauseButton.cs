using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.pauseMusic();
    }
}
