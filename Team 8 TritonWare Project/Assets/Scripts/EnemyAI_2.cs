using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_2 : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletSpeed;
    public float attackRange;
    public ParticleSystem muzzleFlash;
    public LayerMask whatIsPlayer;

    [SerializeField] private float timer = 5;
    private float bulletTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, attackRange, whatIsPlayer)) {
            enemy.LookAt(player);

            enemy.transform.eulerAngles = new Vector3
            (
                0,
                enemy.transform.eulerAngles.y,
                enemy.transform.eulerAngles.z
            );

            ShootAtPlayer();
        }
    }

    // Shoot bullet at player
    void ShootAtPlayer() {
        bulletTime -= Time.deltaTime;

        if(bulletTime > 0) {
            return;
        }

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * bulletSpeed);
        muzzleFlash.Play();
        if(bulletObj != null) {
            Destroy(bulletObj, 5f);
        }
        Debug.Log("Fired Bullet!");
    }
}
