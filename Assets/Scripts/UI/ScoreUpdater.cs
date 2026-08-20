using UnityEngine;
using TMPro; // Required for TextMesh Pro
using System.Collections;
using System;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI preScoreText; 
    private float score = 0;
    public float Score => score;

    void Start() {
        this.score = GameplayManager.instance.playerScore;
        UpdateScoreDisplay();
        SetPreScore(GameplayManager.instance.playerPreScore);
    }

    public void IncrementScore(float score)
    {
        this.score += score;
        GameplayManager.instance.playerScore = this.score;
        UpdateScoreDisplay();
    }

    public void SetPreScore(float collected)
    {
        if (preScoreText)
        {
            if (collected != 0)
            {
                preScoreText.text = $"In Nana's pockets: {Mathf.RoundToInt(collected)}";
                GameplayManager.instance.playerPreScore = collected;
            }
            else
            {
                preScoreText.text = "";
                GameplayManager.instance.playerPreScore = 0;
            }
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText)
            scoreText.text = $"Score: {Mathf.RoundToInt(score)}";
    }
}