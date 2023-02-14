using UnityEngine;

public class Pan : Tool
{
    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if(itemInMainHand != null)
        {
            if(itemInMainHand.GetComponent<Spatula>() != null)
            {
                //do nothing
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
