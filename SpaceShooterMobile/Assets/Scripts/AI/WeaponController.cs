using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform shotSpawn;
    public AudioSource shotAudio;

    public float delay;
    public float fireRate;

    void OnEnable()
    {
        InvokeRepeating("Fire", delay, fireRate);
    }

    private void OnDisable()
    {
        CancelInvoke("Fire");
    }

    void Fire()
    {
        // "Instantiate" a bullet from the bullet pool
        GameObject bullet = ObjectPool.pools.GetBasicEnemyBullet();
        if (bullet != null)
        {
            bullet.transform.position = shotSpawn.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);
            if (shotAudio) shotAudio.Play();
        }
    }
}
