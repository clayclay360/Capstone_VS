using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Utilities
{
    [Header("Stove Variables")]
    public Transform toolPlacement;
    public bool isOccupied;

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            if(itemInMainHand.GetComponent<Pan>() != null)
            {
                Pan pan = itemInMainHand.GetComponent<Pan>();

                pan.transform.position = toolPlacement.position; // position pan
                pan.gameObject.SetActive(true); // activate pan
                pan.transform.parent = transform; // make pan child of stove
                pan.isHot = true; // pan is hot
                player.inventory[0] = null; // item in main hand is null
                pan.stove = this;

                isOccupied = true;
                //isValidTarget = true;

                if (pan.itemsInPan.Count > 0)
                {
                    if(pan.itemsInPan[0].GetComponent<Ingredients>().cookingStatus != Ingredients.CookingStatus.cooked)
                    {
                        pan.CookingCheck(pan.cookingCheck, 2, pan.itemsInPan[0].GetComponent<Ingredients>()); // start cooking check // the cook time is 2 temporary
                        pan.itemsInPan[0].GetComponent<Ingredients>().isCooking = true; // food is cooking
                    }
                }
            }
        }
        player.isInteracting = false;
    }

    public override void Update()
    {
        base.Update();
        
        // if the stove is occupied the player can no longer interact with it
        canInteract = Interactivity();
    }

    public bool Interactivity()
    {
        if (isOccupied)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ratInteraction(RatController rat)
    {
        if (GetComponentInChildren<Pan>() != null)
        {
            Pan panScript = GetComponentInChildren<Pan>();
            GameObject pan = panScript.gameObject;
            if(panScript.itemsInPan != null)
            {
                GameObject ingredient = pan.GetComponentInChildren<Ingredients>().gameObject;
                ingredient.transform.SetParent(null);
                rat.ratInventory = ingredient;
                panScript.itemsInPan.Remove(panScript.itemsInPan.Count);
                Debug.Log(panScript.itemsInPan);
            }
            else
            {
                rat.ratInventory = pan;
                pan.transform.SetParent(rat.transform);
            }
        }

    }
}
