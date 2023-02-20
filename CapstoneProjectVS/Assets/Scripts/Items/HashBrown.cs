using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashBrown : Item, ICollectable
{

    public HashBrown()
    {

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
                Pan pan = itemInMainHand.GetComponent<Pan>();

                if(pan.itemsInPan.Count >= pan.containerSize)
                {
                    
                }
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

    public void Collect(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
