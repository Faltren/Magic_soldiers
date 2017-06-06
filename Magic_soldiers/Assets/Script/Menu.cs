using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private int compteur;

    private bool Up;
    private bool Down;

    public static bool options;

    public Camera cam;

    private GameObject Buttons;
    public static GameObject Options;

    public Scrollbar bar;
    private Text bar_text;
    public static int sensi;

    public Scrollbar bar_vol;
    private Text bar_vol_text;
    public static int volume;

    /*FR - EN*/
    public Text tuto;
    public Text jouer;
    public Text Quitter;

    public Text sensi_text;
    public Text vol;
    public Text langue_text;

    public static string langue = "fr";

    void Start()
    {

        Up = false;
        Down = false;

        options = false;

        Buttons = GameObject.Find("Boutons");
        Options = GameObject.Find("Options_label");

        bar_text = GameObject.Find("Nb_sensi").GetComponent<Text>();

        bar_vol_text = GameObject.Find("Nb_volume").GetComponent<Text>();

        Vector3 camPos = cam.transform.position;
        Buttons.transform.position = new Vector3(camPos.x - 6, camPos.y, camPos.z + 30);

        sensi = 5;

        Options.SetActive(false);

        compteur = 0;
    }


    void Update()
    {

        if (langue == "fr")
        {
            tuto.text = "Tutoriel";
            jouer.text = "Jouer";
            Quitter.text = "Quitter";
            sensi_text.text = "sensibilite";
            vol.text = "volume";
            langue_text.text = "langue";
        }
        else
        {
            tuto.text = "Tutorial";
            jouer.text = "Play";
            Quitter.text = "Quit";
            sensi_text.text = "sensibility";
            vol.text = "Volume";
            langue_text.text = "language";
        }


        Moves();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }

        if (options)
        {
            bar_text.text = (bar.value * 10).ToString();
            sensi = (int)(bar.value * 10);

            bar_vol_text.text = (bar_vol.value * 2).ToString();
            volume = (int)(bar_vol.value * 2);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (compteur)
            {
                case 0: //Tuto
                    SceneManager.LoadScene("Tuto");
                    //Application.LoadLevel("Tuto");
                    break;

                case 1: //Jouer
                    if (langue == "fr")
                        Application.LoadLevel("Lobby_netWork");
                    else
                        Application.LoadLevel("Lobby_netWork_EN");
                    break;

                case 2: //Options
                    options = true;
                    Options.SetActive(true);
                    break;

                case 3: //Quitter
                    Application.Quit();
                    break;

                default:
                    break;
            }

        }

    }


    #region mouvements

    private void Moves()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!options)
                Up = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!options)
                Down = true;
        }

        if (compteur < 4)
        {
            if (compteur > -1)
            {
                if (compteur == 0)
                {
                    if (Up)
                    {
                        MoveUp(-38);
                    }
                    else if (Down)
                    {
                        Down = false;
                    }

                }
                if (compteur == 1)
                {
                    if (Up)
                    {
                        MoveUp(-70);
                    }
                    else if (Down)
                    {
                        MoveDown(0);
                    }
                }
                if (compteur == 2)
                {
                    if (Up)
                    {
                        MoveUp(-92);
                    }
                    else if (Down)
                    {
                        MoveDown(-38);
                    }
                }
                if (compteur == 3)
                {
                    if (Up)
                    {
                        Up = false;
                    }
                    else if (Down)
                    {
                        MoveDown(-70);
                    }
                }
            }
            else
            {
                Up = false;
                Down = false;
                compteur++;
            }
        }
        else
        {
            Up = false;
            Down = false;
            compteur--;
        }
    }


    private void MoveUp(int degre)
    {
        if (Buttons.transform.rotation.x * 100 > degre)
        {
            Buttons.transform.Rotate(new Vector3(-2f, 0, 0));
        }
        else
        {
            compteur++;
            Up = false;
            Down = false;
        }

    }

    private void MoveDown(int degre)
    {
        if (Buttons.transform.rotation.x * 100 < degre)
        {
            Buttons.transform.Rotate(new Vector3(2f, 0, 0));
        }
        else
        {
            compteur--;
            Up = false;
            Down = false;
        }

    }
    #endregion




}
