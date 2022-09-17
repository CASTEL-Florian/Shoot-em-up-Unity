using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    int comboCounter = 0;
    public static ScoreManager Instance { get; private set; }
    private int score = 0;
    void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(this); }
    }

    public void AddScore(int value)
    {
        comboCounter += 1;
        score += value * comboCounter;
        scoreText.text = "Score : " + score.ToString();
        if (comboCounter >= 1)
            comboText.text = "Combo x" + comboCounter.ToString();
    }
    public void ResetCombo()
    {
        comboCounter = 0;
        comboText.text = "";
    }

    public int GetScore()
    {
        return score;
    }
}
