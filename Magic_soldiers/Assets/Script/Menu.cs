using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    private int compteur;

    private bool Up;
    private bool Down;

    GameObject Buttons;
	
	void Start () {

        Up = false;
        Down = false;

        Buttons = GameObject.Find("Boutons");
        compteur = 0;
	}
	
	
	void Update () {

        Moves();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (compteur)
            {
                case 0: //Tuto
                    Application.LoadLevel("Tuto");
                    
                    break;

                case 1: //Jouer
                    Application.LoadLevel("Lobby_netWork");
                    break;

                case 2: //Options
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
            Up = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
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
            Buttons.transform.Rotate(new Vector3(-1f, 0, 0));
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
            Buttons.transform.Rotate(new Vector3(1f, 0, 0));
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
