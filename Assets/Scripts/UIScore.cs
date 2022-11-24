using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TextMeshProUGUI _scoreText;

    public int score;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        ResetPoints();
    }

    public void ResetPoints()
    {
        score = 0;
        _scoreText.text = $"Score: {score}";
    }

    public void AddPoint()
    {
        score++;
        _scoreText.text = $"Score: {score}";
    }
}
