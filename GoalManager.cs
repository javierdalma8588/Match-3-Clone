using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//serializable to be able to edit this on the editor
[System.Serializable]
public class BlanckGoal
{
    public int numberNeeded;
    public int numberCollected;
    public Sprite goalSprite;
    public string matchValue;
}

public class GoalManager : MonoBehaviour
{
    public BlanckGoal[] levelGoals;
    public List<GoalPanel> currenGoals = new List<GoalPanel>();
    public GameObject goalPrefab;
    public GameObject goalIntroParent;
    public GameObject goalGameParent;
    private Board board;
    private EndGameManager endGame;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        endGame = FindObjectOfType<EndGameManager>();
        GetGoals();
        SetUpGoals();
    }

    void GetGoals()
    {
        if(board != null)
        {
            if(board.world != null)
            {
                if (board.level < board.world.levels.Length)
                {
                    if (board.world.levels[board.level] != null)
                    {
                        levelGoals = board.world.levels[board.level].levelGoals;

                        for(int i = 0; i<levelGoals.Length; i++)
                        {
                            levelGoals[i].numberCollected = 0;
                        }
                    }
                }
            }
        }
    }

    void SetUpGoals()
    {
        for(int i = 0; i < levelGoals.Length; i++)
        {
            //create a new Goal Panel at the goal intro panel position
            GameObject goal = Instantiate(goalPrefab, goalIntroParent.transform.position, Quaternion.identity, goalIntroParent.transform);

            //set the Image and Text of the goal
            GoalPanel panel = goal.GetComponent<GoalPanel>();

            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thiString = "0/" + levelGoals[i].numberNeeded;

            GameObject gameGoal = Instantiate(goalPrefab, goalGameParent.transform.position, Quaternion.identity, goalGameParent.transform);

            //set the Image and Text of the goal
            panel = gameGoal.GetComponent<GoalPanel>();

            currenGoals.Add(panel);

            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thiString = "0/" + levelGoals[i].numberNeeded;
        }
    }

    public void UpdateGoals()
    {
        int goalsCompleted = 0;

        for(int i = 0; i < levelGoals.Length; i++)
        {
            currenGoals[i].thisText.text = "" + levelGoals[i].numberCollected + "/" + levelGoals[i].numberNeeded;
            if(levelGoals[i].numberCollected >= levelGoals[i].numberNeeded)
            {
                goalsCompleted++;
                currenGoals[i].thisText.text = "" + levelGoals[i].numberNeeded + "/" + levelGoals[i].numberNeeded;
            }
        }

        if(goalsCompleted >= levelGoals.Length)
        {
            if(endGame != null)
            {
                endGame.WinGame();
            }
            Debug.LogError("you win!!!");
        }
    }

    public void CompareGoal(string goalToCompare)
    {
        for(int i = 0; i< levelGoals.Length; i++)
        {
            if(goalToCompare == levelGoals[i].matchValue)
            {
                levelGoals[i].numberCollected++;
            }
        }
    }
}
