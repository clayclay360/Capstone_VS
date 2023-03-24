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
            if(itemInMainHand.GetComponent<Pan>())
            {
                //get the food that's in the pan
                if (itemInMainHand.GetComponent<Pan>().itemsInPan.Count > 0)
                {
                    Ingredients food = itemInMainHand.GetComponent<Pan>().itemsInPan[0].GetComponent<Ingredients>();

                    if (food.cookingStatus == Ingredients.CookingStatus.cooked)
                    {
                        foodOnPlate.Add(foodOnPlate.Count, food);
                        food.transform.position = foodPlacement.transform.position; // put food on plate
                        food.transform.parent = transform; // have the food be the parent of egg
                        itemInMainHand.GetComponent<Pan>().itemsInPan.Remove(0);
                        return;
                    }
                }
            }
            else if (!player.inventoryFull)
            {
                if (itemInMainHand.GetComponent<Ingredients>())
                {
                    Collect(player);
                }
                else if (itemInMainHand.GetComponent<Tool>())
                {
                    Collect(player);
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

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        if (!player.inventory[0] || !player.inventory[1])
        {
            Interaction = "Grab Plate";
            if (player.isInteracting)
            {
                player.isInteracting = false;
                player.canInteract = false;
                Interaction = "";
            }
        }
        //There is no item in the pan(pre-cooking)
        if (player.inventory[0] && player.inventory[0].TryGetComponent<Pan>(out Pan pan))
        {
            if (pan.itemsInPan.Count > 0)
            {
                Interaction = $"Add {pan.itemsInPan[0].Name} to plate";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                }
                return;
            }
            else
            {
                Interaction = "Inventory Full";
            }
        }
        else if (player.inventoryFull && !player.inventory[0].GetComponent<Ingredients>())
        {
            Interaction = "Inventory Full";
            return;
        }
    }
}
