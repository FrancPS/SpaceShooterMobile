using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ----- RandomRotator -----
 *  Give an initial angular velocity to a game object.
 */

public class RandomRotator : MonoBehaviour
{
    public float tumbleSpeed;
    private Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rig.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
    }

}
