using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Utilities
{
    public Transform toolPlacement;

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            if(itemInMainHand.GetComponent<Pan>() != null)
            {
                Pan pan = itemInMainHand.GetComponent<Pan>();

                pan.transform.position = toolPlacement.position; // position pan            
                pan.transform.parent = transform; // make pan child of stove
                pan.gameObject.SetActive(true); // activate pan
                pan.isHot = true; // pan is hot
                player.inventory[0] = null; // item in main hand is null
                canInteract = false;
                isValidTarget = true;

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
    }
}
