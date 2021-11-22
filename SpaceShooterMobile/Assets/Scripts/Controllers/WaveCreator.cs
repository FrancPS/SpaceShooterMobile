using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Enemy
{
    public string hazard;
    public int score;
}

public class WaveCreator : MonoBehaviour
{
    public int initialPoints;
    public Enemy[] badguys;     // We will assume that the bad guys are entered in order, with the less valuable coming first, and the most valuable at the end of the array

    private GameController gameController;

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public List<Enemy> GenerateWave()
    {
        List<Enemy> wave = new List<Enemy>();
        int pointsLeft = initialPoints + gameController.GetScore();
        int targetIndex, maxSet = 0;

        // get the appropiate enemy set
        if (pointsLeft < 300) { maxSet = 3; }       // Spawn asteroids and purple enemies
        else if (pointsLeft < 1000) { maxSet = 4; } // Start spawning green enemies
        else { maxSet = badguys.Length; }           // Start spawning red enemies

        if (maxSet > badguys.Length) { maxSet = badguys.Length; } // Control for the 1st two cases, in case we have a smaller list of enemies
        
        while (pointsLeft > 0)
        {
            targetIndex = ObtainValidEnemy(maxSet, pointsLeft);
            wave.Add(badguys[targetIndex]);
            pointsLeft -= badguys[targetIndex].score;
        }

        return wave;
    }

    int ObtainValidEnemy(int maxIndex, int pointsLeft)
    {
        int index = Random.Range(0, maxIndex);

        if (badguys[index].score > pointsLeft)
        {
            index = ObtainValidEnemy(index, pointsLeft);
        }
        return index;
    }
}
