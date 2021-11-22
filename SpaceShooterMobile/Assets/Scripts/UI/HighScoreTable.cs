using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
    }

    public bool IsNewHighScore(int score)
    {

        if (score > SettingsManager.Inst.hsData.scores[SettingsManager.Inst.hsData.scores.Length - 1]) { return true; }
        else { return false; }
    }

    public void UpdateHighScoreList(string newName, int newScore)
    {
        int[] scores = SettingsManager.Inst.hsData.scores;
        string[] names = SettingsManager.Inst.hsData.names;


        for (int i = 0; i<scores.Length; ++i)
        {
            if (newScore > scores[i])
            {
                // Move lower scores down
                for (int j = scores.Length-1; j > i; --j)
                {
                    scores[j] = scores[j - 1];
                    names[j] = names[j - 1];
                }
                // Add new score there
                scores[i] = newScore;
                names[i] = newName;
                break;
            }
        }

        InstantiateScoreList();
    }

    public void InstantiateScoreList()
    {
        int[] scores = SettingsManager.Inst.hsData.scores;
        string[] names = SettingsManager.Inst.hsData.names;
        float templateHeight = 200f;

        for (int i = 0; i < scores.Length; ++i)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, templateHeight - 25 * i);
            entryTransform.gameObject.SetActive(true);

            // Entry values
            int rank = i + 1;
            entryTransform.Find("EntryPosition").GetComponent<Text>().text = rank + ". ";
            entryTransform.Find("EntryName").GetComponent<Text>().text = names[i];
            entryTransform.Find("EntryScore").GetComponent<Text>().text = scores[i].ToString();
        }
    }
}
