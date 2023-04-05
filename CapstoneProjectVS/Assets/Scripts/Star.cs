using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Star : MonoBehaviour
{

    public Image[] starImages;
    //public Main main;


    public void DisplayStars(float rating)
    {
        float starScore = Mathf.Abs(rating);

        Debug.Log("You got a star");
        for (int i = 0; i < starScore; i++)
        {
            starImages[i].color = Color.yellow;
        }
    }
}
