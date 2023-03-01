using System.Collections;
using System.Collections.Generic;
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
                Interaction = "Grab Potato";
                Collect(player);
            }
            //if pan is in main hand
            else if(itemInMainHand.GetComponent<Pan>() != null)
            {
                Interaction = "Grab Potato";
                Collect(player);
            }
            else
            {
                Interaction = "Grab Potato";
                //second hand is empty
                Collect(player);
            }
        }
        else
        {
            Interaction = "Grab Potato";
            //main hand is empty
            Collect(player);
        }
    }


    //public void Collect(PlayerController player)
    //{
    //    throw new System.NotImplementedException();
    //}
}
