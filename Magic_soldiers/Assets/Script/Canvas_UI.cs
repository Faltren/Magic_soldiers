using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Canvas_UI : MonoBehaviour {

    private Canvas can;
    private Text text;

    private string original;

    public int nbObjectifs;
    private bool[] objectifs;

    public int nbObjectifsSecondaires;
    private bool[] objectifsSecondaires;

    private int compteur;

    private bool Tuto6;

    void Start()
    {
        can = GetComponent<Canvas>();
        text = GetComponentInChildren<Text>();

        original = text.text;

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

        Tuto6 = false;
    }
	
	
	void Update () {

        Objectifs();

        


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







}
