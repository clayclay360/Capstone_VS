using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : Tool
{
    [Header("UX")]
    public GameObject indicator;

    public Spatula()
    {
        Name = "Spatula";
        Interaction = "";
        isWashable = true;
        status = Status.clean;
    }

    public void Start()
    {
        
        useBeforeDirty = 1;
    }

    public void Update()
    {
        //if (timesUsed >= useBeforeDirty)
        //{
        //    isDirty = true;
        //}
        //else
        //{
        //    isDirty = false;
        //}
        
        switch (status)
        {
            case Status.clean:
                mainSprite = clean;
                break;

            case Status.dirty: 
                mainSprite = dirty; 
                break;
        }
    }

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            //if pan is in main hand
            if (itemInMainHand.GetComponent<Pan>() != null)
            {
                Collect(player);
                CheckCounterTop();
                CheckSink();
            }
            else
            {
                //second hand is empty
                Collect(player);
                CheckCounterTop();
                CheckSink();
            }
        }
        else
        {
            //main hand is empty
            Collect(player);
            CheckCounterTop();
            CheckSink();
        }

        // in tutorial level when the cook book is closed, player moves on to the next step
        if (GameManager.tutorialLevel)
        {
            DisplayIndicator(false);
            Tutorial tutorial = FindObjectOfType<Tutorial>();
            if (tutorial.currentStepNumber == 5)
            {
                tutorial.steps[tutorial.currentStepNumber].isComplete = true;
                tutorial.currentStepNumber++;
            }
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "Cooking Check");

        if (player.inventoryFull)
        {
            Interaction = "Inventory Full";
            return;
        }

        switch (item)
        {
            case PlayerController.ItemInMainHand.empty:
                Interaction = "Grab Spatula";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.pan:
                Interaction = "Grab Spatula";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.hashbrown:
                Interaction = "Grab Spatula";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.egg:
                Interaction = "Grab Spatula";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.bacon:
                Interaction = "Grab Spatula";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
        }

        
    }

    public void DisplayIndicator(bool condition)
    {
        indicator.SetActive(condition);
    }

    public override void IsDirtied()
    {
        if (timesUsed >= useBeforeDirty)
        {
            status = Status.dirty;
            isDirty = true;
            Interaction = "Spatula is dirty";
        }
    }


    public override void IsClean()
    {

        status = Status.clean;
        timesUsed = 0;
        isDirty = false;
        
    }

}
