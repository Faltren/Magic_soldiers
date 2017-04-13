using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyTopPanel : MonoBehaviour
    {
        public bool isInGame = false;

        protected bool isDisplayed = true;
        protected Image panelImage;

        public Button BackButton;
        public Button Menu;

        private bool escaped;

        void Start()
        {
            escaped = false;
            panelImage = GetComponent<Image>();
        }


        void Update()
        {
            if (!isInGame)
                return;

            if (SceneManager.GetActiveScene().name == "Lobby_netWork" || escaped)
            {
                //Menu.gameObject.SetActive(true);
            }
            else if (!escaped)
            {
                Menu.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escaped = true;
                ToggleVisibility(!isDisplayed); 
                BackButton.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && Menu.gameObject.active)
            {
                escaped = false;
                ToggleVisibility(!isDisplayed);
                BackButton.gameObject.SetActive(false);
            }

        }

        public void ToggleVisibility(bool visible)
        {
            isDisplayed = visible;
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(isDisplayed);
            }

            if (panelImage != null)
            {
                panelImage.enabled = isDisplayed;
            }
        }
    }
}