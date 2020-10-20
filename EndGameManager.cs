 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameType
{
    Moves,
    Time
}

[System.Serializable]
public class EndGameRequirements
{
    public GameType gameType;
    public int counterValue;
}

public class EndGameManager : MonoBehaviour
{
    public Text gameTypeLabel;
    public Text counter;
    public GameObject youWinPanel;
    public GameObject tryAgainPanel;
    public EndGameRequirements requirements;
    public int currentCounterValue;
    private Board board;
    private float timerSeconds;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        SetGameType();
        SetUpGame(); 
    }

    void SetGameType()
    {
        if(board.world != null)
        {
            if (board.level < board.world.levels.Length)
            {
                if (board.world.levels[board.level] != null)
                {
                    requirements = board.world.levels[board.level].endGameRequirements;
                }
            }
        }
    }

    void SetUpGame()
    {

        currentCounterValue = requirements.counterValue;
        if(requirements.gameType == GameType.Moves)
        {
            gameTypeLabel.text = "Moves";
        }else
        {
            timerSeconds = 1;
            gameTypeLabel.text = "Time";
        }
        counter.text = currentCounterValue.ToString();
    }

    public void DecreaseCounterValue()
    {
        if (board.currentState != GameState.pause)
        {
            currentCounterValue--;
            counter.text = currentCounterValue.ToString();
            if (currentCounterValue <= 0)
            {
                LoseGame();
            }
        }
    }

    public void WinGame()
    {
        youWinPanel.SetActive(true);
        board.currentState = GameState.win;
        currentCounterValue = 0;
        counter.text = currentCounterValue.ToString();
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    public void LoseGame()
    {
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        Debug.LogError("You Lose");
        currentCounterValue = 0;
        counter.text = currentCounterValue.ToString();
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    void Update()
    {
        if(requirements.gameType == GameType.Time && currentCounterValue > 0)
        {
            timerSeconds -= Time.deltaTime;
            if(timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }
}
