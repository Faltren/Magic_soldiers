﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Canvas_UI : MonoBehaviour {

    private float currentTime;
    private float next_msg;

    private Text text;
    private Text text_msg;
    private Text text_sec;

    private string original;
    private string originalSec;

    public int nbObjectifs;
    private bool[] objectifs;

    public int nbObjectifsSecondaires;
    private bool[] objectifsSecondaires;

    private bool[] isMsgSaid;
    private bool[] Timer;

    private int compteur;

    private bool Tuto6;

    void Start()
    {
        text = GameObject.Find("Objectifs").GetComponent<Text>();
        text_msg = GameObject.Find("Messages").GetComponent<Text>();
        text_sec = GameObject.Find("Secondaires").GetComponent<Text>();



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
    }
	
	
	void Update () {

        Objectifs();
        ObjectifsSecondaires();
        Radio(compteur);        

	}



    private void Messages(int nbMsg)
    {
        if (EditorSceneManager.GetActiveScene().name == "Tuto")
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
                    else if(currentTime < Time.time)
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
                    if (currentTime + next_msg * 3 < Time.time)
                    {
                        text_msg.text = "";
                        isMsgSaid[5] = true;
                    }
                    else if (currentTime + next_msg * 2 < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Vous</b> : <i>Calmez-vous, j'arrive !</i></color>\n<b>Radio</b> : <i>Ne reviens pas ici ils sont beaucoup trop ! Cours chercher de l'aide à la base principale ! On va essayer de le tenir ici !</i>";
                    }
                    else if (currentTime + next_msg < Time.time)
                    {
                        text_msg.text = "<color=grey><b>Radio</b> : <i>Merde ! On est attaqué à la base ! Ils sont beaucoup trop nombreux !</i></color>\n<b>Vous</b> : <i>Calmez-vous, j'arrive !</i>";
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
    }


    private void Radio(int nb)
    {
        if (EditorSceneManager.GetActiveScene().name == "Tuto")
        {
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
        }

    }


    private void Objectifs()
    {
        if (EditorSceneManager.GetActiveScene().name == "Tuto")
        {
            if (objectifs[compteur])
            {
                compteur++;
            }

            switch (compteur)
            {
                case 0:
                    text.text = original + "<size=11>\n\n\n<color=white>- Deplacez vous</color></size>";
                    text.lineSpacing = 0.4f;
                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                        objectifs[compteur] = true;
                    break;

                case 1:
                    text.text = original + "<size=11>\n\n\n<color=red>- Deplacez vous</color></size>" + "<size=11><color=white>\n\n- Regardez autour de vous</color></size>";
                    text.lineSpacing = 0.4f;
                    if (Input.GetAxisRaw("Mouse X") != 0f || Input.GetAxisRaw("Mouse Y") != 0f)
                        objectifs[compteur] = true;
                    break;

                case 2:
                    text.text = original + "<size=11>\n\n<color=red>- Regardez autour de vous</color></size>" + "<size=11><color=white>\n\n- Cherchez le coffre autour de vous et ouvrez le</color></size>";
                    text.lineSpacing = 0.6f;
                    if (Personnage.player.transform.position.x > -46 && Personnage.player.transform.position.x < -42)
                    {
                        if (Personnage.player.transform.position.z > 94 && Personnage.player.transform.position.z < 98)
                        {
                            if (Input.GetKey(KeyCode.E))
                            {
                                objectifs[compteur] = true;
                            }
                        }  
                    }
                        
                    break;

                case 3:
                    text.text = original + "<size=11>\n\n<color=red>- Cherchez le coffre autour de vous et ouvrez le</color></size>" + "<size=11><color=white>\n\n- Tirez sur les cibles</color></size>";
                    text.lineSpacing = 0.6f;
                    if (Input.GetKey(KeyCode.Mouse0))
                        objectifs[compteur] = true;
                    break;

                case 4:
                    text.text = original + "<size=11>\n\n<color=red>- Tirez sur les cibles dehors</color></size>" + "<size=11><color=white>\n\n- Tester le burst sur les cibles</color></size>";
                    text.lineSpacing = 0.6f;
                    if (BalleTir.isSurchauffe)
                        objectifs[compteur] = true;
                    break;

                case 5:
                    text.text = original + "<size=11>\n\n<color=red>- Tester le burst sur les cibles</color></size>" + "<size=11><color=white>\n\n- Aller voir si tout se passe bien à la mine</color></size>";
                    text.lineSpacing = 0.6f;
                    if (Personnage.player.transform.position.z > -19 && Personnage.player.transform.position.z < 15)
                    {
                        if (Personnage.player.transform.position.x < -111 && Personnage.player.transform.position.x > -117)
                        {
                            objectifs[compteur] = true;
                        }
                    }
                    text.lineSpacing = 0.6f;
                    break;

                case 6:
                    text.text = original + "<size=11>\n\n<color=red>- Aller voir si tout se passe bien à la mine</color></size>" + "<size=11><color=white>\n\n- Aller vous entrainer à tirer</color></size>";
                    text.lineSpacing = 0.6f;
                    if (entrainActivate.IsActivated)
                        Tuto6 = true;
                    if (Tuto6 && !entrainActivate.IsActivated)
                        objectifs[compteur] = true;
                    text.lineSpacing = 0.6f;
                    break;

                case 7:
                    text.text = original + "<size=11><color=red>\n\n- Aller vous entrainer à tirer</color></size>" + "<size=11><color=white>\n\n- Aller patrouiller autour de la base </color></size>";
                    text.lineSpacing = 0.6f;
                    if (Personnage.player.transform.position.z > -100 && Personnage.player.transform.position.z < -25)
                        if (Personnage.player.transform.position.x > 395 && Personnage.player.transform.position.x < 427)
                            objectifs[compteur] = true;
                    text.lineSpacing = 0.6f;
                    break;

                case 8:
                    text.text = original + "<size=11><color=red>\n\n- Aller patrouiller autour de la base </color></size>" + "<size=11><color=white>\n\n- Fuyez</color></size>";
                    text.lineSpacing = 0.6f;
                    text.lineSpacing = 0.6f;
                    break;


                default:
                    break;


            }
        }
    }



    private void ObjectifsSecondaires()
    {
        if (EditorSceneManager.GetActiveScene().name == "Tuto")
        {
            if (compteur >= 7)
            {
                if (Personnage.player.transform.position.z > -200 && Personnage.player.transform.position.z < -160)
                {
                    if (Personnage.player.transform.position.x < 156 && Personnage.player.transform.position.x > 130)
                    {
                        if (!objectifsSecondaires[0])
                        {
                            Radio(101);
                            text_sec.text = originalSec + "<size=10><color=white>\n\n- Suivre les traces de pneus\n- Trouver le deuxième coffre secret</color></size>";
                        }
                    }
                }

                else if (Personnage.player.transform.position.z > -153 && Personnage.player.transform.position.z < -108)
                {
                    if (Personnage.player.transform.position.x > -445 && Personnage.player.transform.position.x < -368)
                    {
                        if (!objectifsSecondaires[1])
                        {
                            Radio(102);
                            text_sec.text = originalSec + "<size=10><color=red>\n\n- Trouver les soldats perdus</color>\n- <color=white>Trouver le deuxième coffre secret</color></size>";
                        }
                    }
                }

                else if (Personnage.player.transform.position.z > 91 && Personnage.player.transform.position.z < 98)
                {
                    if (Personnage.player.transform.position.x > -1063 && Personnage.player.transform.position.x < -1055)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            objectifsSecondaires[2] = true;
                        }
                        if (objectifsSecondaires[2])
                        {
                            text_sec.text = originalSec + "<size=10><color=red>\n\n- Trouver les soldats perdus\n- Trouver le deuxième coffre secret</color></size>";
                        }
                        else
                        {
                            text_sec.text = originalSec + "<size=10><color=red>\n\n- Suivre les traces de pneus</color>\n- Trouver le deuxième coffre secret</size>";
                        }  
                    }
                }
                else
                {
                    if (!objectifsSecondaires[0])
                    {
                        text_sec.text = originalSec + "<size=10><color=white>\n\n- Trouver les soldats perdus\n- Trouver le deuxième coffre secret</color></size>";
                    }
                    else if (!objectifsSecondaires[1])
                    {
                        text_sec.text = originalSec + "<size=10><color=white>\n\n- Suivre les traces de pneus\n- Trouver le deuxième coffre secret</color></size>";
                    }
                    else if (!objectifsSecondaires[2])
                    {
                        text_sec.text = originalSec + "<size=10><color=red>\n\n- Suivre les traces de pneus</color>\n-<color=white> Trouver le deuxième coffre secret</color></size>";
                    }
                    else
                    {
                        text_sec.text = originalSec + "<size=10><color=red>\n\n- Suivre les traces de pneus\n- Trouver le deuxième coffre secret</color></size>";
                    }

                }
                                   
            }
            else
            {
                text_sec.text = "";
            }
        }

    }




}
