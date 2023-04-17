using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Variables")]
    public bool startTutorial;
    public Step[] steps;
    public int currentStepNumber;

    [Header("Items")]
    public GameObject passCounter;

    private CounterTop counterTop;
    public int currentNumberOfTaskCompleted { set; get; }

    // variables of the player
    private PlayerController[] playerControllers;

    [HideInInspector]
    public Text playerOneText, playerTwoText;

    private Main main; // main variable;

    public void Start()
    {
        main = FindObjectOfType<Main>();
        counterTop = passCounter.GetComponent<CounterTop>();
    }

    public void StartTutorial()
    {
        GetPlayers();
        StartCoroutine(TutorialSteps());
    }

    public void GetPlayers()
    {
        playerControllers = FindObjectsOfType<PlayerController>();
        playerOneText = playerControllers[1].interactionText;
        playerTwoText = playerControllers[0].interactionText;
    }

    public IEnumerator TutorialSteps()
    {
        //currentStepNumber = 0;

        // show players how to walk
        if (currentStepNumber == 0)
        {
            yield return new WaitForSeconds(1);
            foreach (PlayerController playerController in playerControllers)
            {
                playerController.interactionText.text = "Use [right stick] to move your character!";
            }
            steps[currentStepNumber].isComplete = true;
            currentStepNumber++;
        }

        // show player orderwindow
        if (currentStepNumber == 1)
        {
            yield return new WaitForSeconds(3);
            main.TutorialOrder();
            foreach (PlayerController playerController in playerControllers)
            {
                playerController.interactionText.text = "";
            }
            while (!steps[1].isComplete)
            {
                yield return null;
            }
        }

        // show cook book
        if (currentStepNumber == 2)
        {
            FindObjectOfType<Cookbook>().DisplayIndicator(true);
            while (!steps[2].isComplete)
            {
                yield return null;
            }
        }

        // bacon and pan
        if (currentStepNumber == 3)
        {
            FindObjectOfType<BaconPack>().DisplayIndicator(true);
            FindObjectOfType<Pan>().DisplayIndicator(true);
            while (currentNumberOfTaskCompleted < 2)
            {
                yield return null;
            }
            steps[currentStepNumber].isComplete = true;
            currentStepNumber++;
            currentNumberOfTaskCompleted = 0;
        }  
        
        // counter and stove
        if(currentStepNumber == 4)
        {
            counterTop.DisplayIndicator(true);
            FindObjectOfType<Stove>().DisplayIndicator(true);
            while (currentNumberOfTaskCompleted < 2)
            {
                yield return null;
            }
            steps[currentStepNumber].isComplete = true;
            currentStepNumber++;
            currentNumberOfTaskCompleted = 0;
        }

        // spatula 
        if(currentStepNumber == 5)
        {
            FindObjectOfType<Spatula>().DisplayIndicator(true);
        }

        // bacon
        if (currentStepNumber == 6)
        {
            FindObjectOfType<Bacon>().DisplayIndicator(true);
        }

        Debug.Log("Step Completed");
        //
    }
}
