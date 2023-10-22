using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject player;
    public GameObject placeHolderCam;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Pause() {
        menuUI.active = true;
        player.active = false;
        placeHolderCam.active = true;
    }
}
