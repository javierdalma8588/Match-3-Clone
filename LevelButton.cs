using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Active Objects")]
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    private int startsActive;

    [Header("Level UI")]
    public Image buttonImage;
    public Button myButton;
    public Image[] stars;
    public Text levelText;
    public int level;
    public GameObject confirmPanel;

    private GameData gameData;

    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        LoadData();
        ActivateStarts();
        ShowLevel();
        DecideSprite();
    }

    void LoadData()
    {
        //is there game data
        if(gameData != null)
        {
            //Decide if the level is active
            if(gameData.saveData.isActive[level-1])
            {
                isActive = true;
            }else
            {
                isActive = false;
            }
            //decide stars to activate
            startsActive = gameData.saveData.stars[level - 1];
        }
    }

    void ActivateStarts()
    {
        for(int i = 0; i < startsActive ; i++)
        {

            stars[i].enabled = true;
        }
    }

    void DecideSprite()
    {
        if(isActive)
        {
            buttonImage.sprite = activeSprite;
            myButton.enabled = true;
            levelText.enabled = true;
        }else
        {
            buttonImage.sprite = lockedSprite;
            myButton.enabled = false;
            levelText.enabled = false;
        }
    }

    void ShowLevel()
    {
        levelText.text = level.ToString();
    }

    public  void ConfirmPanel(int level)
    {
        confirmPanel.GetComponent<ConfirmPanel>().level = level;
        confirmPanel.SetActive(true);
    }
}
