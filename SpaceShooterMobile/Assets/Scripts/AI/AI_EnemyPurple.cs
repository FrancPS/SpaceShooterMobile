using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ----- ENEMY PURPLE -----
 *  
 */

public class AI_EnemyPurple : MonoBehaviour
{
    public int scoreValue;
    private GameController gameController;

    public Transform shotSpawn;
    public AudioSource shotAudio;

    public float delay;
    public float fireRate;

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    void OnEnable()
    {
        InvokeRepeating("Fire", delay, fireRate);
    }

    void OnDisable()
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            // Destroy bullet
            other.gameObject.SetActive(false);

            // Destroy this asteroid
            gameObject.SetActive(false);

            // Trigger explosion
            GameObject explosion = ObjectPool.pools.GetEnemyExplosion();
            if (explosion != null)
            {
                explosion.transform.position = transform.position;
                explosion.transform.rotation = Quaternion.identity;
                explosion.SetActive(true);
            }

            // Add score
            gameController.AddScore(scoreValue);
        }
    }
}
