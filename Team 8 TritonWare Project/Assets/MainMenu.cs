using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject player;
    public GameObject placeHolderCam;

    public void PlayGame() {
        menuUI.active = false;
        placeHolderCam.active = false;
        player.active = true;
    }

    public void ExitGame() {
        Debug.Log("Player has exited the game.");
        Application.Quit();
    }
}
