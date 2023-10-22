using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    // Player
    public Transform player;

    // Level Spawns
    public Transform Level1Start;
    public Transform Level2Start;
    public Transform Level3Start;

    // Level Areas
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;

    // Player Variables
    public GameObject menuUI;
    public GameObject placeHolderCam;
    public GameObject playerChar;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void goToLevel1() {
        Level1.active = true;
        Level2.active = false;
        Level3.active = false;
        player.transform.position = Level1Start.transform.position;

        startLevel();
    }

    public void goToLevel2() {
        Level1.active = false;
        Level2.active = true;
        Level3.active = false;
        player.transform.position = Level2Start.transform.position;

        startLevel();
    }

    public void goToLevel3() {
        Level1.active = false;
        Level2.active = false;
        Level3.active = true;
        player.transform.position = Level3Start.transform.position;

        startLevel();
    }

    public void startLevel() {
        menuUI.active = false;
        placeHolderCam.active = false;
        playerChar.active = true;
    }
}
