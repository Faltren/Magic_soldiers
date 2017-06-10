using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.SceneManagement;

public class Canvas_UI : MonoBehaviour {

    private int volume;
    private float currentTime;
    private float next_msg;

    public Text text;
    public Text text_msg;
    public Text text_sec;
    public Text text_infos;
    public Text text_pause;

    public RawImage healthBar;
    public RawImage shieldBar;

    public GameObject quit;

    private string original;
    private string originalSec;

    public int nbObjectifs;
    private bool[] objectifs;

    public int nbObjectifsSecondaires;
    private bool[] objectifsSecondaires;

    private bool[] isMsgSaid;
    private bool[] Timer;

    public static int compteur;

    private bool Tuto6;
    public static bool isPaused;

    void Start()
    {
        volume = 1;

        /*text = GameObject.Find("Objectifs").GetComponent<Text>();
        text_msg = GameObject.Find("Messages").GetComponent<Text>();
        text_sec = GameObject.Find("Secondaires").GetComponent<Text>();
        text_infos = GameObject.Find("Infos").GetComponent<Text>();
        text_pause = GameObject.Find("Pause").GetComponent<Text>();

        healthBar = GameObject.Find("Vie").GetComponent<RawImage>();
        shieldBar = GameObject.Find("Shield").GetComponent<RawImage>();

        quit = GameObject.Find("Quit");*/

        Timer = new bool[8];
        isMsgSaid = new bool[8];
        for (int k1 = 0; k1 < isMsgSaid.Length; k1++)
        {
            isMsgSaid[k1] = false;
            Timer[k1] = false;
        }

        objectifs = new bool[nbObjectifs];
        for (int i = 0; i < nbObjectifs; i++)
        {
            objectifs[i] = false;
        }

        objectifsSecondaires = new bool[nbObjectifsSecondaires];
        for (int j = 0; j < nbObjectifsSecondaires; j++)
        {
            objectifsSecondaires[j] = false;
        }

        compteur = 0;
        original = text.text;
        originalSec = text_sec.text;
        next_msg = 3;

        Tuto6 = false;
        isPaused = false;


    }
	
	
	void Update () {

        /*Placement des barres de vie/bouclier*/
        healthBar.rectTransform.sizeDelta = new Vector2(Personnage_offline.life * 2.25f, 30); //225 = 100 => 1 = 2.25
        shieldBar.rectTransform.sizeDelta = new Vector2(Personnage_offline.shield * 2.25f, 30);

        healthBar.rectTransform.transform.position = new Vector2(Personnage_offline.life * 2.25f / 2 + 37, healthBar.rectTransform.transform.position.y);
        shieldBar.rectTransform.transform.position = new Vector2(Personnage_offline.shield * 2.25f / 2 + 37, healthBar.rectTransform.transform.position.y - 55);

        /* /!\ uniquement pour soutenance 1 !*/
        if (Personnage_offline.player.transform.position.x > 560 && Personnage_offline.player.transform.position.x < 570)
        {
            if (Personnage_offline.player.transform.position.z > -625 && Personnage_offline.player.transform.position.z < -563)
            {
                Application.LoadLevel("Menu");
            }
        }
        /* /!\ uniquement pour soutenance 1 !*/



        //pause
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            quit.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            text_pause.text = "<b>PAUSE</b>";
            isPaused = true;
            Time.timeScale = 0f;
            AudioListener.volume = 0;   
                   
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            quit.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GUI.backgroundColor = Color.clear;
            text_pause.text = "";
            isPaused = false;
            Time.timeScale = 1f;
            AudioListener.volume = volume;
        }

        if (!isPaused && quit.active)
        {
            quit.SetActive(false);
        }

        Objectifs();
        ObjectifsSecondaires();
        Radio(compteur);

        if (Input.GetKey(KeyCode.Tab))
        {
            Display();
        }
        else
        {
            text.text = "";
            text_sec.text = "";
        }
        
        

	}

    private void Messages(int nbMsg)
    {
        if (Menu.langue == "fr")
        {
            switch (nbMsg)
            {
                case 0:
                    if (currentTime + next_msg * 4 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[0] = true;
                    }
                    else if (currentTime + next_msg * 3 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Salut le nouveau ! Je suis chargé de t'aider durant toute cette journée ! Alors tu vas suivre mes ordres et tout se passera bien.</i></color>\n<b>Radio</b> : <i>Tu vois les cibles dehors ? Tire leur dessus !</i>";

                    }
                    else if (currentTime + next_msg * 2 <= Time.time)
                    {
                        text_msg.text = "<color=grey><b>Vous</b> : <i>Euh bonjour...</i></color>\n<b>Radio</b> : <i>Salut le nouveau ! Je suis chargé de t'aider durant toute cette journée ! Alors tu vas suivre mes ordres et tout se passera bien.</i>";
                    }
                    else if (currentTime + next_msg <= Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Merde ... comment ça marche ce machin ?! Tu m'entends ?</i></color>\n<b>Vous</b> : <i>Euh bonjour...</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Merde ... comment ça marche ce machin ?! Tu m'entends ?</i>";
                    }
                    break;

                case 1:
                    if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[1] = true;
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Bien. Essaye de tirer en rafale maintenant !</i>";
                    }
                    break;

                case 2:
                    if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[2] = true;
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Evite de garder trop longtemps ton burst sinon tu vas encore te brûler la main...</i></color>\n<b>Radio</b> : <i>J'ai toujours pas de nouvelle de nos ouvriers dans la mine. Va voir ce qu'il se passe là bas.</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Evite de garder trop longtemps ton burst sinon tu vas encore te brûler la main...</i>";
                    }
                    break;


                case 3:
                    if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[3] = true;
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Parfait, tout va bien.</i></color>\n<b>Radio</b> : <i>On va voir ce que tu vaux le bleu. Va au champ de tir et montre moi de quoi tu es capable.</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Parfait, tout va bien.</i>";
                    }
                    break;

                case 4:
                    if (currentTime + next_msg * 2 + 2 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[4] = true;
                    }
                    else if (currentTime + next_msg + 2 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Pas mal le nouveau ! Tu m'as presque impressionné ...</i></color>\n<b>Radio</b> : <i>J'ai une première vraie mission pour toi ! On a perdu le contact avec pas mal d'hommes à l'extérieur en ce moment. Va faire une ronde là bas.</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Pas mal le nouveau ! Tu m'as presque impressionné ...</i>";
                    }
                    break;

                case 5:
                    if (currentTime + next_msg * 5 - 1 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[5] = true;
                    }
                    else if (currentTime + next_msg * 4 - 1 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Vous</b> : <i>Mais ....</i></color>\n<b>Radio</b> : <i>C'est un ordre !</i>";
                    }
                    else if (currentTime + next_msg * 3 - 1 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>NAN ! Ne reviens pas ici ils sont beaucoup trop ! Nos systèmes de communications longues portées sont morts ! Va prévenir la base principale !</i></color>\n<b>Vous</b> : <i> Mais ....</i>";
                    }
                    else if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Vous</b> : <i>Calmez-vous, j'arrive !</i></color>\n<b>Radio</b> : <i>NAN ! Ne reviens pas ici ils sont beaucoup trop ! Nos systèmes de communications longues portées sont morts ! Va prévenir la base principale !</i>";
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Merde ! On est attaqué à la base et nos batteries sont en train d'exploser !</i></color>\n<b>Vous</b> : <i>Calmez-vous, j'arrive !</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Merde ! On est attaqué à la base ! Ils sont beaucoup trop nombreux !</i>";
                    }
                    break;


                case 101:
                    if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "";
                        objectifsSecondaires[0] = true;
                        isMsgSaid[6] = true;
                        objectifsSecondaires[0] = true;
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Vous</b> : <i>Je vois des traces de pneus sur le sol.</i></color>\n<b>Radio</b> : <i>Bien reçu. Suis les et dis moi où ça te mène.</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Vous</b> : <i>Je vois des traces de pneus sur le sol.</i>";
                    }
                    break;

                case 102:
                    if (currentTime + next_msg * 4 < Time.time)
                    {
                        text_msg.text = "";
                        objectifsSecondaires[1] = true;
                        isMsgSaid[7] = true;
                        objectifsSecondaires[1] = true;
                    }
                    else if (currentTime + next_msg * 3 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Vous</b> : <i>Ils sont morts...</i></color>\n<b>Radio</b> : <i>Ils seront vengés ... Je préviens le commandant. Continue ta mission.</i>";
                    }
                    else if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Parfait, dis leur de revenir au plus vite !</i></color>\n<b>Vous</b> : <i>Ils sont morts...</i>";
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Vous</b> : <i>Mince ... Je crois que j'ai retrouvé le groupe</i></color>\n<b>Radio</b> : <i>Parfait, dis leur de revenir au plus vite !</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Vous</b> : <i>Mince ... Je crois que j'ai retrouvé le groupe</i>";
                    }
                    break;

                default:
                    text_msg.text = "";
                    break;
            }
        }
        else
        {
            switch (nbMsg)
            {
                case 0:
                    if (currentTime + next_msg * 4 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[0] = true;
                    }
                    else if (currentTime + next_msg * 3 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Hello soldier! I will be your commanding officer during this day!</i></color>\n<b>Radio</b> : <i>Shoot the target outside</i>";

                    }
                    else if (currentTime + next_msg * 2 <= Time.time)
                    {
                        text_msg.text = "<color=grey><b>You</b> : <i>Hello</i></color>\n<b>Radio</b> : <i>Hello soldier! I will be your commanding officer during this day!</i>";
                    }
                    else if (currentTime + next_msg <= Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Shit-- How does that works? Can you hear me?</i></color>\n<b>You</b> : <i>Hello</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Shit-- How does that works? Can you hear me?</i>";
                    }
                    break;

                case 1:
                    if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[1] = true;
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Good. Try to brust now</i>";
                    }
                    break;

                case 2:
                    if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[2] = true;
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Don't keep this burst too much time.</i></color>\n<b>Radio</b> : <i>I don't have any news of our workers in the mine. Go there and tell me the news</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Don't keep this burst too much time.</i>";
                    }
                    break;


                case 3:
                    if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[3] = true;
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Perfect, everything is ok.</i></color>\n<b>Radio</b> : <i>Let's see what you got. Go to the sooting and show me your skills.</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Perfect, everything is ok.</i>";
                    }
                    break;

                case 4:
                    if (currentTime + next_msg * 2 + 2 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[4] = true;
                    }
                    else if (currentTime + next_msg + 2 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Not bad</i></color>\n<b>Radio</b> : <i>I've got a first mission for you. We lost the contact with some men outside. Go there and find them.</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Not bad</i>";
                    }
                    break;

                case 5:
                    if (currentTime + next_msg * 5 - 1 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[5] = true;
                    }
                    else if (currentTime + next_msg * 4 - 1 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>You</b> : <i>But--</i></color>\n<b>Radio</b> : <i>That's an order!</i>";
                    }
                    else if (currentTime + next_msg * 3 - 1 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>No! Go get some help at our principal military base!</i></color>\n<b>You</b> : <i>But--</i>";
                    }
                    else if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>You</b> : <i>Calm down, I'm coming!</i></color>\n<b>Radio</b> : <i>No! Go get some help at our principal military base!</i>";
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Shit! We are under attack! Our battery are explodings</i></color>\n<b>You</b> : <i>Calm down, I'm coming!</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>Radio</b> : <i>Shit! We are under attack! Our battery are explodings</i>";
                    }
                    break;


                case 101:
                    if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "";
                        objectifsSecondaires[0] = true;
                        isMsgSaid[6] = true;
                        objectifsSecondaires[0] = true;
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>You</b> : <i>I see some tire tracks over here.</i></color>\n<b>Radio</b> : <i>Follow them and tell me where they go</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>You</b> : <i>I see some tire tracks over here.</i>";
                    }
                    break;

                case 102:
                    if (currentTime + next_msg * 4 < Time.time)
                    {
                        text_msg.text = "";
                        objectifsSecondaires[1] = true;
                        isMsgSaid[7] = true;
                        objectifsSecondaires[1] = true;
                    }
                    else if (currentTime + next_msg * 3 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>You</b> : <i>They are dead.</i></color>\n<b>Radio</b> : <i>They will be avenged. Continue your mission!</i>";
                    }
                    else if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Perfect, tell them to come back!</i></color>\n<b>You</b> : <i>They are dead.</i>";
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>You</b> : <i>I found the group</i></color>\n<b>Radio</b> : <i>Perfect, tell them to come back!</i>";
                    }
                    else if (currentTime < Time.time)
                    {
                        text_msg.text = "<b>You</b> : <i>I found the group</i>";
                    }
                    break;

                default:
                    text_msg.text = "";
                    break;
            }
        }
    }


    private void Radio(int nb)
    {
        //if (EditorSceneManager.GetActiveScene().name == "Tuto")
        //{
            switch (nb)
            {
                case 3:

                    if (!isMsgSaid[0])
                    {
                        if(!Timer[0])
                        {
                            currentTime = Time.time;
                            Timer[0] = true;
                        }
                        Messages(0);
                    }
                    break;

                case 4:
                    if (!isMsgSaid[1])
                    {
                        if (!Timer[1])
                        {
                            currentTime = Time.time;
                            Timer[1] = true;
                        }
                        Messages(1);
                    }
                    break;

                case 5:
                    if (!isMsgSaid[2])
                    {
                        if (!Timer[2])
                        {
                            currentTime = Time.time;
                            Timer[2] = true;
                        }
                        Messages(2);
                    }
                    break;

                case 6:
                    if (!isMsgSaid[3])
                    {
                        if (!Timer[3])
                        {
                            currentTime = Time.time;
                            Timer[3] = true;
                        }
                        Messages(3);
                    }
                    break;

                case 7:
                    if (!isMsgSaid[4])
                    {
                        if (!Timer[4])
                        {
                            currentTime = Time.time;
                            Timer[4] = true;
                        }
                        Messages(4);
                    }
                    break;

                case 8:
                    if (!isMsgSaid[5])
                    {
                        if (!Timer[5])
                        {
                            currentTime = Time.time;
                            Timer[5] = true;
                        }
                        Messages(5);
                    }   
                    break;

                case 101: //100... secondaire
                    if (!isMsgSaid[6])
                    {
                        if (!Timer[6])
                        {
                            currentTime = Time.time;
                            Timer[6] = true;
                        }
                        Messages(101);
                    }
                    
                    break;

                case 102:
                    if (!isMsgSaid[7])
                    {
                        if (!Timer[7])
                        {
                            currentTime = Time.time;
                            Timer[7] = true;
                        }
                        Messages(102);
                    }
                    break;
            }
        //}

    }


    private void Display()
    {
        //if (EditorSceneManager.GetActiveScene().name == "Tuto")
        //{

        if (Menu.langue == "fr")
        {
            switch (compteur)
            {
                case 0:
                    text.text = original + "<size=11>\n\n\n<color=white>- Deplacez vous</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 1:
                    text.text = original + "<size=11>\n\n\n<color=red>- Deplacez vous</color></size>" + "<size=11><color=white>\n\n- Regardez autour de vous</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 2:
                    text.text = original + "<size=11>\n\n<color=red>- Regardez autour de vous</color></size>" + "<size=11><color=white>\n\n- Cherchez le coffre autour de vous et ouvrez le</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 3:
                    text.text = original + "<size=11>\n\n<color=red>- Cherchez le coffre autour de vous et ouvrez le</color></size>" + "<size=11><color=white>\n\n- Tirez sur les cibles</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 4:
                    text.text = original + "<size=11>\n\n<color=red>- Tirez sur les cibles dehors</color></size>" + "<size=11><color=white>\n\n- Tester le burst sur les cibles</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 5:
                    text.text = original + "<size=11>\n\n<color=red>- Tester le burst sur les cibles</color></size>" + "<size=11><color=white>\n\n- Aller voir si tout se passe bien à la mine</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 6:
                    text.text = original + "<size=11>\n\n<color=red>- Aller voir si tout se passe bien à la mine</color></size>" + "<size=11><color=white>\n\n- Aller vous entrainer à tirer</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 7:
                    text.text = original + "<size=11><color=red>\n\n- Aller vous entrainer à tirer</color></size>" + "<size=11><color=white>\n\n- Aller patrouiller autour de la base </color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 8:
                    text.text = original + "<size=11><color=red>\n\n- Aller patrouiller autour de la base </color></size>" + "<size=11><color=white>\n\n- Fuyez pour prévenir la base principale </color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                default:
                    text.text = "";
                    text_sec.text = "";
                    text_msg.text = "";
                    text_infos.text = "";
                    break;

            }


            //objectifs secondaires
            if (compteur >= 7)
            {
                if (!objectifsSecondaires[0])
                {
                    text_sec.text = originalSec + "<size=10><color=white>\n\n- Trouver les soldats perdus</color>\n\n- <color=white>Trouver le deuxième coffre secret</color></size>";
                }
                else if (!objectifsSecondaires[1])
                {
                    text_sec.text = originalSec + "<size=10><color=white>\n\n- Suivre les traces de pneus\n\n- Trouver le deuxième coffre secret</color></size>";
                }
                else if (!objectifsSecondaires[2])
                {
                    text_sec.text = originalSec + "<size=10><color=red>\n\n- Suivre les traces de pneus</color>\n\n- Trouver le deuxième coffre secret</size>";
                }
                else
                {
                    text_sec.text = originalSec + "<size=10><color=red>\n\n- Suivre les traces de pneus\n\n- Trouver le deuxième coffre secret</color></size>";
                }
            }
            else
            {
                text_sec.text = "";
            }
        }
        else
        {
            switch (compteur)
            {
                case 0:
                    text.text = original + "<size=11>\n\n\n<color=white>- Move</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 1:
                    text.text = original + "<size=11>\n\n\n<color=red>- Move</color></size>" + "<size=11><color=white>\n\n- Look around you</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 2:
                    text.text = original + "<size=11>\n\n<color=red>- Look around you</color></size>" + "<size=11><color=white>\n\n- Find the chest and open it</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 3:
                    text.text = original + "<size=11>\n\n<color=red>- Find the chest and open it</color></size>" + "<size=11><color=white>\n\n- Shoot the target</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 4:
                    text.text = original + "<size=11>\n\n<color=red>- Shoot the target</color></size>" + "<size=11><color=white>\n\n- Try to burst on the target</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 5:
                    text.text = original + "<size=11>\n\n<color=red>- Try to burst on the target</color></size>" + "<size=11><color=white>\n\n- Go to the mine to get news</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 6:
                    text.text = original + "<size=11>\n\n<color=red>- Go to the mine to get news</color></size>" + "<size=11><color=white>\n\n- Go to the shooting range</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 7:
                    text.text = original + "<size=11><color=red>\n\n- Go to the shooting range</color></size>" + "<size=11><color=white>\n\n- Go patrol around the base</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                case 8:
                    text.text = original + "<size=11><color=red>\n\n- Go patrol around the base</color></size>" + "<size=11><color=white>\n\n- Run away</color></size>";
                    text.lineSpacing = 0.8f;
                    break;

                default:
                    text.text = "";
                    text_sec.text = "";
                    text_msg.text = "";
                    text_infos.text = "";
                    break;

            }

            if (Menu.langue == "fr")
            {
                //objectifs secondaires
                if (compteur >= 7)
                {
                    if (!objectifsSecondaires[0])
                    {
                        text_sec.text = originalSec + "<size=10><color=white>\n\n- Trouver les soldats perdus</color>\n\n- <color=white>Trouver le deuxième coffre secret</color></size>";
                    }
                    else if (!objectifsSecondaires[1])
                    {
                        text_sec.text = originalSec + "<size=10><color=white>\n\n- Suivre les traces de pneus\n\n- Trouver le deuxième coffre secret</color></size>";
                    }
                    else if (!objectifsSecondaires[2])
                    {
                        text_sec.text = originalSec + "<size=10><color=red>\n\n- Suivre les traces de pneus</color>\n\n- Trouver le deuxième coffre secret</size>";
                    }
                    else
                    {
                        text_sec.text = originalSec + "<size=10><color=red>\n\n- Suivre les traces de pneus\n\n- Trouver le deuxième coffre secret</color></size>";
                    }
                }
                else
                {
                    text_sec.text = "";
                }
            }
            else
            {
                //objectifs secondaires
                if (compteur >= 7)
                {
                    if (!objectifsSecondaires[0])
                    {
                        text_sec.text = originalSec + "<size=10><color=white>\n\n- Find the lost soldiers</color>\n\n- <color=white>Find the second chest</color></size>";
                    }
                    else if (!objectifsSecondaires[1])
                    {
                        text_sec.text = originalSec + "<size=10><color=white>\n\n- Follow the tire tracks\n\n- Find the second chest</color></size>";
                    }
                    else if (!objectifsSecondaires[2])
                    {
                        text_sec.text = originalSec + "<size=10><color=red>\n\n- Follow the tire tracks</color>\n\n- Find the second chest</size>";
                    }
                    else
                    {
                        text_sec.text = originalSec + "<size=10><color=red>\n\n- Follow the tire tracks\n\n- Find the second chest</color></size>";
                    }
                }
                else
                {
                    text_sec.text = "";
                }
            }

            
        }

            
            
        //}

    }


    private void Objectifs()
    {
        //if (EditorSceneManager.GetActiveScene().name == "Tuto")
        //{
            if (objectifs[compteur])
            {
                compteur++;
            }

        if (Menu.langue == "fr")
        {
            switch (compteur)
            {
                case 0:
                    text_infos.text = "<b><i>Appuyez sur W A S D pour vous déplacer et sur Tab pour afficher les objectifs</i></b>";
                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                        objectifs[compteur] = true;
                    break;

                case 1:
                    text_infos.text = "<b><i>Bouger votre souris pour regarder autour de vous</i></b>";
                    if (Input.GetAxisRaw("Mouse X") != 0f || Input.GetAxisRaw("Mouse Y") != 0f)
                        objectifs[compteur] = true;
                    break;

                case 2:
                    text_infos.text = "<b><i>Vous pouvez interagir avec des objets et des PNJ en appuyant sur E</i></b>";
                    if (Personnage_offline.player.transform.position.x > -46 && Personnage_offline.player.transform.position.x < -42)
                    {
                        if (Personnage_offline.player.transform.position.z > 94 && Personnage_offline.player.transform.position.z < 98)
                        {
                            if (Input.GetKey(KeyCode.E))
                            {
                                objectifs[compteur] = true;
                            }
                        }
                    }

                    break;

                case 3:
                    text_infos.text = "<b><i>Faites un Clique Gauche pour tirer</i></b>";
                    if (Input.GetKey(KeyCode.Mouse0))
                        objectifs[compteur] = true;
                    break;

                case 4:

                    if (Input.GetKey(KeyCode.Mouse1))
                        text_infos.text = "";
                    else
                        text_infos.text = "<b><i>Faites un Clique Droit pour tirer en rafale</i></b>";
                    if (BalleTir_offline.isSurchauffe)
                        objectifs[compteur] = true;
                    break;

                case 5:
                    if (Input.GetKey(KeyCode.LeftShift))
                        text_infos.text = "";
                    else
                        text_infos.text = "<b><i>Appuyez sur Maj Gauche pour sprinter</i></b>";

                    if (Personnage_offline.player.transform.position.z > -19 && Personnage_offline.player.transform.position.z < 15)
                    {
                        if (Personnage_offline.player.transform.position.x < -111 && Personnage_offline.player.transform.position.x > -117)
                        {
                            objectifs[compteur] = true;
                        }
                    }
                    break;

                case 6:
                    text_infos.text = "";
                    if (entrainActivate.IsActivated)
                        Tuto6 = true;
                    if (Tuto6 && !entrainActivate.IsActivated)
                        objectifs[compteur] = true;
                    break;

                case 7:
                    if (Personnage_offline.player.transform.position.z > -100 && Personnage_offline.player.transform.position.z < -25)
                        if (Personnage_offline.player.transform.position.x > 395 && Personnage_offline.player.transform.position.x < 427)
                            objectifs[compteur] = true;
                    break;

                case 8:
                    break;


                default:
                    break;
            }
        }
        else
        {
            switch (compteur)
            {
                case 0:
                    text_infos.text = "<b><i>Press W A S D to move and Tab To show the objective</i></b>";
                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                        objectifs[compteur] = true;
                    break;

                case 1:
                    text_infos.text = "<b><i>Move your mouse to look around you</i></b>";
                    if (Input.GetAxisRaw("Mouse X") != 0f || Input.GetAxisRaw("Mouse Y") != 0f)
                        objectifs[compteur] = true;
                    break;

                case 2:
                    text_infos.text = "<b><i>You can interact by pressing E</i></b>";
                    if (Personnage_offline.player.transform.position.x > -46 && Personnage_offline.player.transform.position.x < -42)
                    {
                        if (Personnage_offline.player.transform.position.z > 94 && Personnage_offline.player.transform.position.z < 98)
                        {
                            if (Input.GetKey(KeyCode.E))
                            {
                                objectifs[compteur] = true;
                            }
                        }
                    }

                    break;

                case 3:
                    text_infos.text = "<b><i>Left click to shoot</i></b>";
                    if (Input.GetKey(KeyCode.Mouse0))
                        objectifs[compteur] = true;
                    break;

                case 4:

                    if (Input.GetKey(KeyCode.Mouse1))
                        text_infos.text = "";
                    else
                        text_infos.text = "<b><i>Right click to burst</i></b>";
                    if (BalleTir_offline.isSurchauffe)
                        objectifs[compteur] = true;
                    break;

                case 5:
                    if (Input.GetKey(KeyCode.LeftShift))
                        text_infos.text = "";
                    else
                        text_infos.text = "<b><i>Press Left Maj to Sprint</i></b>";

                    if (Personnage_offline.player.transform.position.z > -19 && Personnage_offline.player.transform.position.z < 15)
                    {
                        if (Personnage_offline.player.transform.position.x < -111 && Personnage_offline.player.transform.position.x > -117)
                        {
                            objectifs[compteur] = true;
                        }
                    }
                    break;

                case 6:
                    text_infos.text = "";
                    if (entrainActivate.IsActivated)
                        Tuto6 = true;
                    if (Tuto6 && !entrainActivate.IsActivated)
                        objectifs[compteur] = true;
                    break;

                case 7:
                    if (Personnage_offline.player.transform.position.z > -100 && Personnage_offline.player.transform.position.z < -25)
                        if (Personnage_offline.player.transform.position.x > 395 && Personnage_offline.player.transform.position.x < 427)
                            objectifs[compteur] = true;
                    break;

                case 8: 
                    break;


                default:
                    break;
            }
        }
       // }
    }



    private void ObjectifsSecondaires()
    {
       // if (EditorSceneManager.GetActiveScene().name == "Tuto")
        //{
            if (compteur >= 7)
            {
                if (Personnage_offline.player.transform.position.z > -200 && Personnage_offline.player.transform.position.z < -160)
                {
                    if (Personnage_offline.player.transform.position.x < 130 && Personnage_offline.player.transform.position.x > -106)
                    {
                        if (!objectifsSecondaires[0])
                        {
                            Radio(101);
                        }
                    }
                }

                else if (Personnage_offline.player.transform.position.z > -153 && Personnage_offline.player.transform.position.z < -108)
                {
                    if (Personnage_offline.player.transform.position.x > -445 && Personnage_offline.player.transform.position.x < -368)
                    {
                        if (!objectifsSecondaires[1])
                        {
                            Radio(102);
                        }
                    }
                }

                else if (Personnage_offline.player.transform.position.z > 91 && Personnage_offline.player.transform.position.z < 98)
                {
                    if (Personnage_offline.player.transform.position.x > -1063 && Personnage_offline.player.transform.position.x < -1055)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            objectifsSecondaires[2] = true;
                        }

                    }
                }
                                   
            }
        //}

    }




}
