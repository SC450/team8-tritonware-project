using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;

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
    }
}
