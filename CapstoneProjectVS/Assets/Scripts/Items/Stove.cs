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

                if(pan.itemsInPan.Count > 0)
                {
                    if(pan.itemsInPan[0].GetComponent<Ingredients>().cookingStatus != Ingredients.CookingStatus.cooked)
                    {
                        pan.transform.position = toolPlacement.position;                        
                        pan.transform.parent = transform;
                        pan.gameObject.SetActive(true);
                        pan.canInteract = false;
                        pan.state = Pan.State.hot;
                        pan.CookingCheck(pan.cookingCheck, 2);
                    }
                }
            }
        }
    }
}
