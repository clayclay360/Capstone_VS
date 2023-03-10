using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class HashBrown : Ingredients, IInteractable
{
    
    public HashBrown()
    {
        Name = "HashBrown";
        Interaction = "";
    }

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            //if spatula is in main hand
            if (itemInMainHand.GetComponent<Spatula>() != null)
            {
                Collect(player);
            }
            //if pan is in main hand
            else if(itemInMainHand.GetComponent<Pan>() != null)
            {
                Collect(player);
            }
            else
            {
                //second hand is empty
                Collect(player);
            }
        }
        else
        {
            //main hand is empty
            Collect(player);
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player) 
    {
        switch (item)
        {
            case PlayerController.ItemInMainHand.empty:
                Interaction = "Grab Potato";
                if (player.isInteracting)
                {
                    player.canInteract = false;
                    gameObject.SetActive(false);
                    Interaction = "";
                }
                break;
            case PlayerController.ItemInMainHand.pan:
                Interaction = "Grab Potato";
                if (player.isInteracting)
                {
                    player.canInteract = false;
                    gameObject.SetActive(false);
                    Interaction = "";
                }
                break;
            case PlayerController.ItemInMainHand.spatula:
                Interaction = "Grab Potato";
                if (player.isInteracting)
                {
                    player.canInteract = false;
                    gameObject.SetActive(false);
                    Interaction = "";
                }
                break;
            case PlayerController.ItemInMainHand.egg:
                Interaction = "Grab Potato";
                if (player.isInteracting)
                {
                    player.canInteract = false;
                    gameObject.SetActive(false);
                    Interaction = "";
                }
                break;
            case PlayerController.ItemInMainHand.bacon:
                Interaction = "Grab Potato";
                if (player.isInteracting)
                {
                    player.canInteract = false;
                    gameObject.SetActive(false);
                    Interaction = "";
                }
                break;
        }
    }

    //public virtual void SwitchModel(State currentState)
    //{

    //}

    //public void Collect(PlayerController player)
    //{
    //    throw new System.NotImplementedException();
    //}
}
