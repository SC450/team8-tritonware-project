using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform player;
    public float damage;
    private float timeSinceFire = 0;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            
            Debug.Log("Hit Player!");

            PlayerHealth playerTarget = player.GetComponent<PlayerHealth>();

            if(playerTarget != null) {
                playerTarget.takeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
