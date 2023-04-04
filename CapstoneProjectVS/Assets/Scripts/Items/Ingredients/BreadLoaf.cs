using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadLoaf : Ingredients, IInteractable
{
    public GameObject toastGO;
    Toast toast;

    public override void Start()
    {
        base.Start();
        Interaction = "";
        Name = "BreadLoaf";
        toast = toastGO.GetComponent<Toast>();

        toast.isBeingUsed = false;
    }

    public void Update()
    {
        if (!toast.isBeingUsed)
        {
            canInteract = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
        } else
        {
            canInteract = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        toast = toastGO.GetComponent<Toast>();
        if (!toast.isBeingUsed)
        {
            //check to see if there's anything in the mainhand
            if (itemInMainHand != null)
            {
                //if spatula is in main hand
                if (itemInMainHand.GetComponent<Spatula>() != null)
                {
                    toast.isBeingUsed = true;
                    toast.Collect(player);
                }
                //if pan is in main hand
                else if (itemInMainHand.GetComponent<Pan>() != null)
                {
                    toast.isBeingUsed = true;
                    toast.Collect(player);
                }
                else
                {
                    //second hand is empty
                    toast.isBeingUsed = true;
                    toast.Collect(player);
                }
            }
            else
            {
                //main hand is empty
                toast.isBeingUsed = true;
                toast.Collect(player);
            }
        }
        outline.enabled = false;
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        switch (item)
        {
            case PlayerController.ItemInMainHand.empty:
                Interaction = "Grab Bread";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.pan:
                Interaction = "Grab Bread";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.spatula:
                Interaction = "Grab Bread";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.hashbrown:
                Interaction = "Grab Bread";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.bacon:
                Interaction = "Grab Bread";
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
