using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    public Transform player;
    public float damage;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            PlayerHealth playerTarget = player.GetComponent<PlayerHealth>();

            playerTarget.takeDamage(damage);
            
            Debug.Log("hit you");
        }
    }
}
