using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_UI_Online : MonoBehaviour {

    private Text text;
    private Text text_msg;
    private Text text_sec;
    private Text text_infos;
    private Text text_pause;

    private RawImage healthBar;
    private RawImage shieldBar;

    private GameObject quit;

    private bool isPaused;

    private int compteur;
    private string levelName;

    private string objectifs;
    private string objectifsSecondaire;

    public Canvas_UI_Online(Text text, Text text_msg, Text text_sec, Text text_infos, Text text_pause, RawImage healthBar, RawImage shieldBar, GameObject quit)
    {       
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                compteur = 4;
                levelName = "Level1";
                break;

            default:
                break;
        }

        this.text = text;
        this.text_msg = text_msg;
        this.text_sec = text_sec;
        this.text_infos = text_infos;
        this.text_pause = text_pause;

        this.healthBar = healthBar;
        this.shieldBar = shieldBar;

        this.quit = quit;

        isPaused = false;

        objectifs = text.text;
        objectifsSecondaire = text_sec.text;
    }

    public void Display()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            quit.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            quit.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GUI.backgroundColor = Color.clear;
            isPaused = false;
        }

        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            quit.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            text.gameObject.SetActive(true);
            text_sec.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
            text_sec.gameObject.SetActive(false);
        }

        Objectif();
        ObjectifsSecondaires();
        Message();
    }

    private void Objectif()
    {
        if (levelName == "Level1")
        {

        }



    }

    private void ObjectifsSecondaires()
    {
        if (levelName == "Level1")
        {

        }



    }

    private void Message()
    {
        if (levelName == "Level1")
        {

        }





    }


}
