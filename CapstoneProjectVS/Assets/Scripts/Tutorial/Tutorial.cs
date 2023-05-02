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

    [Header("Sprites")]
    public Sprite xButtonSprite;
    public Sprite analogSprite;

    [Header("UI")]
    public GameObject gameMenu;

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
            playerOneController.interactButtonIcon.sprite = analogSprite;
            playerOneText.text = "Use Right Stick to move your character!";
            playerOneSteps[playerOneCurrentStep].isComplete = true;
            playerOneCurrentStep++;
        }

        // wait three sec
        await Task.Delay(3000);
        playerOneController.isDisplayingInformation = false;
        playerOneText.text = "";
        playerOneController.interactButtonIcon.sprite = xButtonSprite;

        // show cook book
        if (playerOneCurrentStep == 1)
        {
            playerOneController.GuideArrow(true, FindObjectOfType<Cookbook>().transform);
            FindObjectOfType<Cookbook>().DisplayIndicator(true);
            FindObjectOfType<Cookbook>().canInteract = true;
            while (!playerOneSteps[1].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab pan
        if (playerOneCurrentStep == 2)
        {
            playerOneController.GuideArrow(true, FindObjectOfType<Pan>().transform);
            FindObjectOfType<Pan>().DisplayIndicator(true);
            FindObjectOfType<Pan>().canInteract = true;
            while (!playerOneSteps[2].isComplete)
            {
                await Task.Yield();
            }
        }

        // place pan on stove
        if (playerOneCurrentStep == 3)
        {
            playerOneController.GuideArrow(true, FindObjectOfType<Stove>().transform);
            FindObjectOfType<Stove>().DisplayIndicator(true);
            FindObjectOfType<Stove>().canInteract = true;
            while (!playerOneSteps[3].isComplete)
            {
                await Task.Yield();
            }

            // waiting for bacon on counter
            while (playerTwoCurrentStep < 4)
            {
                playerOneText.text = "Waiting for Player Two";
                playerOneController.isDisplayingInformation = true;
                playerOneController.GuideArrow(false);
                await Task.Yield();
            }
            playerOneText.text = "";
            playerOneController.isDisplayingInformation = false;
        }

        // grab spatula
        if (playerOneCurrentStep == 4)
        {
            playerOneController.GuideArrow(true, FindObjectOfType<Spatula>().transform);
            FindObjectOfType<Spatula>().DisplayIndicator(true);
            FindObjectOfType<Spatula>().canInteract = true;
            while (!playerOneSteps[4].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab bacon
        if (playerOneCurrentStep == 5)
        {
            playerOneController.GuideArrow(true, FindObjectOfType<Bacon>().transform);
            FindObjectOfType<Bacon>().DisplayIndicator(true);
            FindObjectOfType<Bacon>().canInteract = true;
            while (!playerOneSteps[5].isComplete)
            {
                await Task.Yield();
            }
            playerTwoController.canInteract = false;
            playerOneController.GuideArrow(false);
            playerOneController.isDisplayingInformation = true;
            playerOneText.text = "Press Right Bumper or Left Bumper to switch items in your hand!";
            playerOneController.GuideArrow(false);
            await Task.Delay(3000);
            playerTwoController.canInteract = true;
            playerOneText.text = "";
            playerOneController.isDisplayingInformation = false;
        }

        // place bacon on pan
        if (playerOneCurrentStep == 6)
        {
            playerOneController.GuideArrow(true, FindObjectOfType<Pan>().transform);
            FindObjectOfType<Pan>().DisplayIndicator(true);
            FindObjectOfType<Pan>().canInteract = true;
            while (!playerOneSteps[6].isComplete)
            {
                await Task.Yield();
            }
            playerOneController.GuideArrow(false);
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
            playerOneController.GuideArrow(true, FindObjectOfType<Pan>().transform);
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
            while (playerTwoCurrentStep < 8)
            {
                playerOneText.text = "Waiting for Player Two";
                playerOneController.isDisplayingInformation = true;
                playerOneController.GuideArrow(false);
                await Task.Yield();
            }
            playerOneText.text = "";
            playerOneController.isDisplayingInformation = false;

            playerOneController.GuideArrow(true, FindObjectOfType<Plate>().transform);
            FindObjectOfType<Plate>().DisplayIndicator(true);
            FindObjectOfType<Plate>().canInteract = true;
            while (!playerOneSteps[9].isComplete)
            {
                await Task.Yield();
            }
        }

        playerOneController.GuideArrow(false);
        Debug.Log("Step Completed");
    }
    public async void PlayerTwo()
    {
        // show players how to walk
        if (playerTwoCurrentStep == 0)
        {
            await Task.Delay(1000);
            playerTwoController.isDisplayingInformation = true;
            playerTwoController.interactButtonIcon.sprite = analogSprite;
            playerTwoText.text = "Use Right Stick to move your character!";
            playerTwoSteps[playerTwoCurrentStep].isComplete = true;
            playerTwoCurrentStep++;
        }

        // wait three seconds
        await Task.Delay(3000);
        playerTwoController.isDisplayingInformation = false;
        playerTwoText.text = "";
        playerTwoController.interactButtonIcon.sprite = xButtonSprite;

        // show player orderwindow
        if (playerTwoCurrentStep == 1)
        {
            playerTwoController.GuideArrow(true, FindObjectOfType<OrderManager>().transform);
            FindObjectOfType<OrderManager>().canInteract = true;
            main.TutorialOrder();
            while (!playerTwoSteps[1].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab bacon
        if (playerTwoCurrentStep == 2)
        {
            playerTwoController.GuideArrow(true, FindObjectOfType<BaconPack>().transform);
            FindObjectOfType<BaconPack>().DisplayIndicator(true);
            FindObjectOfType<Bacon>().canInteract = true;
            while (!playerTwoSteps[2].isComplete)
            {
                await Task.Yield();
            }
        }

        // put bacon on counter
        if(playerTwoCurrentStep == 3)
        {
            playerTwoController.GuideArrow(true, counterTop.transform);
            counterTop.DisplayIndicator(true);
            while (!playerTwoSteps[3].isComplete)
            {
                await Task.Yield();
            }
        }
        
        // grab dirty plate
        if(playerTwoCurrentStep == 4)
        {
            playerTwoController.GuideArrow(true, FindObjectOfType<Plate>().transform);
            FindObjectOfType<Plate>().DisplayIndicator(true);
            FindObjectOfType<Plate>().canInteract = true;
            while (!playerTwoSteps[4].isComplete)
            {
                await Task.Yield();
            }
            playerTwoController.GuideArrow(false);
            await Task.Delay(3000);
            playerTwoText.text = "";
        }

        // place plate in sink
        if (playerTwoCurrentStep == 5)
        {
            playerTwoController.GuideArrow(true, FindObjectOfType<SinkScript>().transform);
            FindObjectOfType<SinkScript>().DisplayIndicator(true);
            FindObjectOfType<SinkScript>().canInteract = true;
            while (!playerTwoSteps[5].isComplete)
            {
                await Task.Yield();
            }
            playerTwoController.GuideArrow(false);
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
                playerTwoController.GuideArrow(false);
                await Task.Yield();
            }
            playerTwoText.text = "";
            playerTwoController.isDisplayingInformation = false;

            playerTwoController.GuideArrow(true, counterTop.transform);
            counterTop.DisplayIndicator(true);
            while (!playerTwoSteps[7].isComplete)
            {
                await Task.Yield();
            }
        }

        // grab plate
        if(playerTwoCurrentStep == 8)
        {
            // wait till player one puts plate on counter
            while(playerOneCurrentStep < 10)
            {
                await Task.Yield();
                playerTwoController.GuideArrow(false);
                playerTwoText.text = "Waiting for Player One";
                playerTwoController.isDisplayingInformation = true;
            }

            Debug.Log(playerOneCurrentStep);
            playerTwoText.text = "";
            playerTwoController.isDisplayingInformation = false;
            playerTwoController.GuideArrow(true, FindObjectOfType<Plate>().transform);
            FindObjectOfType<Plate>().DisplayIndicator(true);
            
            while (!playerTwoSteps[8].isComplete)
            {
                await Task.Yield();
            }
        }

        // place order in window
        if (playerTwoCurrentStep == 9)
        {
            playerTwoController.GuideArrow(true, FindObjectOfType<OrderManager>().transform);
            FindObjectOfType<OrderManager>().DisplayIndicator(true);
            while (!playerTwoSteps[9].isComplete)
            {
                await Task.Yield();
            }
            Debug.Log("Combat Time");
            playerTwoController.GuideArrow(false);
            Combat();
        }

        Debug.Log("Step Completed");
    }

    public async void Combat()
    {
        #region diplay
        playerOneController.canInteract = false;
        playerTwoController.canInteract = false;
        playerOneController.isDisplayingInformation = true;
        playerTwoController.isDisplayingInformation = true;
        playerOneText.text = "Great Job!";
        playerTwoText.text = "Great Job!";
        await Task.Delay(3000);
        playerOneText.text = "";
        playerTwoText.text = "";
        await Task.Delay(1500);
        playerOneText.text = "Watch out for rats! Press [right bumper] top throw knives!";
        playerTwoText.text = "Watch out for rats! Press [right bumper] top throw knives!";
        await Task.Delay(3000);
        playerOneText.text = "Be careful! You can't throw knives when both hands are full";
        playerTwoText.text = "Be careful! You can't throw knives when both hands are full";
        await Task.Delay(3000);
        playerOneText.text = "";
        playerTwoText.text = "";
        playerOneController.canInteract = true;
        playerTwoController.canInteract = true;
        playerOneController.isDisplayingInformation = false;
        playerTwoController.isDisplayingInformation = false;
        #endregion

        // spawn rates
        foreach(RatSpawn rs in FindObjectsOfType<RatSpawn>())
        {
            rs.enabled = true;
        }

        while(GameManager.numberOfRatsKilled < 3)
        {
            await Task.Yield();
        }

        // game ends here display menu
        Debug.Log("Game Over");
        foreach (RatSpawn rs in FindObjectsOfType<RatSpawn>())
        {
            rs.enabled = false;
        }
        GameManager.gameStarted = false;
        gameMenu.SetActive(true);
    }
}
