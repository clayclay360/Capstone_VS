using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Image[] starImages;
    public Text content;

    public void DisplayResults(float rating)
    {
        float score = Mathf.Abs(rating);

        for (int i = 0; i < score; i++)
        {
            starImages[i].color = Color.yellow;
        }

        if (score == 3)
        {
            content.text = "Awesome Job!";
        }
        else if (score == 2)
        {
            content.text = "Not Bad";
        }
        else
        {
            content.text = "Bad";
        }
    }
}
