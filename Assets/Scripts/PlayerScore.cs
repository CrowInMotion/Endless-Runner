using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int playerScore = 0;
    public TMP_Text guiScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void setPlayerScore(int score)
    {
        playerScore = score;
        setGUIPlayerScore();
    }

    public void increasePlayerScore(int val)
    {
        playerScore += val;
        setGUIPlayerScore();
    }

    public int getScore()
    {
        return playerScore;
    }

    public void setGUIPlayerScore()
    {
        guiScore.text = "Score: ";
        guiScore.text += playerScore.ToString();
        Debug.Log("Player Score in PlayerScore: " + playerScore.ToString());
    }
}