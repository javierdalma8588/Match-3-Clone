using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Board board;
    public Text scorteText;
    public int score;
    public Image scoreBar;
    private GameData gameData;

    private void Start()
    {
        board = FindObjectOfType<Board>();
        gameData = FindObjectOfType<GameData>();
        scoreBar.fillAmount = 0;
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scorteText.text = score.ToString();
        if(gameData != null)
        {
            int highScore = gameData.saveData.highscores[board.level];
            if (score > highScore)
            {
                gameData.saveData.highscores[board.level] = score;
            }
            gameData.Save();
        }
        UpdateBar();
    }

    void UpdateBar()
    {
        if (board != null && scoreBar != null)
        {
            int lenght = board.scoreGoals.Length;
            scoreBar.fillAmount = (float)score / (float)board.scoreGoals[lenght - 1];
        }
    }
}
