using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // Singleton Class
    public static SettingsManager Inst { get; private set; }

    // Settings
    public class SettingsData
    {
        public float volumeLevel = 0.5f;
        public int difficulty = 0;
    }
    // High Scores
    public class HighScoreData
    {
        public int[] scores = new int[10] { 100, 50, 10, 0, 0, 0, 0, 0, 0, 0 };
        public string[] names = new string[10] { "AAA", "BBB", "CCC", "DDD", "EEE", "FFF", "GGG", "HHH", "III", "JJJ" };
    }

    // Members
    public SettingsData stData;
    public HighScoreData hsData;

    void Awake()
    {
        if (SettingsManager.Inst == null)
        {
            // If an instance of Settings Manager does not exist, creat a new one
            SettingsManager.Inst = this;
            DontDestroyOnLoad(gameObject);
            // Create and load config files
            SaveSystem.Init();
            stData = SaveSystem.LoadSettings();
            hsData = SaveSystem.LoadScores();
        }
        else
        {
            // If it already exists, destroy this new attempt to create one.
            Destroy(gameObject);
        }
    }


    void OnApplicationQuit()
    {
        SaveSystem.SaveSettings(SettingsManager.Inst.stData);
        SaveSystem.SaveScores(SettingsManager.Inst.hsData);
    }
}
