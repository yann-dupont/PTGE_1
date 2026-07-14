using UnityEngine;
using TMPro; // Required for TextMesh Pro
using System.Collections;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Drag your TMP object here
    private int score = 0;

    void Start() {
        if (scoreText)
            scoreText.text += " " + score;
    }

    public void incrementScore(int score) {
        this.score += score;
        if (scoreText)
            scoreText.text += " " + score;
    }

}