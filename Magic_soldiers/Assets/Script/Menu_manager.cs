using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_manager : MonoBehaviour {

    public void OnClickCroix()
    {
        Menu.options = false;
        Menu.Options.SetActive(false);
        Personnage_offline.sensibility = Menu.sensi;
        Personnage.sensibility = Menu.sensi;
    }

    public void OnCLickFR()
    {
        Canvas_UI_Online.langue = "fr";
        Menu.langue = "fr";
    }

    public void OnclickEN()
    {
        Canvas_UI_Online.langue = "en";
        Menu.langue = "en";
    }

}
