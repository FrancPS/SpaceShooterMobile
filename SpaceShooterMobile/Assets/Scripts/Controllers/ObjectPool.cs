using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ----- Object Pool -----
 *  Initialise and maintain the pools of instantiable gameobjects in the game,
 *  such as bullets, enemies, obstacles, etc.
 */

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool pools;

    [Header("Bullets")]
    public GameObject playerBulletPrefab;
    public GameObject basicEnemyBulletPrefab;
    private List<GameObject> playerBulletPool;
    private List<GameObject> basicEnemyBulletPool;

    [Header("Asteroids")]
    public GameObject asteroid01Prefab;
    public GameObject asteroid02Prefab;
    public GameObject asteroid03Prefab;
    private List<GameObject> asteroid01Pool;
    private List<GameObject> asteroid02Pool;
    private List<GameObject> asteroid03Pool;

    [Header("Enemies")]
    public GameObject purpleEnemyPrefab;
    public GameObject greenEnemyPrefab;
    public GameObject redEnemyPrefab;
    private List<GameObject> purpleEnemyPool;
    private List<GameObject> greenEnemyPool;
    private List<GameObject> redEnemyPool;

    [Header("Explosions")]
    public GameObject asteroidExplosionPrefab;
    public GameObject enemyExplosionPrefab;
    private List<GameObject> asteroidExplosionPool;
    private List<GameObject> enemyExplosionPool;

    // Scene organization parent objects
    private GameObject bullets;
    private GameObject asteroids;
    private GameObject enemies;
    private GameObject explosions;

    void Awake()
    {
        pools = this;

        // Bullet Pool
        playerBulletPool = new List<GameObject>();
        basicEnemyBulletPool = new List<GameObject>();

        // Asteroid Pools
        asteroid01Pool = new List<GameObject>();
        asteroid02Pool = new List<GameObject>();
        asteroid03Pool = new List<GameObject>();

        // Enemy Pools
        purpleEnemyPool = new List<GameObject>();
        greenEnemyPool = new List<GameObject>();
        redEnemyPool = new List<GameObject>();

        // Explosions
        asteroidExplosionPool = new List<GameObject>();
        enemyExplosionPool = new List<GameObject>();

        // Parents
        bullets = new GameObject("Bullets");
        asteroids = new GameObject("Asteroids");
        enemies = new GameObject("Enemies");
        explosions = new GameObject("Explosions");
    }

    public GameObject GetPlayerBullet()
    {
        int i;
        for (i = 0; i < playerBulletPool.Count; ++i)
        {
            if (!playerBulletPool[i].activeInHierarchy)
            {
                return playerBulletPool[i];
            }
        }

        // if no object was available, instantiate a new one
        GameObject tmp;
        tmp = Instantiate(playerBulletPrefab, bullets.transform);
        tmp.SetActive(false);
        playerBulletPool.Add(tmp);
        return playerBulletPool[i];
    }

    public GameObject GetBasicEnemyBullet()
    {
        int i;
        for (i = 0; i < basicEnemyBulletPool.Count; ++i)
        {
            if (!basicEnemyBulletPool[i].activeInHierarchy)
            {
                return basicEnemyBulletPool[i];
            }
        }
        // if no object was available, instantiate a new one
        GameObject tmp;
        tmp = Instantiate(basicEnemyBulletPrefab, bullets.transform);
        tmp.SetActive(false);
        basicEnemyBulletPool.Add(tmp);
        return basicEnemyBulletPool[i];
    }

    public GameObject GetAsteroid()
    {
        int asteroidType = Random.Range(1, 4); // random integer: 1, 2 or 3
        int i;
        GameObject tmp;
        
        switch (asteroidType)
        {
            case 1:
                for (i = 0; i < asteroid01Pool.Count; ++i)
                {
                    if (!asteroid01Pool[i].activeInHierarchy)
                    {
                        return asteroid01Pool[i];
                    }
                }
                // if no object was available, instantiate a new one
                tmp = Instantiate(asteroid01Prefab, asteroids.transform);
                tmp.SetActive(false);
                asteroid01Pool.Add(tmp);
                return asteroid01Pool[i];
            case 2:
                for (i = 0; i < asteroid02Pool.Count; ++i)
                {
                    if (!asteroid02Pool[i].activeInHierarchy)
                    {
                        return asteroid02Pool[i];
                    }
                }
                // if no object was available, instantiate a new one
                tmp = Instantiate(asteroid02Prefab, asteroids.transform);
                tmp.SetActive(false);
                asteroid02Pool.Add(tmp);
                return asteroid02Pool[i];
            case 3:
                for (i = 0; i < asteroid03Pool.Count; ++i)
                {
                    if (!asteroid03Pool[i].activeInHierarchy)
                    {
                        return asteroid03Pool[i];
                    }
                }
                // if no object was available, instantiate a new one
                tmp = Instantiate(asteroid03Prefab, asteroids.transform);
                tmp.SetActive(false);
                asteroid03Pool.Add(tmp);
                return asteroid03Pool[i];
            default:
                return null;
        }
    }

    public GameObject GetAsteroidExplosion()
    {
        int i;
        for (i = 0; i < asteroidExplosionPool.Count; ++i)
        {
            if (!asteroidExplosionPool[i].activeInHierarchy)
            {
                return asteroidExplosionPool[i];
            }
        }

        // if no object was available, instantiate a new one
        GameObject tmp;
        tmp = Instantiate(asteroidExplosionPrefab, explosions.transform);
        tmp.SetActive(false);
        asteroidExplosionPool.Add(tmp);
        return asteroidExplosionPool[i];
    }

    public GameObject GetPurpleEnemy()
    {
        int i;
        for (i = 0; i < purpleEnemyPool.Count; ++i)
        {
            if (!purpleEnemyPool[i].activeInHierarchy)
            {
                return purpleEnemyPool[i];
            }
        }
        // if no object was available, instantiate a new one
        GameObject tmp;
        tmp = Instantiate(purpleEnemyPrefab, enemies.transform);
        tmp.SetActive(false);
        purpleEnemyPool.Add(tmp);
        return purpleEnemyPool[i];
    }

    public GameObject GetGreenEnemy()
    {
        int i;
        for (i = 0; i < greenEnemyPool.Count; ++i)
        {
            if (!greenEnemyPool[i].activeInHierarchy)
            {
                return greenEnemyPool[i];
            }
        }
        // if no object was available, instantiate a new one
        GameObject tmp;
        tmp = Instantiate(greenEnemyPrefab, enemies.transform);
        tmp.SetActive(false);
        greenEnemyPool.Add(tmp);
        return greenEnemyPool[i];
    }

    public GameObject GetRedEnemy()
    {
        int i;
        for (i = 0; i < redEnemyPool.Count; ++i)
        {
            if (!redEnemyPool[i].activeInHierarchy)
            {
                return redEnemyPool[i];
            }
        }
        // if no object was available, instantiate a new one
        GameObject tmp;
        tmp = Instantiate(redEnemyPrefab, enemies.transform);
        tmp.SetActive(false);
        redEnemyPool.Add(tmp);
        return redEnemyPool[i];
    }

    public GameObject GetEnemyExplosion()
    {
        int i;
        for (i = 0; i < enemyExplosionPool.Count; ++i)
        {
            if (!enemyExplosionPool[i].activeInHierarchy)
            {
                return enemyExplosionPool[i];
            }
        }

        // if no object was available, instantiate a new one
        GameObject tmp;
        tmp = Instantiate(enemyExplosionPrefab, explosions.transform);
        tmp.SetActive(false);
        enemyExplosionPool.Add(tmp);
        return enemyExplosionPool[i];
    }
}
