using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ----- ENEMY PURPLE -----
 *  
 */

public class AI_EnemyRed : MonoBehaviour
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
    public float dodgeSpeedMax;
    public float dodgeSmoothing;
    private Rigidbody rig;
    private Vector3 newPosition;
    private float targetDodge;
    private float rotationSpeed;
    private bool rotating;
    public float tiltAngle;

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        boundary.UpdateBoundaries();
        tiltAngle = 0;
    }
    void OnEnable()
    {
        StartCoroutine(Evade());
        rig.rotation = Quaternion.Euler(0f, 0f, 0f);
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
        if (rotating && Mathf.Abs(tiltAngle) < 360f)
        {
            tiltAngle += rotationSpeed * rig.velocity.x * -2;
            rig.rotation = Quaternion.Euler(0f, 0f, tiltAngle);
        } else
        {
            rotating = false;
            tiltAngle = 0;
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < 3; ++i)
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
            yield return new WaitForSeconds(fireRate);
        }
        yield break;
    }

    IEnumerator Evade()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(evasionStartWait.x, evasionStartWait.y));
            targetDodge = Random.Range(5, dodgeSpeedMax) * -Mathf.Sign(transform.position.x);
            StartCoroutine(Fire());
            rotating = true;
            rotationSpeed = Random.Range(dodgeDuration.x, dodgeDuration.y);
            yield return new WaitForSeconds(rotationSpeed);
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
