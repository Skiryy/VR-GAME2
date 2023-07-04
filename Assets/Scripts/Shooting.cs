using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float bulletDelay = 3.0f;
    public InputActionProperty gunShooting;
    public float triggerPressed;
    public float raycastRange = 100f;
    public LayerMask enemyLayerMask;
    public ParticleSystem muzzleFlash;
    AudioSource m_shootingSound;

    private float lastShotTime = -Mathf.Infinity;
    void Start()
    {
        m_shootingSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        triggerPressed = gunShooting.action.ReadValue<float>();
        if (triggerPressed > 0 && Time.time >= lastShotTime + bulletDelay)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }
    private void Shoot(){
        RaycastHit hit;
        muzzleFlash.Play();
        m_shootingSound.Play();

        if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, raycastRange, enemyLayerMask))
        {
            Rat_Enemy enemy = hit.collider.GetComponent<Rat_Enemy>();
            Bug_Enemy enemy2 = hit.collider.GetComponent<Bug_Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(50);
            }
            if (enemy2 != null)
            {
                enemy2.TakeDamage(50);
            }
            lastShotTime = Time.time;
        }
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

    }
}