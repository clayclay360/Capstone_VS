using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : Tool
{

    public Spatula()
    {
        Name = "Spatula";
        Interaction = "";
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
}
