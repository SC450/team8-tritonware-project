using UnityEngine;

public class Target : MonoBehaviour {
    public float health = 100f;

    public void takeDamage(float amount) {
        health -= amount;
        if(health <= 0f) {
            die();
        }
    }

    void die() {
        Destroy(gameObject);
    }
}
