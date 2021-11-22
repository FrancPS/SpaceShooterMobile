using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string CONFIG_FOLDER = Application.persistentDataPath + "/Config/";
    private static readonly string SETTINGS_FILE = "/settings.json";
    private static readonly string HIGHSCORE_FILE = "/highscores.json";

    public static void Init() {
        if (!Directory.Exists(CONFIG_FOLDER))
        {
            Directory.CreateDirectory(CONFIG_FOLDER);
        }
    }

    // --------- SETTINGS
    public static void SaveSettings(SettingsManager.SettingsData stData)
    {
        string settingsStr = JsonUtility.ToJson(stData);
        File.WriteAllText(CONFIG_FOLDER + SETTINGS_FILE, settingsStr);
    }

    public static SettingsManager.SettingsData LoadSettings()
    {
        SettingsManager.SettingsData settings;
        if (File.Exists(CONFIG_FOLDER + SETTINGS_FILE))
        {
            string settingsStr = File.ReadAllText(CONFIG_FOLDER + SETTINGS_FILE);
            settings = JsonUtility.FromJson<SettingsManager.SettingsData>(settingsStr);
            
        } else
        {
            settings = new SettingsManager.SettingsData();
        }
        return settings;
    }


    // --------- SCORES
    public static void SaveScores(SettingsManager.HighScoreData hsData)
    {
        string scoresStr = JsonUtility.ToJson(hsData);
        File.WriteAllText(CONFIG_FOLDER + HIGHSCORE_FILE, scoresStr);
    }

    public static SettingsManager.HighScoreData LoadScores()
    {
        SettingsManager.HighScoreData scores;
        if (File.Exists(CONFIG_FOLDER + HIGHSCORE_FILE))
        {
            string scoresStr = File.ReadAllText(CONFIG_FOLDER + HIGHSCORE_FILE);
            scores = JsonUtility.FromJson<SettingsManager.HighScoreData>(scoresStr);

        }
        else
        {
            scores = new SettingsManager.HighScoreData();
        }
        return scores;
    }
}
