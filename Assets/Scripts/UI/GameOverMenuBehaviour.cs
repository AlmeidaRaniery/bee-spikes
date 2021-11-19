using UnityEngine;
using TMPro;


public class GameOverMenuBehaviour : MonoBehaviour
{
    public TMP_Text scoreDisplay;
    public TMP_Text highScoreDisplay;
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    //This is called when we finish the game. It uses PlayerPrefs to get the high score
    //and assigns it if the last score is greater than the current high score.
    //It also sets the text to the appropriate UI elements.
    public void UpdateFinalScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        scoreDisplay.text = score.ToString();
        
        if (score > highScore)
            highScore = SetHighScore(score);

         highScoreDisplay.text = highScore.ToString();
    }

    public int SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
        return score;
    }
}
