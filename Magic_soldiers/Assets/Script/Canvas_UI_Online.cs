using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Canvas_UI_Online : MonoBehaviour {

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;
    public GameObject door5;
    public GameObject door6;
    public GameObject door7;

    private Transform player;
    private Personnage perso;

    private Text text;
    private Text text_msg;
    private Text text_sec;
    private Text text_infos;
    private Text text_pause;

    private GameObject quit;

    private Image healthBar;
    private Image shieldBar;

    private Image msg_img;

    private bool isPaused;

    private string levelName;

    private string objectifs;
    private string objectifsSecondaire;

    private float currentTime;
    private float timeForNext;

    private bool[] isPrincipalFinished;
    private bool[] isSecondaryFinished;
    private bool[] isMessageSent;

    private bool isGobelinActivated;
    private bool isGobelinQuestDone;
    //private int isSending = 0;

    private int life;
    private int shield;

    public Canvas_UI_Online(Personnage perso, GameObject quit, Text text, Text text_msg, Text text_sec, Text text_infos, Text text_pause, Image healthBar, Image shieldBar, Image msg_img, Transform player, GameObject door1, GameObject door2, GameObject door3, GameObject door4, GameObject door5, GameObject door6, GameObject door7)
    {
        isGobelinActivated = false;
        isGobelinQuestDone = false;

        this.player = player;
        this.perso = perso;

        this.door1 = door1;
        this.door1 = door2;
        this.door1 = door3;
        this.door1 = door4;
        this.door1 = door5;
        this.door1 = door6;
        this.door1 = door7;

        //Level1
        levelName = "Level1";
        isPrincipalFinished = new bool[] { false, false, false, false};
        isSecondaryFinished = new bool[] { false, false };
        isMessageSent = new bool[] { false, false, false, false, false, false, false, false, false, false }; //10

        this.text = text;
        this.text_msg = text_msg;
        this.text_sec = text_sec;
        this.text_infos = text_infos;
        this.text_pause = text_pause;

        this.quit = quit;
        quit.SetActive(false);

        this.healthBar = healthBar;
        this.shieldBar = shieldBar;

        this.msg_img = msg_img;
        msg_img.gameObject.SetActive(false);

        isPaused = false;

        objectifs = text.text;
        objectifsSecondaire = text_sec.text;

        text.text = "";
        text_sec.text = "";

        currentTime = 0;
        timeForNext = 3;

        life = 100;
        shield = 100;
    }

    public void Display(int life, int shield)
    {

        this.life = life;
        this.shield = shield;

        /*Verification si il y a une pause*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
                perso.escaped = true;
                quit.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isPaused = false;
                perso.escaped = false;
                quit.SetActive(false);
            }
            
        }

        if (isPaused && !perso.escaped)
        {
            isPaused = false;
        }
        else if(!isPaused && perso.escaped)
        {
            isPaused = true;
        }

        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            quit.SetActive(false);
        }
        else
        {
            quit.SetActive(true);
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
        Texte_pause();
    }

    
    #region textePause
    private void Texte_pause()
    {
        if (player.transform.position.z >= 43 && player.transform.position.z <= 113 && player.transform.position.x >= 413 && player.transform.position.x <= 454)
        {
            levelName = "Level2";
            text_pause.text = "Fin du Niveau 1";
            isPrincipalFinished[3] = true;
            isPrincipalFinished = new bool[] { false, false, false, false, false };
            isSecondaryFinished = new bool[] { false };
            isMessageSent = new bool[] { false, false, false, false, false, false, false, false, false, false, false }; //11
            text.text = objectifs + " " + levelName + " ";
            text_sec.text = objectifsSecondaire;
            isGobelinActivated = false;
            isGobelinQuestDone = false;
        }
        else
        {
            text_pause.text = "";
        }
    }
    #endregion

    #region Objectifs
    private void Objectif()
    {
        if (levelName == "Level1")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Allez voir les véhicules</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Allez voir les véhicules</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 54 && player.transform.position.z <= 130)
                {
                    if (player.transform.position.x >= 135 && player.transform.position.x <= 180)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Allez voir si il y a des survivants</color></size>" + "<size=14>\n\n<color=white>-Cherchez une sortie</color></size>";
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
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Cherchez une sortie</color></size>" + "<size=14>\n\n<color=white>-Trouvez des batteries pour ouvrir la portes</color></size>";
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
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Trouvez des batteries pour ouvrir la portes</color></size>" + "<size=14>\n\n<color=white>-Retournez à la porte</color></size>";
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
            else if (!isPrincipalFinished[3])
            {
                if (player.transform.position.z >= 43 && player.transform.position.z <= 113)
                {
                    if (player.transform.position.x >= 413 && player.transform.position.x <= 454)
                    {
                        isPrincipalFinished[3] = true;
                        levelName = "Level2";
                        isPrincipalFinished = new bool[] { false, false, false, false, false };
                        isSecondaryFinished = new bool[] { false };
                        isMessageSent = new bool[] { false, false, false, false, false, false, false, false, false, false, false }; //11
                        text.text = objectifs + " " + levelName + " ";
                        text_sec.text = objectifsSecondaire;
                        isGobelinActivated = false;
                        isGobelinQuestDone = false;
                    }
                }
            }

        }//fin level1

        if (levelName == "Level2")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Avancez dans le donjon</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Avancez dans le donjon</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 368 && player.transform.position.z <= 477)
                {
                    if (player.transform.position.x >= 600 && player.transform.position.x <= 739)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Avancez dans le donjon</color></size>" + "<size=14>\n\n<color=white>-Tuez les squelettes</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[0] = true;
                    }
                }

            }
            else if (!isPrincipalFinished[1])
            {
                if (player.transform.position.z >= 320 && player.transform.position.z <= 352)
                {
                    if (player.transform.position.x >= 738 && player.transform.position.x <= 759)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Tuez les squelettes</color></size>" + "<size=14>\n\n<color=white>-Explorez la zone</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[1] = true;
                    }
                }
            }
            else if(!isPrincipalFinished[2])
            {
                if (player.transform.position.z >= 198 && player.transform.position.z <= 245)
                {
                    if (player.transform.position.x >= 1086 && player.transform.position.x <= 1129)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Explorez la zone</color></size>" + "<size=14>\n\n<color=white>-Trouvez un moyen d'ouvrir la porte</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[2] = true;
                    }
                }
            }
            else if(!isPrincipalFinished[3])
            {
                if (player.transform.position.z >= 78 && player.transform.position.z <= 173)
                {
                    if (player.transform.position.x >= 886 && player.transform.position.x <= 927)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Trouvez un moyen d'ouvrir la porte</color></size>" + "<size=14>\n\n<color=white>-Retournez à la porte</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[3] = true;
                    }
                }
            }
            else if(!isPrincipalFinished[4])
            {
                //fin
            }


        }//fin level2



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

                if (player.transform.position.z >= 48 && player.transform.position.z <= 112)
                {
                    if (player.transform.position.x >= 249 && player.transform.position.x <= 290)
                    {
                        isSecondaryFinished[0] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver des cristaux</color></size>";
            }

            if (!isSecondaryFinished[1])
            {
                text_sec.text = text_sec.text + "<size=14>\n\n<color=white>-Trouver le coffre d'or</color></size>";

                if (player.transform.position.z >= 350 && player.transform.position.z <= 355)
                {
                    if (player.transform.position.x >= 246 && player.transform.position.x <= 250)
                    {
                        isSecondaryFinished[1] = true;
                    }
                }
            }
            else
            {
                text_sec.text = text_sec.text + "<size=14>\n\n<color=red>-Trouver le coffre d'or</color></size>";
            }

        }//fin level 1

        if (levelName == "Level2")
        {
            if (!isSecondaryFinished[0])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver leurs statue</color></size>";

                if (player.transform.position.z >= 262 && player.transform.position.z <= 333)
                {
                    if (player.transform.position.x >= 535 && player.transform.position.x <= 581)
                    {
                        isSecondaryFinished[0] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver leurs statue</color></size>";
            }

            if (isGobelinActivated)
            {
                if (isGobelinQuestDone && isMessageSent[10])
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=red>-Trouver du KiBrille</color></size>";
                }
                else
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=white>-Trouver du KiBrille</color></size>";
                }               
            }

            /*portes level2*/
            if (player.transform.position.z >= 116 && player.transform.position.z <= 147)
            {
                if (player.transform.position.x >= 752 && player.transform.position.x <= 794)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door2, 10))
                    {
                        perso.CmdOpen_door(door2, 10);
                    }
                    if (!perso.FonctionNulleQuiRetrouneBool(door3, 10))
                    {
                        perso.CmdOpen_door(door3, 10);
                    }

                }
            }

            if (player.transform.position.z >= 78 && player.transform.position.z <= 173)
            {
                if (player.transform.position.x >= 886 && player.transform.position.x <= 927)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door5, 10))
                    {
                        perso.CmdOpen_door(door5, 10);
                    }

                }
            }

            if (player.transform.position.z >= 250 && player.transform.position.z <= 280)
            {
                if (player.transform.position.x >= 1143 && player.transform.position.x <= 1197)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door4, 10))
                    {
                        perso.CmdOpen_door(door4, 10);
                    }
                    if (!perso.FonctionNulleQuiRetrouneBool(door7, 10))
                    {
                        perso.CmdOpen_door(door7, 10);
                    }
                    if (!perso.FonctionNulleQuiRetrouneBool(door6, 10))
                    {
                        perso.CmdOpen_door(door6, 10);
                    }

                }
            }
            /*fin portes level2*/

        }//fin level 2


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
                            msg_img.gameObject.SetActive(false);
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
                            msg_img.gameObject.SetActive(true);
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
                            msg_img.gameObject.SetActive(false);
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
                            msg_img.gameObject.SetActive(true);
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
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>La porte est fermée</i></color>\n<b>Radio</b> : <i>Il doit surement rester des batteries pour l'alimenter. Trouve en plusieurs pour alimenter la porte</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
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
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>J'ai trouvé les batteries</i></color>\n<b>Radio</b> : <i>Ok, retourne à la porte, j'ai réussi à rediriger l'énergie vers elle</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
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
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>C'est bon, j'y suis</i></color>\n<b>Radio</b> : <i>Parfait, continue vers la sortie</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
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
                                msg_img.gameObject.SetActive(false);
                            }
                            else
                            {
                                msg_img.gameObject.SetActive(true);
                                text_msg.text = "<b>Radio</b> : <i>Ces batteries sont trop endommagées, trouve en d'autres</i>";
                            }
                        }
                        else
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[5] = true;
                                msg_img.gameObject.SetActive(false);
                            }
                            else
                            {
                                msg_img.gameObject.SetActive(true);
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
                                msg_img.gameObject.SetActive(false);
                            }
                            else
                            {
                                msg_img.gameObject.SetActive(true);
                                text_msg.text = "<b>Radio</b> : <i>Ces batteries sont trop endommagées, trouve en d'autres</i>";
                            }
                        }
                        else
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[6] = true;
                                msg_img.gameObject.SetActive(false);
                            }
                            else
                            {
                                msg_img.gameObject.SetActive(true);
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
                                msg_img.gameObject.SetActive(false);
                            }
                            else
                            {
                                msg_img.gameObject.SetActive(true);
                                text_msg.text = "<b>Radio</b> : <i>Pas besoin de plus de batteries, va vers la sortie</i>";
                            }
                        }
                        else
                        {
                            if (currentTime + timeForNext < Time.time)
                            {
                                text_msg.text = "";
                                isMessageSent[7] = true;
                                msg_img.gameObject.SetActive(false);
                            }
                            else
                            {
                                msg_img.gameObject.SetActive(true);
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
                            msg_img.gameObject.SetActive(false);
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
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>C'est quoi ce truc ?!</i>";
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
                            msg_img.gameObject.SetActive(false);
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
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>C'est ça la base dont vous parliez ?</i>";
                        }
                    }
                }
            }
        }//fin level1

        if (levelName == "Level2")
        {
            if (!isMessageSent[0]) //mangeoire
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 110 && player.transform.position.z <= 188)
                {
                    if (player.transform.position.x >= 548 && player.transform.position.x <= 625)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Putin de merde ... c'est quoi ce délire ?!</i></color>\n<b>Radio</b> : <i>Je crois qu'on a retrouvé une partie de nos hommes</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Putin de merde ... c'est quoi ce délire ?!</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[1]) //batterie 1
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 116 && player.transform.position.z <= 147)
                {
                    if (player.transform.position.x >= 752 && player.transform.position.x <= 794)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[1] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>J'ai trouvé des batteries en état de marche</i></color>\n<b>Radio</b> : <i>Ok, j'essaye de les activer, essaye de voir ce qu'elles ont ouvert</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>J'ai trouvé des batteries en état de marche</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[2]) //batteries 3
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 250 && player.transform.position.z <= 280)
                {
                    if (player.transform.position.x >= 1143 && player.transform.position.x <= 1197)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[2] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>J'ai trouvé des batteries en état de marche</i></color>\n<b>Radio</b> : <i>Ok, j'essaye de les activer, essaye de voir ce qu'elles ont ouvert</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>J'ai trouvé des batteries en état de marche</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[3]) //statue
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 262 && player.transform.position.z <= 333)
                {
                    if (player.transform.position.x >= 535 && player.transform.position.x <= 581)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[3] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>J'ai encore trouvé une de leurs statue</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[4]) //squelette
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 368 && player.transform.position.z <= 477)
                {
                    if (player.transform.position.x >= 600 && player.transform.position.x <= 739)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[4] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Mince, il y a beaucoup de squelette !</i></color>\n<b>Radio</b> : <i>Bien reçu, vous devriez en tuer le plus possible pour avancer</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Mince, il y a beaucoup de squelette !</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[5]) //porte finale
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 198 && player.transform.position.z <= 245)
                {
                    if (player.transform.position.x >= 1086 && player.transform.position.x <= 1129)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[5] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>La porte est encore fermée</i></color>\n<b>Radio</b> : <i>Essayez de trouver des batteries pour l'ouvrir</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>La porte est encore fermée</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[6]) //batteries 2
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 74 && player.transform.position.z <= 133)
                {
                    if (player.transform.position.x >= 891 && player.transform.position.x <= 922)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[6] = true;
                            isPrincipalFinished[1] = true;
                            isPrincipalFinished[2] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Je pense que ces batteries peuvent ouvrir la porte</i></color>\n<b>Radio</b> : <i>Affirmatif, la porte est ouverte</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Je pense que ces batteries peuvent ouvrir la porte</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[7]) //C'est grand...
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 320 && player.transform.position.z <= 352)
                {
                    if (player.transform.position.x >= 738 && player.transform.position.x <= 759)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[7] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Wow, ces mines feraient une bonne base pour un film fantastique ...</i>";
                        }
                    }
                }
            }

            /*Gobelin*/
            if (!isMessageSent[8])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 386 && player.transform.position.z <= 402)
                {
                    if (player.transform.position.x >= 1018 && player.transform.position.x <= 1038)
                    {
                        if (currentTime + timeForNext * 4 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[8] = true;
                            isGobelinActivated = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Vous croyez que je peux lui faire confiance ?</i></color>\n<b>Radio</b> : <i>Je ne sais pas trop, les gobelins sont connus pour leurs fourberies ... Aide le quand même, on ne sait jamais</i>";
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Gobelin</b> : <i>Eh toi ! Ramene moi du KiBrille et je te donnerai quelque chose en échange!</i></color>\n<b>Vous</b> : <i>Vous croyez que je peux lui faire confiance ?</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>Eh toi ! Ramene moi du KiBrille et je te donnerai quelque chose en échange!</i>";
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

                if (player.transform.position.z >= 272 && player.transform.position.z <= 290)
                {
                    if (player.transform.position.x >= 906 && player.transform.position.x <= 922)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[9] = true;
                            isGobelinQuestDone = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>C'est ça du KiBrille ?</i></color>\n<b>Radio</b> : <i>Je pense que oui</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>C'est ça du KiBrille ?</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[10] && isGobelinQuestDone)
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 386 && player.transform.position.z <= 402)
                {
                    if (player.transform.position.x >= 1018 && player.transform.position.x <= 1038)
                    {
                        if (currentTime + timeForNext * 4 < Time.time)
                        {
                            if(Personnage.attack <= 10)
                                Personnage.attack += 1;
                            text_msg.text = "";
                            isMessageSent[10] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>Oh merci ! Tiens prends ça, tu tireras plus fort avec ça. Maintenant va-t'en et ne dis pas que je suis là !</i>";
                        }
                    }
                }
            }
            /*Gobelin*/

        }//fin level2



    }
    #endregion



}
