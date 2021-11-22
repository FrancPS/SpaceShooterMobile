using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreInput : MonoBehaviour
{
    public HighScoreTable highScoreTable;
    public Button closeButton;
    private InputField inputString;
    private int newScore;
    private string validUpperChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string validLowerChar = "abcdefghijklmnopqrstuvwxyz";

    public void SetNewScore(int score)
    {
        newScore = score;
        inputString = transform.Find("InputField").GetComponent<InputField>();
        inputString.onValidateInput = (string text, int charIndex, char addedChar) =>
        {
            return ValidateChar(validUpperChar, validLowerChar, addedChar);
        };
    }

    void Update()
    {
        if (inputString.text.Length == 3) { closeButton.interactable = true; }
        else { closeButton.interactable = false; }
    }

    public void OnClose()
    {
        highScoreTable.UpdateHighScoreList(inputString.GetComponent<InputField>().text, newScore);
    }

    private char ValidateChar(string validUpperChar, string validLowerChar, char addedChar)
    {
        if (validUpperChar.IndexOf(addedChar) != -1)
        {
            return addedChar;
        }
        else if (validLowerChar.IndexOf(addedChar) != -1)
        {
            return System.Char.ToUpper(addedChar);
        } else
        {
            return '\0';
        }
    }
}
