using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : Utilities, IUtility, ICookable
{
    [Header("Toaster Variables")]
    public bool isOccupied;
    public Toast toast;

    [Header("CookingCheck")]
    public GameObject cookingCheck;

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            if (itemInMainHand.GetComponent<Toast>() != null)
            {
                toast = itemInMainHand.GetComponent<Toast>();
                toast.transform.parent = transform; // have the pan be the parent of egg
                toast.canInteract = false; // egg can't not be interacted
                player.inventory[0] = null; // item in main hand is null
                player.isInteracting = false; //player is no longer interacting
                isOccupied = true;
                isValidTarget = true;

                CookingCheck(cookingCheck, 2, toast); // start cooking // the cook time is 2 temporary
            }
        }
        else
        {
            if(toast.state == Toast.State.toasted)
            {
                toast.Collect(player);
                isValidTarget = false;
                isOccupied = false;
                toast = null;
                outline.enabled = false;
            }
        }
    }

    public void CookingCheck(GameObject cookingCheck, float cookTime, Ingredients food)
    {
        //reset everything
        cookingCheck.SetActive(true); // display cooking check
        ToastCheck cookingCheckScript = cookingCheck.GetComponent<ToastCheck>(); // get cooking script
        cookingCheckScript.food = food;
        cookingCheckScript.StartCooking();
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "Using the Toaster");

        if ((player.inventory[0] && player.inventory[0].TryGetComponent<Toast>(out _)) ||
            (player.inventory[1] && player.inventory[1].TryGetComponent<Toast>(out _)))
        {
            Toast handToast = null;
            if (player.inventory[0].GetComponent<Toast>() != null)
            {
                handToast = player.inventory[0].GetComponent<Toast>();
            }
            else if (player.inventory[1].GetComponent<Toast>() != null)
            {
                handToast = player.inventory[1].GetComponent<Toast>();
            }

            if (handToast != null)
            {
                if (handToast.state == Toast.State.slice)
                {
                    Interaction = "Place Bread in Toaster";
                }
                else
                {
                    Interaction = "Toast is already Toasted";
                }
            }
        }
        else if (toast != null && toast.state == Toast.State.toasted)
        {
            Interaction = "Grab Toast";
        }
        else
        {
            Interaction = "";
        }
    }

    public void StartHighlight() 
    {
        outline.OutlineWidth = 3f;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.cyan; //Miro says yellow, cyan for visibility on the counter
        outline.enabled = true;
    }

    public void RatInteraction(RatController rat)
    {
        if (isOccupied)
        {
            toast.Collect(null, rat);
            if (cookingCheck.activeInHierarchy)
            {
                cookingCheck.GetComponent<ToastCheck>().StopCooking();
                cookingCheck.SetActive(false);
            }
            toast.cookingStatus = Ingredients.CookingStatus.spoiled;
            isOccupied = false;
            isValidTarget = false;
        }
    }
}
