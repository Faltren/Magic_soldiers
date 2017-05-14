using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class QuitManager : NetworkBehaviour
{

    /*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    public void OnQuit()
    {
       Destroy(GameObject.Find("LobbyManager"));
        SceneManager.LoadScene("Lobby_netWork");
        Network.Disconnect();
        NetworkClient.ShutdownAll();
    }

}
