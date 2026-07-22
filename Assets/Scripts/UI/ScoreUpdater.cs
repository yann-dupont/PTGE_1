using UnityEngine;
using TMPro; // Required for TextMesh Pro
using System.Collections;
using System;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI preScoreText; 
    private float score = 0;

    void Start() {
        UpdateScoreDisplay();
        SetPreScore(0);
    }

    public void IncrementScore(float score)
    {
        this.score += score;
        UpdateScoreDisplay();
    }

    public void SetPreScore(float collected)
    {
        if (preScoreText)
        {
            if (collected != 0)
            {
                preScoreText.text = $"In Nana's pockets: {Mathf.RoundToInt(collected)}";
            }
            else
            {
                preScoreText.text = "";
            }
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText)
            scoreText.text = $"Score: {Mathf.RoundToInt(score)}";
    }
}