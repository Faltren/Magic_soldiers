using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Canvas_UI_Online : MonoBehaviour
{

    public static string langue = Menu.langue;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;
    public GameObject door5;
    public GameObject door6;
    public GameObject door7;
    public GameObject door8;
    public GameObject door9;
    public GameObject door10;
    public GameObject door11;
    public GameObject door12;
    public GameObject door13;
    public GameObject door14;
    public GameObject door15;

    private Transform player;
    private Personnage perso;

    private Text text;
    private Text text_msg;
    private Text text_sec;
    private Text text_infos;
    private Text text_pause;

    private GameObject quit;

    private RawImage healthBar;
    private RawImage shieldBar;

    private Image msg_img;

    private bool isPaused;

    public static string levelName = "Level1";

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

    public Canvas_UI_Online(Personnage perso, GameObject quit, Text text, Text text_msg, Text text_sec, Text text_infos, Text text_pause, RawImage healthBar, RawImage shieldBar, Image msg_img, Transform player, GameObject door1, GameObject door2, GameObject door3, GameObject door4, GameObject door5, GameObject door6, GameObject door7, GameObject door8, GameObject door9, GameObject door10, GameObject door11, GameObject door12, GameObject door13, GameObject door14, GameObject door15)
    {
        isGobelinActivated = false;
        isGobelinQuestDone = false;

        this.player = player;
        this.perso = perso;

        this.door1 = door1;
        this.door2 = door2;
        this.door3 = door3;
        this.door4 = door4;
        this.door5 = door5;
        this.door6 = door6;
        this.door7 = door7;
        this.door8 = door8;
        this.door9 = door9;
        this.door10 = door10;
        this.door11 = door11;
        this.door12 = door12;
        this.door13 = door13;
        this.door14 = door14;
        this.door15 = door15;


        //Level1
        levelName = "Level1";
        isPrincipalFinished = new bool[] { false, false, false, false };
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
        else if (!isPaused && perso.escaped)
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

        if (langue == "fr")
        {
            Objectif();
            ObjectifsSecondaires();
            Message();
            Texte_pause();
        }
        else
        {
            Objectif_EN();
            ObjectifsSecondaires_EN();
            Message_EN();
            Texte_Pause_EN();
        }

    }


    #region textePause
    private void Texte_pause()
    {
        if (player.transform.position.z >= 43 && player.transform.position.z <= 113 && player.transform.position.x >= 413 && player.transform.position.x <= 454) //passage vers level2
        {
            levelName = "Level2";
            text_pause.text = "Fin du Niveau 1";

            isPrincipalFinished = new bool[] { false, false, false, false, false };
            isSecondaryFinished = new bool[] { false };
            isMessageSent = new bool[] { false, false, false, false, false, false, false, false, false, false, false }; //11

            text.text = objectifs + " " + levelName + " ";
            text_sec.text = objectifsSecondaire;

            isGobelinActivated = false;
            isGobelinQuestDone = false;
        }
        else if(player.transform.position.z >= 199 && player.transform.position.z <= 230 && player.transform.position.x >= 1129 && player.transform.position.x <= 1158)//passage vers level3
        {
            levelName = "Level3";
            text_pause.text = "Fin du Niveau 2";

            isPrincipalFinished = new bool[] { false, false, false, false };
            isSecondaryFinished = new bool[] { false, false };
            isMessageSent = new bool[] { false, false, false, false, false, false, false, false }; //8

            text.text = objectifs + " " + levelName + " ";
            text_sec.text = objectifsSecondaire;

            isGobelinActivated = false;
            isGobelinQuestDone = false;
        
        }
        else if (player.transform.position.z >= 479 && player.transform.position.z <= 506 && player.transform.position.x >= 1512 && player.transform.position.x <= 1554)//passage vers level4
        {
            levelName = "Level4";
            text_pause.text = "Fin du Niveau 3";

            isPrincipalFinished = new bool[] { false, false, false };
            isSecondaryFinished = new bool[] { false };

            isMessageSent = new bool[] { false, false, false, false, false, false, false, false }; //8
            text.text = objectifs + " " + levelName + " ";

            text_sec.text = objectifsSecondaire;
            isGobelinActivated = false;
            isGobelinQuestDone = false;
        }
        else if (player.transform.position.z >= 964 && player.transform.position.z <= 990 && player.transform.position.x >= 1543 && player.transform.position.x <= 1580)//passage vers level5
        {
            levelName = "Level5";
            text_pause.text = "Fin du Niveau 4";

            isPrincipalFinished = new bool[] { false, false, false, false };
            isSecondaryFinished = new bool[] { false };

            isMessageSent = new bool[] { false, false, false, false, false, false, false, false, false }; //11
            text.text = objectifs + " " + levelName + " ";

            text_sec.text = objectifsSecondaire;
            isGobelinActivated = false;
            isGobelinQuestDone = false;
        }
        else if (player.transform.position.z >= 1053 && player.transform.position.z <= 1099 && player.transform.position.x >= 1966 && player.transform.position.x <= 1997)//passage vers level6
        {
            levelName = "Level6";
            text_pause.text = "Fin du Niveau 5";

            isPrincipalFinished = new bool[] { false, false, false };
            isSecondaryFinished = new bool[] { false, false, false };

            isMessageSent = new bool[] { false, false, false, false, false, false, false }; //7
            text.text = objectifs + " " + levelName + " ";

            text_sec.text = objectifsSecondaire;
            isGobelinActivated = false;
            isGobelinQuestDone = false;
        }
        else if (player.transform.position.z >= 1376 && player.transform.position.z <= 1436 && player.transform.position.x >= 2431 && player.transform.position.x <= 2480)//passage vers level7
        {
            levelName = "Level7";
            text_pause.text = "Fin du Niveau 6";

            isPrincipalFinished = new bool[] { false, false };
            isSecondaryFinished = new bool[] { false };

            isMessageSent = new bool[] { false };
            text.text = objectifs + " " + levelName + " ";

            text_sec.text = objectifsSecondaire;
            isGobelinActivated = false;
            isGobelinQuestDone = false;
        }
        else if (player.transform.position.z >= 1041 && player.transform.position.z <= 1093 && player.transform.position.x >= 2964 && player.transform.position.x <= 2997)//passage vers level8
        {
            levelName = "Level8";
            text_pause.text = "Fin du Niveau 7";

            isPrincipalFinished = new bool[] { false };
            isSecondaryFinished = new bool[] { };

            isMessageSent = new bool[] { false, false };
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

    private void Texte_Pause_EN()
    {

    }
    #endregion

    #region Objectifs
    private void Objectif()
    {
        #region level1
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
        #endregion

        #region level2
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
            else if (!isPrincipalFinished[2])
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
            else if (!isPrincipalFinished[3])
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
            else if (!isPrincipalFinished[4])
            {
                isPrincipalFinished[4] = true;
                levelName = "Level3";

                isPrincipalFinished = new bool[] { false, false, false, false };
                isSecondaryFinished = new bool[] { false, false };

                isMessageSent = new bool[] { false, false, false, false, false, false, false, false }; //8

                text.text = objectifs + " " + levelName + " ";
                text_sec.text = objectifsSecondaire;

                isGobelinActivated = false;
                isGobelinQuestDone = false;
            }


        }//fin level2
        #endregion

        #region level3
        if (levelName == "Level3")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Avancez dans le donjon</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Avancez dans le donjon</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 446 && player.transform.position.z <= 480)
                {
                    if (player.transform.position.x >= 1493 && player.transform.position.x <= 1552)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Avancez dans le donjon</color></size>" + "<size=14>\n\n<color=white>-Trouver des générateurs</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[0] = true;
                    }
                }

            }
            else if (!isPrincipalFinished[1])
            {
                if (player.transform.position.z >= 42 && player.transform.position.z <= 64)
                {
                    if (player.transform.position.x >= 1530 && player.transform.position.x <= 1560)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Trouver des générateurs</color></size>" + "<size=14>\n\n<color=white>-Retourner vers la sortie</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[1] = true;
                    }
                }
            }
            else if (!isPrincipalFinished[2])
            {
                if (player.transform.position.z >= 446 && player.transform.position.z <= 480)
                {
                    if (player.transform.position.x >= 1493 && player.transform.position.x <= 1552)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Retourner vers la sortie</color></size>" + "<size=14>\n\n<color=white>-Continuez d'anvancer dans le donjon</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[2] = true;
                    }
                }
            }
            else if (!isPrincipalFinished[3])
            {
                isPrincipalFinished[3] = true;
                levelName = "Level4";

                isPrincipalFinished = new bool[] { false, false, false };
                isSecondaryFinished = new bool[] { false };

                isMessageSent = new bool[] { false, false, false, false, false, false, false, false }; //8
                text.text = objectifs + " " + levelName + " ";

                text_sec.text = objectifsSecondaire;
                isGobelinActivated = false;
                isGobelinQuestDone = false;
            }

        }//fin level3
        #endregion

        #region level4
        if (levelName == "Level4")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Avancez dans le donjon</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Avancez dans le donjon</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 730 && player.transform.position.z <= 944)
                {
                    if (player.transform.position.x >= 1414 && player.transform.position.x <= 1626)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Avancez dans le donjon</color></size>" + "<size=14>\n\n<color=white>-Tuer la maman</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[0] = true;
                    }
                }

            }
            else if (!isPrincipalFinished[1])
            {
                if (player.transform.position.z >= 964 && player.transform.position.z <= 998)
                {
                    if (player.transform.position.x >= 1546 && player.transform.position.x <= 1582)
                    {
                        isPrincipalFinished[1] = true;
                    }
                }
            }
               
            else if (!isPrincipalFinished[2])
            {
                isPrincipalFinished[2] = true;
                levelName = "Level5";

                isPrincipalFinished = new bool[] { false, false, false, false };
                isSecondaryFinished = new bool[] { false };

                isMessageSent = new bool[] { false, false, false, false, false, false, false, false, false }; //11
                text.text = objectifs + " " + levelName + " ";

                text_sec.text = objectifsSecondaire;
                isGobelinActivated = false;
                isGobelinQuestDone = false;
            }

        }//fin level4
        #endregion

        #region level5
        if (levelName == "Level5")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Sortir du labyrinthe</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Sortir du labyrinthe</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 1284 && player.transform.position.z <= 1352)
                {
                    if (player.transform.position.x >= 1686 && player.transform.position.x <= 1700)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Sortir du labyrinthe</color></size>" + "<size=14>\n\n<color=white>-Trouver un générateur</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[0] = true;
                    }
                }

            }
            else if (!isPrincipalFinished[1])
            {
                if (player.transform.position.z >= 1315 && player.transform.position.z <= 1338)
                {
                    if (player.transform.position.x >= 1617 && player.transform.position.x <= 1638)
                    {
                        isPrincipalFinished[1] = true;

                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Trouver un générateur</color></size>" + "<size=14>\n\n<color=white>-Trouver un moyen de sortir</color></size>";
                        text.lineSpacing = 0.8f;
                    }
                }
            }

            else if (!isPrincipalFinished[2])
            {
                if (player.transform.position.z >= 1172 && player.transform.position.z <= 1203)
                {
                    if (player.transform.position.x >= 1810 && player.transform.position.x <= 1839)
                    {
                        isPrincipalFinished[2] = true;

                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Trouver un moyen de sortir</color></size>" + "<size=14>\n\n<color=white>-Suivre ce chemin</color></size>";
                        text.lineSpacing = 0.8f;
                    }
                }
            }

            else if (!isPrincipalFinished[3])
            {
                isPrincipalFinished[3] = true;
                levelName = "Level6";

                isPrincipalFinished = new bool[] { false, false, false };
                isSecondaryFinished = new bool[] { false, false, false };

                isMessageSent = new bool[] { false, false, false, false, false, false, false}; //7
                text.text = objectifs + " " + levelName + " ";

                text_sec.text = objectifsSecondaire;
                isGobelinActivated = false;
                isGobelinQuestDone = false;
            }

        }//fin level5
        #endregion

        #region level6
        if (levelName == "Level6")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Trouver une sortie</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Trouver une sortie</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 1197 && player.transform.position.z <= 1223)
                {
                    if (player.transform.position.x >= 2032 && player.transform.position.x <= 2075)
                    {
                        text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=red>-Trouver une sortie</color></size>" + "<size=14>\n\n<color=white>-Survivre jusqu'à la sortie</color></size>";
                        text.lineSpacing = 0.8f;

                        isPrincipalFinished[0] = true;
                    }
                }

            }
            else if (!isPrincipalFinished[1])
            {
                if (player.transform.position.z >= 1366 && player.transform.position.z <= 1436)
                {
                    if (player.transform.position.x >= 2430 && player.transform.position.x <= 2579)
                    {
                        isPrincipalFinished[1] = true;
                    }
                }
            }

            else if (!isPrincipalFinished[2])
            {
                isPrincipalFinished[2] = true;
                levelName = "Level7";

                isPrincipalFinished = new bool[] { false, false };
                isSecondaryFinished = new bool[] { false};

                isMessageSent = new bool[] { false}; 
                text.text = objectifs + " " + levelName + " ";

                text_sec.text = objectifsSecondaire;
                isGobelinActivated = false;
                isGobelinQuestDone = false;
            }

        }//fin level6
        #endregion

        #region level7
        if (levelName == "Level7")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Fuir</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Fuir</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 1041 && player.transform.position.z <= 1097)
                {
                    if (player.transform.position.x >= 2947 && player.transform.position.x <= 2969)
                    {
                        isPrincipalFinished[0] = true;
                    }
                }

            }
            else if (!isPrincipalFinished[1])
            {
                isPrincipalFinished[1] = true;
                levelName = "Level8";

                isPrincipalFinished = new bool[] { false };
                isSecondaryFinished = new bool[] { };

                isMessageSent = new bool[] { false, false }; 
                text.text = objectifs + " " + levelName + " ";

                text_sec.text = objectifsSecondaire;
                isGobelinActivated = false;
                isGobelinQuestDone = false;
            }

        }//fin level7
        #endregion

        #region level8
        if (levelName == "Level8")
        {
            if (!isPrincipalFinished[0])
            {
                if (text.text != objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Après la mère, le père ...</color></size>")
                {
                    text.text = objectifs + " " + levelName + " " + "<size=14>\n\n<color=white>-Après la mère, le père ...</color></size>";
                    text.lineSpacing = 0.8f;
                }

                if (player.transform.position.z >= 792 && player.transform.position.z <= 814)
                {
                    if (player.transform.position.x >= 3429 && player.transform.position.x <= 3468)
                    {
                        isPrincipalFinished[0] = true;
                    }
                }

            }
            else if (!isPrincipalFinished[1])
            {
                isPrincipalFinished[1] = true;
                levelName = "Fin";

                isPrincipalFinished = new bool[] { false };
                isSecondaryFinished = new bool[] { };

                isMessageSent = new bool[] { false, false }; //11
                text.text = objectifs + " " + levelName + " ";

                text_sec.text = objectifsSecondaire;
                isGobelinActivated = false;
                isGobelinQuestDone = false;
            }

        }//fin level8
        #endregion

    }

    private void Objectif_EN()
    {

    }

    #endregion

    #region Secondaires
    private void ObjectifsSecondaires()
    {
        #region level1
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
        #endregion

        #region level2
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
        #endregion

        #region level3
        if (levelName == "Level3")
        {
            if (!isSecondaryFinished[0])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver des soldats</color></size>";

                if (player.transform.position.z >= 377 && player.transform.position.z <= 451)
                {
                    if (player.transform.position.x >= 1325 && player.transform.position.x <= 1392)
                    {
                        isSecondaryFinished[0] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver des soldats</color></size>";
            }

            if (!isSecondaryFinished[1])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver un coffre</color></size>";

                if (player.transform.position.z >= 177 && player.transform.position.z <= 197)
                {
                    if (player.transform.position.x >= 1419 && player.transform.position.x <= 1429)
                    {
                        isSecondaryFinished[1] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver un coffre</color></size>";
            }


            if (isGobelinActivated)
            {
                if (isGobelinQuestDone && isMessageSent[10])
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=red>-Trouver des armes</color></size>";
                }
                else
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=white>-Trouver des armes</color></size>";
                }
            }

            /*portes level3*/
            if (player.transform.position.z >= 42 && player.transform.position.z <= 64)
            {
                if (player.transform.position.x >= 1530 && player.transform.position.x <= 1560)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door9, 10))
                    {
                        perso.CmdOpen_door(door9, 10);
                    }

                }
            }

            if (player.transform.position.z >= 338 && player.transform.position.z <= 358)
            {
                if (player.transform.position.x >= 1303 && player.transform.position.x <= 1328)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door8, 10))
                    {
                        perso.CmdOpen_door(door8, 10);
                    }

                }
            }
            /*fin portes level3*/

        }//fin level 3
        #endregion

        #region level4
        if (levelName == "Level4")
        {
            if (!isSecondaryFinished[0])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver un autel</color></size>";

                if (player.transform.position.z >= 574 && player.transform.position.z <= 616)
                {
                    if (player.transform.position.x >= 1285 && player.transform.position.x <= 1400)
                    {
                        isSecondaryFinished[0] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver un autel</color></size>";
            }

            if (isGobelinActivated)
            {
                if (isGobelinQuestDone && isMessageSent[7])
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=red>-Atteignez le coffre</color></size>";
                }
                else
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=white>-Atteignez le coffre</color></size>";
                }
            }

            /*portes level4*/
            if (player.transform.position.z >= 900 && player.transform.position.z <= 934)
            {
                if (player.transform.position.x >= 1643 && player.transform.position.x <= 1662)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door11, 10))
                    {
                        perso.CmdOpen_door(door11, 10);
                    }

                }
            }

            if (player.transform.position.z >= 338 && player.transform.position.z <= 358)
            {
                if (player.transform.position.x >= 1303 && player.transform.position.x <= 1328)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door10, 10))
                    {
                        perso.CmdOpen_door(door10, 10);
                    }

                }
            }
            /*fin portes level4*/

        }//fin level 4
        #endregion

        #region level5
        if (levelName == "Level5")
        {
            if (!isSecondaryFinished[0])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver leur réserve de nourriture</color></size>";

                if (player.transform.position.z >= 1046 && player.transform.position.z <= 1156)
                {
                    if (player.transform.position.x >= 1721 && player.transform.position.x <= 1865)
                    {
                        isSecondaryFinished[0] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver leur réserve de nourriture</color></size>";
            }

            if (isGobelinActivated)
            {
                if (isGobelinQuestDone && isMessageSent[7])
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=red>-Trouver du KiBrille</color></size>";
                }
                else
                {
                    text_sec.text = text_sec.text + "<size=14>\n\n<color=white>-Trouver du KiBrille</color></size>";
                }
            }

            /*portes level5*/
            if (player.transform.position.z >= 1315 && player.transform.position.z <= 1338)
            {
                if (player.transform.position.x >= 1617 && player.transform.position.x <= 1638)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door12, 10))
                    {
                        perso.CmdOpen_door(door12, 10);
                    }

                }
            }

            if (player.transform.position.z >= 1425 && player.transform.position.z <= 1449)
            {
                if (player.transform.position.x >= 1909 && player.transform.position.x <= 1925)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door14, 10))
                    {
                        perso.CmdOpen_door(door14, 10);
                    }
                    if (!perso.FonctionNulleQuiRetrouneBool(door13, 10))
                    {
                        perso.CmdOpen_door(door13, 10);
                    }
                }
            }
            /*fin portes level5*/

        }//fin level 5
        #endregion

        #region level6
        if (levelName == "Level6")
        {
            if (!isSecondaryFinished[0])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver un objet à ramener à un gobelin</color></size>";

                if (player.transform.position.z >= 1114 && player.transform.position.z <= 1140)
                {
                    if (player.transform.position.x >= 2242 && player.transform.position.x <= 2259)
                    {
                        isSecondaryFinished[0] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver un objet à ramener à un gobelin</color></size>";
            }

            if (!isSecondaryFinished[1])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver un objet à ramener à un gobelin</color></size>";

                if (player.transform.position.z >= 1246 && player.transform.position.z <= 1261)
                {
                    if (player.transform.position.x >= 2395 && player.transform.position.x <= 2408)
                    {
                        isSecondaryFinished[1] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver un objet à ramener à un gobelin</color></size>";
            }

            if (!isSecondaryFinished[2])
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=white>-Trouver un objet à ramener à un gobelin</color></size>";

                if (player.transform.position.z >= 1240 && player.transform.position.z <= 1253)
                {
                    if (player.transform.position.x >= 2253 && player.transform.position.x <= 2269)
                    {
                        isSecondaryFinished[2] = true;
                    }
                }
            }
            else
            {
                text_sec.text = objectifsSecondaire + "<size=14>\n\n<color=red>-Trouver un objet à ramener à un gobelin</color></size>";
            }

            /*portes level6*/
            if (player.transform.position.z >= 1042 && player.transform.position.z <= 1063)
            {
                if (player.transform.position.x >= 2214 && player.transform.position.x <= 2247)
                {
                    if (!perso.FonctionNulleQuiRetrouneBool(door15, 10))
                    {
                        perso.CmdOpen_door(door15, 10);
                    }

                }
            }
            /*fin portes level6*/

        }//fin level 6
        #endregion
    }

    private void ObjectifsSecondaires_EN()
    {

    }

    #endregion

    #region Messages
    private void Message()
    {
        #region level1
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
        #endregion

        #region level2
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
                            if (Personnage.attack <= 10)
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

        #endregion

        #region level3
        if (levelName == "Level3")
        {
            if (!isMessageSent[0]) //porte
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 446 && player.transform.position.z <= 480)
                {
                    if (player.transform.position.x >= 1493 && player.transform.position.x <= 1552)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>La porte est bloquée !</i></color>\n<b>Radio</b> : <i>Cherche un générateur pour l'ouvrir.</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>La porte est bloquée !</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[1]) //generateur 1
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 42 && player.transform.position.z <= 64)
                {
                    if (player.transform.position.x >= 1530 && player.transform.position.x <= 1560)
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

            if (!isMessageSent[2]) //generateur 2
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 338 && player.transform.position.z <= 358)
                {
                    if (player.transform.position.x >= 1303 && player.transform.position.x <= 1328)
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

            if (!isMessageSent[3]) //autel
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
                            text_msg.text = "<b>Vous</b> : <i>Mince ... j'espère qu'ils n'ont pas trop souffert</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[4]) //camp
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 64 && player.transform.position.z <= 135)
                {
                    if (player.transform.position.x >= 1621 && player.transform.position.x <= 1698)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[4] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Je crois que j'ai trouvé un autre avant-poste</i></color>\n<b>Radio</b> : <i>Chercher aux alentours pour voir si vous ne trouvez pas quelque chose d'intéressant.</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Je crois que j'ai trouvé un autre avant-poste</i>";
                        }
                    }
                }
            }

            /*Gobelin*/
            if (!isMessageSent[5])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 416 && player.transform.position.z <= 443)
                {
                    if (player.transform.position.x >= 1596 && player.transform.position.x <= 1624)
                    {
                        if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[5] = true;
                            isGobelinActivated = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Gobelin</b> : <i>Soldat ! Tu dois avoir des armes toi ... Donne moi une arme s'il te plait... pour me défendre</i></color>\n<b>Vous</b> : <i>Je ne suis pas rassuré a l'idée de lui donner une arme ...</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>Soldat ! Tu dois avoir des armes toi ... Donne moi une arme s'il te plait... pour me défendre</i>";
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

                if (player.transform.position.z >= 272 && player.transform.position.z <= 290)
                {
                    if (player.transform.position.x >= 906 && player.transform.position.x <= 922)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[6] = true;
                            isGobelinQuestDone = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Je devrais ramener ces armes au gobelin</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[7] && isGobelinQuestDone)
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 416 && player.transform.position.z <= 443)
                {
                    if (player.transform.position.x >= 1596 && player.transform.position.x <= 1624)
                    {
                        if (currentTime + timeForNext * 4 < Time.time)
                        {
                            if (Personnage.attack <= 10)
                                Personnage.life -= 20;
                            text_msg.text = "";
                            isMessageSent[7] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>MERCI MAINTENANT MEURS !</i>";
                        }
                    }
                }
            }
            /*Gobelin*/

        }//fin level3
        #endregion

        #region level4
        if (levelName == "Level4")
        {
            if (!isMessageSent[0]) //entrée
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 479 && player.transform.position.z <= 519)
                {
                    if (player.transform.position.x >= 1510 && player.transform.position.x <= 1549)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Vous ne m'avez toujours pas donné votre nom</i></color>\n<b>Radio</b> : <i>Je m'apelle Eldarion, c'est tout ce que tu as à savoir</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Vous ne m'avez toujours pas donné votre nom</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[1]) //boss
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 730 && player.transform.position.z <= 944)
                {
                    if (player.transform.position.x >= 1414 && player.transform.position.x <= 1626)
                    {
                        if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[1] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>C'est ma mère ... tue la !</i></color>\n<b>Radio</b> : <i>Votre quoi ?! Il faudra qu'on ai une discussion vous et moi !</i>";
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>C'est quoi ce machin ?!</i></color>\n<b>Radio</b> : <i>C'est ma mère ... tue la !</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>C'est quoi ce machin ?!</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[2]) //autel
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 574 && player.transform.position.z <= 616)
                {
                    if (player.transform.position.x >= 1285 && player.transform.position.x <= 1400)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[2] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>J'ai encore trouvé une de leurs statue ...</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[3]) //generateur 1
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 900 && player.transform.position.z <= 934)
                {
                    if (player.transform.position.x >= 1643 && player.transform.position.x <= 1662)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[3] = true;
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

            if (!isMessageSent[4]) //generateur 2
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 651 && player.transform.position.z <= 687)
                {
                    if (player.transform.position.x >= 1624 && player.transform.position.x <= 1683)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[4] = true;
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

            /*Gobelin*/
            if (!isMessageSent[5])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 791 && player.transform.position.z <= 807)
                {
                    if (player.transform.position.x >= 1322 && player.transform.position.x <= 1339)
                    {
                        if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[5] = true;
                            isGobelinActivated = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>L'humain ! Je n'arrive pas à atteindre ce coffre en haut. si tu y arrives, je te donnerai quelque chose.</i>";
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

                if (player.transform.position.z >= 663 && player.transform.position.z <= 667)
                {
                    if (player.transform.position.x >= 1296 && player.transform.position.x <= 1300)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[6] = true;
                            isGobelinQuestDone = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Ce n'était pas facile...</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[7] && isGobelinQuestDone)
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 416 && player.transform.position.z <= 443)
                {
                    if (player.transform.position.x >= 1596 && player.transform.position.x <= 1624)
                    {
                        if (currentTime + timeForNext * 4 < Time.time)
                        {
                            if (Personnage.personnageSpeedWalk <= 15)
                            {
                                Personnage.personnageSpeedRun += 1;
                                Personnage.personnageSpeedWalk += 1;
                            }                                
                            text_msg.text = "";
                            isMessageSent[7] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>Bravo, prend ca, tu iras plus vite.</i>";
                        }
                    }
                }
            }
            /*Gobelin*/

        }//fin level4
        #endregion

        #region level5
        if (levelName == "Level5")
        {
            if (!isMessageSent[0]) //entrée
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 964 && player.transform.position.z <= 998)
                {
                    if (player.transform.position.x >= 1546 && player.transform.position.x <= 1582)
                    {
                        if (currentTime + timeForNext * 5 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext * 4 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Dites moi pourquoi je suis là ! Est-ce que la base est intacte ?!</i></color>\n<b>Radio</b> : <i>La base est intacte et vous êtes là pour tuer mes parents...</i>";
                        }
                        else if (currentTime + timeForNext * 3 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>Sachez juste que je suis de votre côté.</i></color>\n<b>Vous</b> : <i>Dites moi pourquoi je suis là ! Est-ce que la base est intacte ?!</i>";
                        }
                        else if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>Je suis un de ses monstres...</i></color>\n<b>Radio</b> : <i>Sachez juste que je suis de votre côté.</i>";
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Vous</b> : <i>Qui êtes vous réellement ?!</i></color>\n<b>Radio</b> : <i>Je suis un de ses monstres...</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Qui êtes vous réellement ?!</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[1]) //porte
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1284 && player.transform.position.z <= 1352)
                {
                    if (player.transform.position.x >= 1686 && player.transform.position.x <= 1700)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[1] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Encore une porte fermée... Je vais trouver un générateur</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[2]) //generateur 1
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1315 && player.transform.position.z <= 1338)
                {
                    if (player.transform.position.x >= 1617 && player.transform.position.x <= 1638)
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

            if (!isMessageSent[3]) //generateur 2
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1425 && player.transform.position.z <= 1449)
                {
                    if (player.transform.position.x >= 1909 && player.transform.position.x <= 1925)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[3] = true;
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

            if (!isMessageSent[4]) //chemin
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1172 && player.transform.position.z <= 1203)
                {
                    if (player.transform.position.x >= 1810 && player.transform.position.x <= 1839)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[4] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Je devrais suivre ce chemin...</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[5]) //nourriture
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }
                if (player.transform.position.z >= 1046 && player.transform.position.z <= 1156)
                {
                    if (player.transform.position.x >= 1721 && player.transform.position.x <= 1865)
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
                            text_msg.text = "<b>Vous</b> : <i>Mon dieu ... C'est ça leur réserve de nourriture ... J'ai pas envie de savoir ce qu'il y a dedans.</i>";
                        }
                    }
                }
            }

            /*Gobelin*/
            if (!isMessageSent[6])
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1380 && player.transform.position.z <= 1403)
                {
                    if (player.transform.position.x >= 1836 && player.transform.position.x <= 1863)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[6] = true;
                            isGobelinActivated = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>Psst. Tu te souviens de moi ? je cherche encore du KiBrille s'il te plait...</i>";
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

                if (player.transform.position.z >= 1275 && player.transform.position.z <= 1298)
                {
                    if (player.transform.position.x >= 1540 && player.transform.position.x <= 1562)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[7] = true;
                            isGobelinQuestDone = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Je devrais ramener ça au gobelin.</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[8] && isGobelinQuestDone)
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 416 && player.transform.position.z <= 443)
                {
                    if (player.transform.position.x >= 1596 && player.transform.position.x <= 1624)
                    {
                        if (currentTime + timeForNext * 4 < Time.time)
                        {
                            if (Personnage.attack <= 10)
                                Personnage.attack += 1;
                            text_msg.text = "";
                            isMessageSent[8] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>Merci, prends ça, tu tireras encore plus fort !</i>";
                        }
                    }
                }
            }
            /*Gobelin*/

        }//fin level5
        #endregion

        #region level6
        if (levelName == "Level6")
        {
            if (!isMessageSent[0]) //entrée
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1050 && player.transform.position.z <= 1100)
                {
                    if (player.transform.position.x >= 1975 && player.transform.position.x <= 2010)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>Je suis désolé d'avoir menti ...</i></color>\n<b>Vous</b> : <i>Depuis quand les monstres ont des sentiments ?</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Radio</b> : <i>Je suis désolé d'avoir menti ...</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[1]) //village monstre
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1197 && player.transform.position.z <= 1223)
                {
                    if (player.transform.position.x >= 2032 && player.transform.position.x <= 2075)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[1] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>Punaise, vaut mieux que je me fasse discret ici ...</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[2]) //village gobelin
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1147 && player.transform.position.z <= 1203)
                {
                    if (player.transform.position.x >= 2274 && player.transform.position.x <= 2314)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[2] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Vous</b> : <i>un village gobelin ...</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[3]) //generateur
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1042 && player.transform.position.z <= 1063)
                {
                    if (player.transform.position.x >= 2214 && player.transform.position.x <= 2247)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[3] = true;
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

            if (!isMessageSent[4] && isSecondaryFinished[0]) //gobelin 1
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1144 && player.transform.position.z <= 1153)
                {
                    if (player.transform.position.x >= 2350 && player.transform.position.x <= 2359)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[4] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Gobelin</b> : <i>Merci de m'avoir ramené ça !</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[5] && isSecondaryFinished[1]) //gobelin 2
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1091 && player.transform.position.z <= 1102)
                {
                    if (player.transform.position.x >= 2294 && player.transform.position.x <= 2302)
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
                            text_msg.text = "<b>Gobelin</b> : <i>Merci de m'avoir ramené ça !</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[6] && isSecondaryFinished[2]) //gobelin 3
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1077 && player.transform.position.z <= 1085)
                {
                    if (player.transform.position.x >= 2354 && player.transform.position.x <= 2364)
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
                            text_msg.text = "<b>Gobelin</b> : <i>Merci de m'avoir ramené ça !</i>";
                        }
                    }
                }
            }



        }//fin level6
        #endregion

        #region level7
        if (levelName == "Level7")
        {
            if (!isMessageSent[0]) //entrée
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 1366 && player.transform.position.z <= 1436)
                {
                    if (player.transform.position.x >= 2430 && player.transform.position.x <= 2579)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Radio</b> : <i>Tu y es presque ! COURS ! Ne te retourne surtout pas !</i>";
                        }
                    }
                }
            }

        }//fin level7
        #endregion

        #region level8
        if (levelName == "Level8")
        {
            if (!isMessageSent[0]) //sortie
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 713 && player.transform.position.z <= 782)
                {
                    if (player.transform.position.x >= 3316 && player.transform.position.x <= 3338)
                    {
                        if (currentTime + timeForNext * 2 < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[0] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "<color=grey><b>Radio</b> : <i>Désolé pour t'avoir menti et merci .... Adieu</i></color>\n<b>Vous</b> : <i>Adieu ...</i>";
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Radio</b> : <i>Désolé pour t'avoir menti et merci .... Adieu</i>";
                        }
                    }
                }
            }

            if (!isMessageSent[1]) //soldats (FIN)
            {
                if (text_msg.text == "")
                {
                    currentTime = Time.time;
                }

                if (player.transform.position.z >= 792 && player.transform.position.z <= 814)
                {
                    if (player.transform.position.x >= 3429 && player.transform.position.x <= 3468)
                    {
                        if (currentTime + timeForNext < Time.time)
                        {
                            text_msg.text = "";
                            isMessageSent[1] = true;
                            msg_img.gameObject.SetActive(false);
                        }
                        else
                        {
                            msg_img.gameObject.SetActive(true);
                            text_msg.text = "<b>Commandant</b> : <i>On nous a prévenu que vous étiez dedans, on ne pensait pas vous revoir. Bien joué</i>";
                        }
                    }
                }
            }

        }//fin level8
        #endregion
    }


    private void Message_EN()
    {

    }
    #endregion



}
