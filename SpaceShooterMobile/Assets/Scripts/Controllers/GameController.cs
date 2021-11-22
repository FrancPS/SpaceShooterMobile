using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class MapBoundary
{
    public float xMin, xMax, zMin, zMax;

    public void UpdateBoundaries()
    {
        Vector2 halfScreenSize = Utils.GetScreenDimensionsInWorldUnits() * 0.5f;
        // width boundaries + player ship width
        xMin = -halfScreenSize.x + 0.7f;
        xMax = halfScreenSize.x - 0.7f;

        // height boundaries + relative offset
        zMin = -halfScreenSize.y + 12f;
        zMax = halfScreenSize.y;
    }
}

public class GameController : MonoBehaviour
{
    [Header("Wave Controls")]
    public float waveStartDelay;
    public float waveSpawnDelay;
    public float nextWaveDelay;
    public Vector3 spawnValues;
    private Vector3 spawnPosition;
    private WaveCreator waveCreator;
    private float internalSpawnDelay;

    [Header("Scoring")]
    public Text scoreText;
    private int score;

    [Header("HUD")]
    public GameObject gameOverPanel;
    public ScoreInput newHighScoreInput;
    public HighScoreTable highScoreTable;
    private bool gameOver;

    void Awake()
    {
        spawnPosition = default;
        score = 0;
        newHighScoreInput.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        gameOver = false;
    }

    void Start()
    {
        waveCreator = gameObject.GetComponent<WaveCreator>();
        UpdateSpawnValues();
        AddScore(0);
        internalSpawnDelay = waveSpawnDelay - 0.25f * SettingsManager.Inst.stData.difficulty;
        StartCoroutine(SpawnWaves());
    }

    void UpdateSpawnValues()
    {
        Vector2 halfScreenSize = Utils.GetScreenDimensionsInWorldUnits() * 0.5f;
        spawnValues = new Vector3(halfScreenSize.x - 0.7f, 0f, halfScreenSize.y + 6f);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(waveStartDelay);    // Make Coroutine wait some time at the beginning of the game
        
        while (!gameOver)
        {
            List<Enemy> wave = waveCreator.GenerateWave();
            GameObject hazard;
            for (int i = 0; i < wave.Count; ++i)
            {
                switch(wave[i].hazard)
                {
                    case "Asteroid":
                        hazard = ObjectPool.pools.GetAsteroid();    // "Instantiate" an asteroid from the asteroid pool
                        break;
                    case "PurpleEnemy":
                        hazard = ObjectPool.pools.GetPurpleEnemy();
                        break;
                    case "GreenEnemy":
                        hazard = ObjectPool.pools.GetGreenEnemy();
                        break;
                    case "RedEnemy":
                        hazard = ObjectPool.pools.GetRedEnemy();
                        break;
                    default:
                        hazard = null;
                        break;
                }

                if (hazard != null)
                {
                    spawnPosition = spawnValues;
                    spawnPosition.x = Random.Range(-spawnPosition.x, spawnPosition.x);
                    hazard.transform.position = spawnPosition;
                    hazard.transform.rotation = Quaternion.identity;
                    hazard.SetActive(true);
                }
                yield return new WaitForSeconds(internalSpawnDelay);    // Make Coroutine wait some time between spawns
            }

            yield return new WaitForSeconds(nextWaveDelay);     // Make Coroutine wait some time between waves
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        if (highScoreTable.IsNewHighScore(score))
        {
            newHighScoreInput.gameObject.SetActive(true);
            newHighScoreInput.SetNewScore(score);
        }
        else
        {
            highScoreTable.InstantiateScoreList();
            gameOverPanel.SetActive(true);
        }
        gameOver = true;
    }
}
