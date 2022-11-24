using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TextMeshProUGUI _scoreText;

    private int _score;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        _score = 0;
        _scoreText.text = $"Score: {_score}";
    }

    public void AddPoint()
    {
        _score++;
        _scoreText.text = $"Score: {_score}";
    }
}
