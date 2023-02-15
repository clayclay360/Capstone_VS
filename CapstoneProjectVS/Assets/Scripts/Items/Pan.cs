using System.Collections.Generic;
using UnityEngine;

public class Pan : Tool
{
    public Dictionary<int, Item> itemsInPan = new Dictionary<int, Item>();

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if(itemInMainHand != null)
        {
            //if spatula is in main hand
            if(itemInMainHand.GetComponent<Spatula>() != null)
            {
                //if Pan is not empty and is hot
                if (itemsInPan.Count > 0 && isHot)
                {

                }
                else
                {
                    Collect(player);
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
}
