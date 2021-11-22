using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ----- MOVER -----
 *  Give an initial speed, direction and angular velocity to a game object.
 *  Deactivate the gameobject after a specified time.
 */

public class Mover : MonoBehaviour
{
    public float speed;
    public float timeToDie;
    private Rigidbody rig;
    private float timeLived;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        timeLived = 0;
    }

    void Start()
    {
        rig.velocity = transform.forward * speed;
    }

    void Update()
    {
        // Remove the object from the game after it leaves the screen (only if TTD > 0)
        if (timeToDie != 0)
        {
            timeLived += Time.deltaTime;
            if (timeLived >= timeToDie)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        timeLived = 0;
    }
}
