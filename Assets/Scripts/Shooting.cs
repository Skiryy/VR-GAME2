using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float bulletDelay = 5.0f; 
    public InputActionProperty gunShooting;
    public float triggerPressed;

    private float lastShotTime = -Mathf.Infinity;

    private void Update()
    {
        if (Time.time - lastShotTime >= bulletDelay)
        {
            triggerPressed = gunShooting.action.ReadValue<float>();
            if (triggerPressed > 0)
            {
                Debug.Log(triggerPressed);
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                lastShotTime = Time.time;
            }
        }
    }
}