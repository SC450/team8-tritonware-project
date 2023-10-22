using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;

    public GameObject menuUI;
    public GameObject player;
    public GameObject placeHolderCam;

    void Update() {
        healthBar.fillAmount = health / 100;
    }

    public void takeDamage(float amount) {
        health -= amount;
        if(health <= 0f) {
            die();
        }
    }

    void die() {
        Debug.Log("Bruh... you died!");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuUI.active = true;
        player.active = false;
        placeHolderCam.active = true;

        health = 100f;
    }
}
