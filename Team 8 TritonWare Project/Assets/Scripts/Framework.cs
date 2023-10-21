using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Framework : MonoBehaviour {
    // Initialize Text Fields and GUI
    [SerializeField] TextMeshProUGUI currentAmmoText;
    public Image crosshair;

    // Gun Variables
    public float damage = 40f;
    public float range = 100f;

    public float maxAmmo = 10f;
    public float reserveAmmo = 45f;
    private float currentAmmo;
    public int reloadTime = 3;
    private bool isReloading = false;

    // Game Variables
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;

    void Start() {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update() {
        if(isReloading) {
            return;
        }

        if(currentAmmo <= 0) {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }

        if(Input.GetKey(KeyCode.Mouse1) && isReloading == false) {
            animator.SetBool("ADSing", true);
            crosshair.enabled = false;
        } else {
            animator.SetBool("ADSing", false);
            crosshair.enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) {
            StartCoroutine(Reload());
            return;
        }

        currentAmmoText.text = currentAmmo.ToString() + "|" + maxAmmo.ToString();
    }

    IEnumerator Reload() {
        isReloading = true;
        animator.SetBool("ADSing", false);
        Debug.Log("Reloading ammo... yes...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot() {
        muzzleFlash.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

            if(target != null) {
                target.takeDamage(damage);
            }

            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGameObject, 2f);
        }
    }
}
