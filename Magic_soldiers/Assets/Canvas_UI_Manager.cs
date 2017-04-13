using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas_UI_Manager : MonoBehaviour {

    public void OnClickQuit()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        SceneManager.LoadScene("Menu");
    }
}
