using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    private int _score;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private Button retryButton;

    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        _score = PlayerPrefs.GetInt("score");
        _scoreText.text = $"Your Score: {_score}";

        retryButton.onClick.AddListener(StartNewGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void StartNewGame()
    {
        retryButton.onClick.RemoveAllListeners();
        SceneManager.LoadScene("Game");
    }

    private void ExitGame()
    {
        Debug.Log("Exiting...");
        exitButton.onClick.RemoveAllListeners();
        Application.Quit();
    }
}
