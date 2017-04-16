using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Canvas_UI_Online : MonoBehaviour {

    public GameObject door1;

    private Transform player;
    private Personnage perso;

    private Text text;
    private Text text_msg;
    private Text text_sec;
    private Text text_infos;
    private Text text_pause;

    private RawImage healthBar;
    private RawImage shieldBar;

    private bool isPaused;

    private int compteur;
    private string levelName;

    private string objectifs;
    private string objectifsSecondaire;

    private float currentTime;
    private float timeForNext;

    private bool[] isPrincipalFinished;
    private bool[] isSecondaryFinished;
    private bool[] isMessageSent;

    public Canvas_UI_Online(Personnage perso, Text text, Text text_msg, Text text_sec, Text text_infos, Text text_pause, RawImage healthBar, RawImage shieldBar, Transform player, GameObject door1)
    {
        this.player = player;
        this.perso = perso;

        this.door1 = door1;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                compteur = 4;
                levelName = "Level1";
                isPrincipalFinished = new bool[] { false, false, false };
                isSecondaryFinished = new bool[] { false, false };
                isMessageSent = new bool[] { false, false, false, false, false, false, false, false, false, false}; //10
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

        isPaused = false;

        objectifs = text.text;
        objectifsSecondaire = text_sec.text;

        currentTime = 0;
        timeForNext = 3;
    }

    public void Display(int life, int shield)
    {

        /*Placement des barres de vie/bouclier*/
        healthBar.rectTransform.sizeDelta = new Vector2(life * 2.25f, 30); //225 = 100 => 1 = 2.25
        shieldBar.rectTransform.sizeDelta = new Vector2(shield * 2.25f, 30);

        healthBar.rectTransform.transform.position = new Vector2(life * 2.25f / 2 + 37, healthBar.rectTransform.transform.position.y);
        shieldBar.rectTransform.transform.position = new Vector2(shield * 2.25f / 2 + 37, healthBar.rectTransform.transform.position.y - 55);
        /*Fin du placement*/

        /*Verification si il y a une pause*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
                perso.escaped = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isPaused = false;
                perso.escaped = false;
            }
            
        }

        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        /*Fin de la verif*/

        /*Affichage des objectifs (et objectifs secondaires)*/
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
        /*Fin affichage*/

        Objectif();
        ObjectifsSecondaires();
        Message();
    }

    #region Objectifs
    private void Objectif()
    {
        if (levelName == "Level1")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + "<size=14>\n\n<color=white>-Allez voir les véhicules</color></size>")
                {
                    text.text = objectifs + "<size=14>\n\n<color=white>-Allez voir les véhicules</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 54 && player.transform.position.z <= 130)
                {
                    if (player.transform.position.x >= 135 && player.transform.position.x <= 180)
                    {
                        text.text = objectifs + "<size=14>\n\n<color=red>-Allez voir si il y a des survivants</color></size>" + "<size=14>\n\n<color=white>-Cherchez une sortie</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[0] = true;
                    }
                }
            }

            else if (!isPrincipalFinished[1])
            {
                if (player.transform.position.z >= 57 && player.transform.position.z <= 122)
                {
                    if (player.transform.position.x >= 367 && player.transform.position.x <= 430)
                    {
                        text.text = objectifs + "<size=14>\n\n<color=red>-Cherchez une sortie</color></size>" + "<size=14>\n\n<color=white>-Trouvez des batteries pour ouvrir la portes</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[1] = true;
                    }
                }
            }

            else if (!isPrincipalFinished[2])
            {
                if (player.transform.position.z >= 147 && player.transform.position.z <= 197)
                {
                    if (player.transform.position.x >= 108 && player.transform.position.x <= 186)
                    {
                        text.text = objectifs + "<size=14>\n\n<color=red>-Trouvez des batteries pour ouvrir la portes</color></size>" + "<size=14>\n\n<color=white>-Retournez à la porte</color></size>";
                        text.lineSpacing = 0.8f;

                        if (perso.FonctionNulleQuiRetrouneBool(door1, 10))
                        {
                            isPrincipalFinished[2] = true;
                        }
                        else
                        {
                            perso.CmdOpen_door(door1, 10);
                        }

                    }
                }
            }
        }//fin level1



    }
    #endregion

    #region Secondaires
    private void ObjectifsSecondaires()
    {
        if (levelName == "Level1")
        {
            if (!isSecondaryFinished[0])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver des cristaux</color></size>";
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver des cristaux</color></size>";
            }

            if (!isSecondaryFinished[1])
            {
                text_sec.text = text_sec.text + "<size=14>\n\n<color=white>-Trouver le coffre d'or</color></size>";
            }
            else
            {
                text_sec.text = text_sec.text + "<size=14>\n\n<color=red>-Trouver le coffre d'or</color></size>";
            }

            if (player.transform.position.z >= 48 && player.transform.position.z <= 112)
            {
                if (player.transform.position.x >= 249 && player.transform.position.x <= 290)
                {
                    text.text = objectifs + "<size=14>\n\n<color=red>-Trouvez des batteries pour ouvrir la portes</color></size>" + "<size=14>\n\n<color=white>-Retournez à la porte</color></size>";
                    text.lineSpacing = 0.8f;

                    isSecondaryFinished[0] = true;
                }
            }
            if (player.transform.position.z >= 350 && player.transform.position.z <= 355)
            {
                if (player.transform.position.x >= 246 && player.transform.position.x <= 250)
                {
                    text.text = objectifs + "<size=14>\n\n<color=red>-Trouvez des batteries pour ouvrir la portes</color></size>" + "<size=14>\n\n<color=white>-Retournez à la porte</color></size>";
                    text.lineSpacing = 0.8f;

                    isSecondaryFinished[1] = true;
                }
            }
        }//fin level 1



    }

    #endregion

    #region Messages
    private void Message()
    {
        if (levelName == "Level1")
        {
            if (!isMessageSent[0])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 85 && player.transform.position.z <= 110)
                {
                    if (player.transform.position.x >= 100 && player.transform.position.x <= 121)
                    {
                        if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Oui, ça va là-bas ?</i></color>\n<b>Radio</b> : <i>On a du mal à tenir, dépèche toi !</i>";
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>Tu m'entends ?</i></color>\n<b>Vous</b> : <i>Oui, ça va là-bas ?</i>";
                        }
                        else
                        {
                            text_msg.text = "<b>Radio</b> : <i>Tu m'entends ?</i>";
                        }
                    }
                }

            }



            if (!isMessageSent[1])
            {

                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 54 && player.transform.position.z <= 130)
                {
                    if (player.transform.position.x >= 135 && player.transform.position.x <= 180)
                    {
                        if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[1] = true;
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>Tu crois que tu es le premier a avoir été ici ? On a eu une base ici ... Ca s'est mal terminé</i></color>\n<b>Vous</b> : <i>Mince, désolé je ne savais pas</i>";
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>C'est quoi ce délire, pourquoi il y a des véhicules ici ?!</i></color>\n<b>Radio</b> : <i>Tu crois que tu es le premier a avoir été ici ? On a eu une base ici ... Ca s'est mal terminé</i>";
                        }
                        else
                        {
                            text_msg.text = "<b>Vous</b> : <i>C'est quoi ce délire, pourquoi il y a des véhicules ici ?!</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[2])
            { 
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 57 && player.transform.position.z <= 122)
                {
                    if (player.transform.position.x >= 367 && player.transform.position.x <= 430)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[2] = true;
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>La porte est fermée</i></color>\n<b>Radio</b> : <i>Il doit surement rester des batteries pour l'alimenter. Trouve en plusieurs pour alimenter la porte</i>";
                        }
                        else
                        {
                            text_msg.text = "<b>Vous</b> : <i>La porte est fermée</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[3])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 147 && player.transform.position.z <= 197)
                {
                    if (player.transform.position.x >= 108 && player.transform.position.x <= 186)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[3] = true;
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>J'ai trouvé les batteries</i></color>\n<b>Radio</b> : <i>Ok, retourne à la porte, j'ai réussi à rediriger l'énergie vers elle</i>";
                        }
                        else
                        {
                            text_msg.text = "<b>Vous</b> : <i>J'ai trouvé les batteries</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[4] && isMessageSent[2])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 65 && player.transform.position.z <= 110)
                {
                    if (player.transform.position.x >= 423 && player.transform.position.x <= 464)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[4] = true;
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>C'est bon, j'y suis</i></color>\n<b>Radio</b> : <i>Parfait, continue vers la sortie</i>";
                        }
                        else
                        {
                            text_msg.text = "<b>Vous</b> : <i>C'est bon, j'y suis</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[5])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 352 && player.transform.position.z <= 368)
                {
                    if (player.transform.position.x >= 229 && player.transform.position.x <= 251)
                    {
                        if (isMessageSent[2])
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[5] = true;
                            }
                            else
                            {
                                text_msg.text = "<b>Radio</b> : <i>Ces batteries sont trop endommagées, trouve en d'autres</i>";
                            }
                        }
                        else
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[5] = true;
                            }
                            else
                            {
                                text_msg.text = "<b>Radio</b> : <i>Ces batteries sont très vieilles</i>";
                            }
                        }

                        
                    }
                }
            }

            if (!isMessageSent[6])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 169 && player.transform.position.z <= 184)
                {
                    if (player.transform.position.x >= 275 && player.transform.position.x <= 301)
                    {
                        if (isMessageSent[2])
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[6] = true;
                            }
                            else
                            {
                                text_msg.text = "<b>Radio</b> : <i>Ces batteries sont trop endommagées, trouve en d'autres</i>";
                            }
                        }
                        else
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[6] = true;
                            }
                            else
                            {
                                text_msg.text = "<b>Radio</b> : <i>Ces batteries sont très vieilles</i>";
                            }
                        }
                    }
                }
            }

            if (!isMessageSent[7])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 238 && player.transform.position.z <= 261)
                {
                    if (player.transform.position.x >= 246 && player.transform.position.x <= 280)
                    {

                        if (isMessageSent[3])
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[7] = true;
                            }
                            else
                            {
                                text_msg.text = "<b>Radio</b> : <i>Pas besoin de plus de batteries, va vers la sortie</i>";
                            }
                        }
                        else
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[7] = true;
                            }
                            else
                            {
                                text_msg.text = "<b>Radio</b> : <i>Il n'y a pas assez de batteries, trouve en d'autres</i>";
                            }
                        }                        
                    }
                }
            }

            if (!isMessageSent[8])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 173 && player.transform.position.z <= 216)
                {
                    if (player.transform.position.x >= 346 && player.transform.position.x <= 376)
                    {
                        if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[8] = true;
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>C'est leurs dieu</i></color>\n<b>Vous</b> : <i>Mieux vaut ne pas rester là...</i>";
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>C'est quoi ce trucs ?!</i></color>\n<b>Radio</b> : <i>C'est leurs dieu</i>";
                        }
                        else
                        {
                            text_msg.text = "<b>Vous</b> : <i>C'est quoi ce trucs ?!</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[9])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 345 && player.transform.position.z <= 400)
                {
                    if (player.transform.position.x >= 297 && player.transform.position.x <= 402)
                    {
                        if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[9] = true;
                        }
                        else if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Où sont nos soldats ? Je ne voit personne. Même pas de corps ...</i></color>\n<b>Radio</b> : <i>Je ne sais pas, reste concentré dans ta mission</i>";
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>Nan, c'est un avant poste</i></color>\n<b>Vous</b> : <i>Où sont nos soldats ? Je ne voit personne. Même pas de corps ...</i>";
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>C'est ça la base dont vous parliez ?</i></color>\n<b>Radio</b> : <i>Nan, c'est un avant poste</i>";
                        }
                        else
                        {
                            text_msg.text = "<b>Vous</b> : <i>C'est ça la base dont vous parliez ?</i>";
                        }
                    }
                }
            }



        }//fin level1





    }
    #endregion



}
