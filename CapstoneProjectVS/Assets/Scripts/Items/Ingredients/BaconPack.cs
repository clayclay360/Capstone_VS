using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaconPack : Ingredients, IInteractable
{
    public GameObject baconGO;
    Bacon bacon;

    [Header("UX")]
    public GameObject indicator;

    public override void Start()
    {
        base.Start();
        Interaction = "";
        Name = "BaconPack";
        bacon = baconGO.GetComponent<Bacon>();

        bacon.isBeingUsed = false;
    }

    public void Update()
    {
        if (!bacon.isBeingUsed)
        {
            canInteract = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            canInteract = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
        //Debug.Log(isBeingUsed);
    }

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        bacon = baconGO.GetComponent<Bacon>();
        if (!bacon.isBeingUsed)
        {
            //check to see if there's anything in the mainhand
            if (itemInMainHand != null)
            {
                //if spatula is in main hand
                if (itemInMainHand.GetComponent<Spatula>() != null)
                {
                    bacon.isBeingUsed = true;
                    bacon.Collect(player);
                }
                //if pan is in main hand
                else if (itemInMainHand.GetComponent<Pan>() != null)
                {
                    bacon.isBeingUsed = true;
                    bacon.Collect(player);
                }
                else
                {
                    //second hand is empty
                    bacon.isBeingUsed = true;
                    bacon.Collect(player);
                }
            }
            else
            {
                //main hand is empty
                bacon.isBeingUsed = true;
                bacon.Collect(player);
            }
            bacon.isBeingUsed = true;
        }
        outline.enabled = false;

        // Tutorial Level
        DisplayIndicator(false);

        if (GameManager.tutorialLevel)
        {
            Tutorial tutorial = FindObjectOfType<Tutorial>();

            // if on step 2 then complete task
            if (tutorial.playerTwoCurrentStep == 2)
            {
                tutorial.playerTwoSteps[tutorial.playerTwoCurrentStep].isComplete = true;
                tutorial.playerTwoCurrentStep++;

                tutorial.playerTwoController.isDisplayingInformation = true;
                tutorial.playerTwoText.text = "You can only take one of each ingredient at a time!";
                StartCoroutine(TurnOffDisplay());
            }
        }
    }

    public IEnumerator TurnOffDisplay()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<Tutorial>().playerTwoText.text = "";
        FindObjectOfType<Tutorial>().playerTwoController.isDisplayingInformation = false;
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        if (player.inventoryFull)
        {
            Interaction = "Inventory Full";
            return;
        }

        switch (item)
        {
            case PlayerController.ItemInMainHand.empty:
                Interaction = "Grab Bacon";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.pan:
                Interaction = "Grab Bacon";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.spatula:
                Interaction = "Grab Bacon";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.hashbrown:
                Interaction = "Grab Bacon";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.bacon:
                Interaction = "Grab Bacon";
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
}
