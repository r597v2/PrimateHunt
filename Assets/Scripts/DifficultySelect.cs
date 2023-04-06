using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultySelect : MonoBehaviour
{
    private TextMeshProUGUI difficultyText;
    private string[] difficultyLevelTexts = new string[] {"Newcomer", "Adept", "Experienced", "Extreme"};

    void Start()
    {
        GlobalGameSettings.difficultyLevel = 0;
        difficultyText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseDiff()
    {
        if(GlobalGameSettings.difficultyLevel < difficultyLevelTexts.Length - 1)
        {
            GlobalGameSettings.difficultyLevel++;
            difficultyText.text = difficultyLevelTexts[GlobalGameSettings.difficultyLevel];
        }
    }

    public void DecreaseDiff()
    {
        if(GlobalGameSettings.difficultyLevel > 0)
        {
            GlobalGameSettings.difficultyLevel--;
            difficultyText.text = difficultyLevelTexts[GlobalGameSettings.difficultyLevel];
        }
    }
}
