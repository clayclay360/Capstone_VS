using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCarton : Ingredients, IInteractable
{
    public GameObject eggGO;
    Egg egg;

    public void Start()
    {
        Interaction = "";
        Name = "EggCarton";
        egg = eggGO.GetComponent<Egg>();

        egg.isBeingUsed = false;
    }

    public void Update()
    {
        if (!egg.isBeingUsed)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        egg = eggGO.GetComponent<Egg>();
        if (!egg.isBeingUsed)
        {
            //check to see if there's anything in the mainhand
            if (itemInMainHand != null)
            {
                //if spatula is in main hand
                if (itemInMainHand.GetComponent<Spatula>() != null)
                {
                    egg.isBeingUsed = true;
                    egg.Collect(player);
                }
                //if pan is in main hand
                else if (itemInMainHand.GetComponent<Pan>() != null)
                {
                    egg.isBeingUsed = true;
                    egg.Collect(player);
                }
                else
                {
                    //second hand is empty
                    egg.isBeingUsed = true;
                    egg.Collect(player);
                }
            }
            else
            {
                //main hand is empty
                egg.isBeingUsed = true;
                egg.Collect(player);
            }
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        switch (item)
        {
            case PlayerController.ItemInMainHand.empty:
                Interaction = "Grab Egg";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.pan:
                Interaction = "Grab Egg";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.spatula:
                Interaction = "Grab Egg";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.hashbrown:
                Interaction = "Grab Egg";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.bacon:
                Interaction = "Grab Egg";
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
