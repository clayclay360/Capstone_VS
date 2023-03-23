using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : Tool
{
    public Spatula()
    {
        Name = "Spatula";
        Interaction = "";
        isWashable = true;
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


}
