using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Variables")]
    public bool startTutorial;
    public Step[] steps;
    public Step[] playerOneSteps, playerTwoSteps;
    public int currentStepNumber;

    [Header("Items")]
    public GameObject passCounter;

    private CounterTop counterTop;
    public int currentNumberOfTaskCompleted { set; get; }

    // variables of the player
    private PlayerController[] playerControllers;
    [HideInInspector]
    public PlayerController playerOneController, playerTwoController;

    [HideInInspector]
    public Text playerOneText, playerTwoText;

    private Main main; // main variable;
    [HideInInspector]
    public int playerOneCurrentStep, playerTwoCurrentStep;

    public void Start()
    {
        main = FindObjectOfType<Main>();
        counterTop = passCounter.GetComponent<CounterTop>();
    }

    public void StartTutorial()
    {
        GetPlayers();
        PlayerOne();
        PlayerTwo();
    }

    public void GetPlayers()
    {
        playerControllers = FindObjectsOfType<PlayerController>(); // get players
        playerOneController = playerControllers[1]; // assign player
        playerTwoController = playerControllers[0]; // assign player
        playerOneText = playerOneController.interactionText; // get text
        playerTwoText = playerTwoController.interactionText; // get text
    }
    public async void PlayerOne()
    {
        // show players how to walk
        if (playerOneCurrentStep == 0)
        {
            await Task.Delay(1000); // wait one sec
            playerOneController.isDisplayingInformation = true;
            playerOneText.text = "Use [right stick] to move your character!";
            playerOneSteps[playerOneCurrentStep].isComplete = true;
            playerOneCurrentStep++;
        }

        // wait three sec
        await Task.Delay(3000);
        playerOneController.isDisplayingInformation = false;
        playerOneText.text = "";

        // show cook book
        if (playerOneCurrentStep == 1)
        {
            FindObjectOfType<Cookbook>().DisplayIndicator(true);
            while (!playerOneSteps[1].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab pan
        if (playerOneCurrentStep == 2)
        {
            FindObjectOfType<Pan>().DisplayIndicator(true);
            while (!playerOneSteps[2].isComplete)
            {
                await Task.Yield();
            }
        }

        // place pan on stove
        if (playerOneCurrentStep == 3)
        {
            FindObjectOfType<Stove>().DisplayIndicator(true);
            while (!playerOneSteps[3].isComplete)
            {
                await Task.Yield();
            }

            // waiting for bacon on counter
            while (playerTwoCurrentStep < 4)
            {
                playerOneText.text = "Waiting for Player Two";
                playerOneController.isDisplayingInformation = true;
                await Task.Yield();
            }
            playerOneText.text = "";
            playerOneController.isDisplayingInformation = false;
        }

        // grab spatula
        if (playerOneCurrentStep == 4)
        {
            FindObjectOfType<Spatula>().DisplayIndicator(true);
            while (!playerOneSteps[4].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab bacon
        if (playerOneCurrentStep == 5)
        {
            FindObjectOfType<Bacon>().DisplayIndicator(true);
            while (!playerOneSteps[5].isComplete)
            {
                await Task.Yield();
            }
            playerOneController.isDisplayingInformation = true;
            playerOneText.text = "Press [right bumper] or [left bumper] to switch items in your hand!";
            await Task.Delay(3000);
            playerOneText.text = "";
            playerOneController.isDisplayingInformation = false;
        }

        // place bacon on pan
        if (playerOneCurrentStep == 6)
        {
            FindObjectOfType<Pan>().DisplayIndicator(true);
            while (!playerOneSteps[6].isComplete)
            {
                await Task.Yield();
            }
        }

        // cooking bacon
        if(playerOneCurrentStep == 7)
        {
            while (!playerOneSteps[7].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab pan off stove
        if (playerOneCurrentStep == 8)
        {
            FindObjectOfType<Pan>().DisplayIndicator(true);
            while (!playerOneSteps[8].isComplete)
            {
                await Task.Yield();
            }
        }

        // place food on plate
        if (playerOneCurrentStep == 9)
        {
            // waiting for plate on counter
            while (playerTwoCurrentStep < 7)
            {
                playerOneText.text = "Waiting for Player Two";
                playerOneController.isDisplayingInformation = true;
                await Task.Yield();
            }
            playerOneText.text = "";
            playerOneController.isDisplayingInformation = false;

            FindObjectOfType<Plate>().DisplayIndicator(true);
            while (!playerOneSteps[9].isComplete)
            {
                await Task.Yield();
            }
        }

        Debug.Log("Step Completed");
    }
    public async void PlayerTwo()
    {
        // show players how to walk
        if (playerTwoCurrentStep == 0)
        {
            await Task.Delay(1000);
            playerTwoController.isDisplayingInformation = true;
            playerTwoText.text = "Use [right stick] to move your character!";
            playerTwoSteps[playerTwoCurrentStep].isComplete = true;
            playerTwoCurrentStep++;
        }

        // wait three seconds
        await Task.Delay(3000);
        playerTwoController.isDisplayingInformation = false;
        playerTwoText.text = "";

        // show player orderwindow
        if (playerTwoCurrentStep == 1)
        {
            main.TutorialOrder();
            while (!playerTwoSteps[1].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab bacon
        if (playerTwoCurrentStep == 2)
        {
            FindObjectOfType<BaconPack>().DisplayIndicator(true);
            while (!playerTwoSteps[2].isComplete)
            {
                await Task.Yield();
            }
        }

        // put bacon on counter
        if(playerTwoCurrentStep == 3)
        {
            counterTop.DisplayIndicator(true);
            while (!playerTwoSteps[3].isComplete)
            {
                await Task.Yield();
            }
        }
        
        // grab dirty plate
        if(playerTwoCurrentStep == 4)
        {
            FindObjectOfType<Plate>().DisplayIndicator(true);
            while (!playerTwoSteps[4].isComplete)
            {
                await Task.Yield();
            }
            await Task.Delay(3000);
            playerTwoText.text = "";
        }

        // place plate in sink
        if (playerTwoCurrentStep == 5)
        {
            FindObjectOfType<SinkScript>().DisplayIndicator(true);
            while (!playerTwoSteps[5].isComplete)
            {
                await Task.Yield();
            }
        }

        // wait for plate to finish cleaning
        while (playerTwoCurrentStep == 6)
        {
            await Task.Yield();
        }

        // place plate on counter
        if(playerTwoCurrentStep == 7)
        {
            // waiting for bacon on counter
            while (playerOneCurrentStep < 6)
            {
                playerTwoText.text = "Waiting for Player One";
                playerTwoController.isDisplayingInformation = true;
                await Task.Yield();
            }
            playerTwoText.text = "";
            playerTwoController.isDisplayingInformation = false;

            counterTop.DisplayIndicator(true);
            while (!playerTwoSteps[7].isComplete && !playerOneSteps[9].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab plate
        if(playerTwoCurrentStep == 8 && playerOneCurrentStep > 8)
        {
            FindObjectOfType<Plate>().DisplayIndicator(true);
            while (!playerTwoSteps[8].isComplete)
            {
                await Task.Yield();
            }
        }

        // place order in window
        if (playerTwoCurrentStep == 9)
        {
            FindObjectOfType<OrderManager>().DisplayIndicator(true);
            while (!playerTwoSteps[9].isComplete)
            {
                await Task.Yield();
            }
        }
        Debug.Log("Step Completed");
    }
}
