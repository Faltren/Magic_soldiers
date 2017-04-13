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
}
