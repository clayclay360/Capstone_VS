using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class OrderManager : Item
{
    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        if(itemInMainHand != null)
        {
            if(itemInMainHand.GetComponent<Plate>() != null)
            {
                Plate plate = itemInMainHand.GetComponent<Plate>(); // get plate
                Main main = FindObjectOfType<Main>(); // find main

                // if the food on the plate is a main or side order, than the order is complete
                if (plate.foodOnPlate[0].Name == main.mainRecipe.Name ||
                    plate.foodOnPlate[0].Name == main.sideRecipeOne.Name ||
                    plate.foodOnPlate[0].Name == main.sideRecipeTwo.Name)
                {
                    main.OrderComplete(plate.foodOnPlate[0].Name);
                }
            }
        }
    }
}
