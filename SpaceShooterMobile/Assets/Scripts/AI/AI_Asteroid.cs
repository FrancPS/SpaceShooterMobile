using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ----- ASTEROID -----
 *  
 */

public class AI_Asteroid : MonoBehaviour
{
    public int scoreValue;
    private GameController gameController;

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
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
            GameObject explosion = ObjectPool.pools.GetAsteroidExplosion();
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
