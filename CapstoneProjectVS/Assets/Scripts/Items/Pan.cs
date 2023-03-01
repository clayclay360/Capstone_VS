using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Tool
{
    public Dictionary<int, Item> itemsInPan = new Dictionary<int, Item>();
    public enum State { cold, hot}
    [Header("State")]
    public State state;
    [Header("CookingCheck")]
    public GameObject cookingCheck;

    public Pan()
    {
        Name = "Pan";
        Interaction = "";
    }

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
            else if(itemInMainHand.GetComponent<Egg>() != null)
            {
                Egg egg = itemInMainHand.GetComponent<Egg>();

                if (itemsInPan.Count <= containerSize && egg.cookingStatus == Ingredients.CookingStatus.uncooked) // if pan is not full and egg is not cooked
                {
                    itemsInPan.Add(itemsInPan.Count, egg); // add egg in the pan inventory.
                    egg.transform.position = transform.position; // put egg on pan
                    egg.transform.parent = transform; // have the pan be the parent of egg
                    egg.gameObject.SetActive(true); // display pan
                    egg.canInteract = false; // egg can't not be interacted
                    egg.state = Egg.State.yoked; // change state
                    egg.SwitchModel(Egg.State.yoked); // change model
                    player.inventory[0] = null; // item in main hand is null
                }
            }
            else
            {
                //second hand is empty
                Collect(player);
                Debug.Log("Pan is here");
            }
        }
        else
        {
            //main hand is empty
            Collect(player);
        }
    }
}
