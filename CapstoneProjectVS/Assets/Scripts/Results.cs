using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Image[] starImages;
    public Text content;
    public Text scoreText;
    public static Results instance;

    public float finalScore = 0;

    [Header("Result Buttons")]
    public const int RESBUTTUM = 2;
    public Button menuButt, restartButt;

    private void Awake()
    {
        instance = this;
    }

    public void DisplayResults(float rating)
    {
        Main main = GetComponent<Main>();
        float score = Mathf.Abs(rating);
        

        for (int i = 0; i < score; i++)
        {
            starImages[i].color = Color.yellow;
            finalScore = score + main.orderScore;
        }

        if (score == 3)
        {
            content.text = "Perfect!";
            scoreText.text = finalScore.ToString();
        }
        else if (score == 2)
        {
            content.text = "Good";
            scoreText.text = finalScore.ToString();
        }
        else if(score == 1) 
        {
            content.text = "Mediocre";
            scoreText.text = finalScore.ToString();
        }
        else
        {
            content.text = "Eww";
            scoreText.text = finalScore.ToString();
        }

        Time.timeScale = 0;
    }

   

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene("StartMenu");
    }

    public void Restart()
    {
        Debug.Log("Restarting Game...");
        SceneManager.LoadScene("Level Redesign");
    }
}
