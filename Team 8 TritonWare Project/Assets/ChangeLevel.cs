using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public void changeToLevel1() {
        SceneManager.LoadScene(1);
        GameObject levelHandler = GameObject.Find("LevelHandler");
        LevelHandler levelHandlerScript = levelHandler.GetComponent<LevelHandler>();

        levelHandlerScript.goToLevel1();
    }

    public void changeToLevel2() {
        SceneManager.LoadScene(1);
        GameObject levelHandler = GameObject.Find("LevelHandler");
        LevelHandler levelHandlerScript = levelHandler.GetComponent<LevelHandler>();

        levelHandlerScript.goToLevel2();
    }

    public void changeToLevel3() {
        SceneManager.LoadScene(1);
        GameObject levelHandler = GameObject.Find("LevelHandler");
        LevelHandler levelHandlerScript = levelHandler.GetComponent<LevelHandler>();

        levelHandlerScript.goToLevel3();
    }
}
