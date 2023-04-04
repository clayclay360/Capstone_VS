using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Image[] starImages;
    public Text content;

    [Header("Result Buttons")]
    public const int RESBUTTUM = 2;
    public Button menuButt, restartButt;

    public void DisplayResults(float rating)
    {
        float score = Mathf.Abs(rating);

        for (int i = 0; i < score; i++)
        {
            starImages[i].color = Color.yellow;
        }

        if (score == 3)
        {
            content.text = "Perfect!";
        }
        else if (score == 2)
        {
            content.text = "Good";
        }
        else
        {
            content.text = "Ewww";
        }
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Debug.Log("Restarting Game...");
        SceneManager.LoadScene("");
    }
}
