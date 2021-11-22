using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ----- ENEMY PURPLE -----
 *  
 */

public class AI_EnemyGreen : MonoBehaviour
{
    [Header("Score")]
    public int scoreValue;
    private GameController gameController;

    [Header("Shooting")]
    public Transform shotSpawn;
    public AudioSource shotAudio;
    public float delay;
    public float fireRate;

    [Header("Movement")]
    public MapBoundary boundary;
    public Vector2 evasionStartWait;
    public Vector2 dodgeDuration;
    public float shipTilt;
    public float dodgeSpeedMax;
    public float dodgeSmoothing;
    private float targetDodge;
    private Rigidbody rig;
    private Vector3 newPosition;

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        boundary.UpdateBoundaries();
    }
    void OnEnable()
    {
        InvokeRepeating("Fire", delay, fireRate);
        StartCoroutine(Evade());
    }

    void OnDisable()
    {
        CancelInvoke("Fire");
        targetDodge = 0;
        rig.rotation = Quaternion.identity;
        rig.velocity = new Vector3(0f, 0f, rig.velocity.z);
    }

    private void FixedUpdate()
    {
        // Apply maneuvering position
        float newManeuver = Mathf.MoveTowards(rig.velocity.x, targetDodge, Time.deltaTime * dodgeSmoothing);
        rig.velocity = new Vector3(newManeuver, 0f, rig.velocity.z);

        // Clamp ship position to map boundaries
        newPosition.Set(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 0f, rig.position.z);
        rig.position = newPosition;

        // Set ship rotation to movement
        rig.rotation = Quaternion.Euler(0f, 0f, -rig.velocity.x * shipTilt);

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

    IEnumerator Evade()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(evasionStartWait.x, evasionStartWait.y));
            targetDodge = Random.Range(1, dodgeSpeedMax) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(dodgeDuration.x, dodgeDuration.y));
            targetDodge = 0;
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
