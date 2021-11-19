using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameUIBehaviour : MonoBehaviour
{
    //Handles the displaying of HUD information during gameplay, such as scores and flower count.
    [Header("Display Elements")]
    public TMP_Text scoreDisplay;
    public TMP_Text flowerCounter;
    [Header("Events")]
    public UnityEvent<int> onScoreUpdate;
    public UnityEvent<int> onUIClosed;

    private int score;
    private int flowers;

    //This function recieves the amount of points to increase, not the total score.
    public void UpdateScore(int value)
    {
        score += value;
        scoreDisplay.text = score.ToString();
        onScoreUpdate.Invoke(score);
    }

    //UpdateFlowerCount and GetFlowerTotal both use PlayerPrefs to store 
    //and get the ammount of flowers you have between sessions.
    //It receives the amount of flowers to increase the counter by.
    public void UpdateFlowerCount(int value)
    {
        flowers += value;
        flowerCounter.text = flowers.ToString();
        PlayerPrefs.SetInt("FlowerTotal", flowers);
    }

    int GetFlowerTotal()
    {
       return PlayerPrefs.GetInt("FlowerTotal", 0);
    }
    // Start is called before the first frame update
    public void StartUI()
    {
        gameObject.SetActive(true);
        ResetScore();
        flowers = GetFlowerTotal();
        flowerCounter.text = flowers.ToString();
    }

    //This will reset the score to zero
    public void ResetScore()
    {
        UpdateScore(-score);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
        onUIClosed.Invoke(score);
    }

}
