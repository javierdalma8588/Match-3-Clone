using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour
{
    [Header("Level Information")]
    public string levelToLoad;
    public int level;
    private GameData gamedata;
    private int starsActive;
    private int highScore;

    [Header("UI Elements")]
    public Image[] stars;
    public Text highScoreText;
    public Text starText;

    void OnEnable()
    {
        gamedata = FindObjectOfType<GameData>();
        LoadData(); 
        ActivateStarts();
        SetText();
    }

    void LoadData()
    {
        if(gamedata != null)
        {
            starsActive = gamedata.saveData.stars[level - 1];
            highScore = gamedata.saveData.highscores[level - 1];
        }
    }

    void SetText()
    {
        highScoreText.text = highScore.ToString();
        starText.text = starsActive + "/3"; 
    }

    void ActivateStarts()
    {
        for (int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("Current Level", level-1);
        SceneManager.LoadScene(levelToLoad);
    }
}
