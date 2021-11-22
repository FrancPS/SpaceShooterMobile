using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/* ----- Player Controller -----
 *  This script must be attached to and only to the Player game object in the scene.
 *  
 *  Control the player input and perform the appropiate actions.
 *  - Move the player ship from input axis
 *  - Shoot bullets
 */

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    [Header("Movement")]
    public float shipSpeed;
    public float shipTiltAngle;
    public MapBoundary boundary;
    private Rigidbody rig;
    private Vector3 movement;
    private Vector3 newPosition;

    [Header("Combat")]
    public Transform shotSpawn;
    public GameObject deathParticlesObject;
    public float fireRate;
    private float nextFireTime;

    [Header("HP control")]
    public LivesDisplayer livesDisplayer;
    public int totalLives;

    [Header("Audios")]
    public AudioSource shotAudio;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        movement = new Vector3(0f, 0f, 0f);
        nextFireTime = Time.time;
    }

    void Start()
    {
        boundary.UpdateBoundaries();
        totalLives -= SettingsManager.Inst.stData.difficulty; // initial lives 5 - difficulty level;
        livesDisplayer.InstantiateLivesIcons(totalLives);

    }

    void FixedUpdate() // Phisics Update
    {
        // Get Input
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        // Set rigidbody velocity
        movement.Set(moveHorizontal * shipSpeed, 0f, moveVertical * shipSpeed);
        rig.velocity = movement;

        // Clamp player ship position to map boundaries
        newPosition.Set(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax));
        rig.position = newPosition;

        // Set player ship rotation to follow movement
        rig.rotation = Quaternion.Euler(0f, 0f, -moveHorizontal * shipTiltAngle);

        // TODO: rotation effects
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            // "Instantiate" a bullet from the bullet pool
            GameObject bullet = ObjectPool.pools.GetPlayerBullet();
            if (bullet != null)
            {
                bullet.transform.position = shotSpawn.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
                if (shotAudio) shotAudio.Play();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        totalLives -= 1;
        Instantiate(deathParticlesObject, transform.position, transform.rotation);
        other.gameObject.SetActive(false);
        livesDisplayer.UpdateLivesIcons(totalLives);
        if (totalLives <= 0)
        {
            // if enough damage is taken
            Destroy(this.gameObject);
            gameController.GameOver();
        }
    }
}
