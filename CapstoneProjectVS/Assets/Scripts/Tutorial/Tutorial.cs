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
    public int currentNumberOfTaskCompleted { set; get; }

    // variables of the player
    private PlayerController[] playerControllers;
    private Text[] playerInteractionTexts;

    private Main main; // main variable;

    public void Start()
    {
        main = FindObjectOfType<Main>();
    }

    public void StartTutorial()
    {
        GetPlayers();
        StartCoroutine(TutorialSteps());
    }

    public void GetPlayers()
    {
        playerControllers = FindObjectsOfType<PlayerController>();
    }

    public IEnumerator TutorialSteps()
    {
        currentStepNumber = 0;

        yield return new WaitForSeconds(1);
        // show players how to walk
        foreach(PlayerController playerController in playerControllers)
        {
            playerController.interactionText.text = "Use [right stick] to move your character!";
        }
        steps[currentStepNumber].isComplete = true;
        currentStepNumber++;
        yield return new WaitForSeconds(3);
        // show player orderwindow
        main.TutorialOrder();
        foreach (PlayerController playerController in playerControllers)
        {
            playerController.interactionText.text = "";
        }
        while (!steps[1].isComplete)
        {
            yield return null;
        }
        // show cook book
        FindObjectOfType<Cookbook>().DisplayIndicator(true);
        while (!steps[2].isComplete)
        {
            yield return null;
        }
        // bacon and pan
        FindObjectOfType<BaconPack>().DisplayIndicator(true);
        FindObjectOfType<Pan>().DisplayIndicator(true);
        while(currentNumberOfTaskCompleted < 2)
        {
            yield return null;
        }
        steps[currentStepNumber].isComplete = true;
        currentStepNumber++;
        Debug.Log("Step Completed");
        //
    }
}
