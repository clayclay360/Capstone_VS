using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Tool
{
    [Header("Plate Variables")]
    public bool Occupied;
    public Transform foodPlacement;
    public Dictionary<int, Item> foodOnPlate = new Dictionary<int, Item>();

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            // if item in hand is egg
            if (itemInMainHand.GetComponent<Pan>())
            {
                //get the food that's in the pan
                Ingredients food = itemInMainHand.GetComponent<Pan>().itemsInPan[0].GetComponent<Ingredients>();

                if (food.cookingStatus == Ingredients.CookingStatus.cooked)
                {
                    foodOnPlate.Add(foodOnPlate.Count,food);
                    food.transform.position = foodPlacement.transform.position; // put food on plate
                    food.transform.parent = transform; // have the food be the parent of egg
                    //player.inventory[0] = null; // item in main hand is null
                }
            }
        }
        else
        {
            Collect(player);
            CheckCounterTop();
            CheckSink();
        }
    }
}
