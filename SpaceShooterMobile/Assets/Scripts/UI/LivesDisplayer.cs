using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplayer : MonoBehaviour
{
    public Transform livesContainer;
    public Transform lifeTemplate;
    public int iconSeparation;

    private List<GameObject> livesIconList = new List<GameObject>();

    void Awake()
    {
        lifeTemplate.gameObject.SetActive(false);
    }

    public void InstantiateLivesIcons(int lives)
    {
        for (int i = 0; i < lives; ++i)
        {
            Transform entryTransform = Instantiate(lifeTemplate, livesContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(entryRectTransform.anchoredPosition.x + iconSeparation * i, entryRectTransform.anchoredPosition.y);
            entryTransform.gameObject.SetActive(true);
            livesIconList.Add(entryTransform.gameObject.gameObject);
        }
    }

    public void UpdateLivesIcons(int newLives)
    {
        if (newLives >= livesIconList.Count) return;

        for (int i = 0; i < livesIconList.Count; ++i)
        {
            if (i < newLives) { livesIconList[i].SetActive(true); }
            else { livesIconList[i].SetActive(false); }
        }
    }
}
