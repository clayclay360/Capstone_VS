using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class OrderManager : Item
{
    [Header("Variables")]
    public GameObject orderWindow;
    
    [Header("UX")]
    public Indicator indicator;
    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        if(itemInMainHand != null)
        {
            if(itemInMainHand.GetComponent<Plate>() != null)
            {
                Plate plate = itemInMainHand.GetComponent<Plate>(); // get plate
                Main main = FindObjectOfType<Main>(); // find main

                // if the food on the plate is a main or side order, then the order is complete
                for(int i = 0; main.sideRecipe.Length > i; i++)
                {
                    if (main.sideRecipe[i] != null)
                    {
                        if (plate.foodOnPlate[0].Name == main.sideRecipe[i].Name)
                        {
                            main.OrderComplete(plate.foodOnPlate[0].Name, plate.foodOnPlate[0].GetComponent<Ingredients>());
                            if (plate.foodOnPlate[0].Name == "Omelet" || plate.foodOnPlate[0].Name == "Bacon" || plate.foodOnPlate[0].Name == "Toast")
                            {
                                Ingredients ingredient;
                                ingredient = plate.foodOnPlate[0].GetComponent<Ingredients>();
                                plate.foodOnPlate[0].transform.parent = null;
                                plate.foodOnPlate[0].canInteract = true;
                                plate.foodOnPlate.Remove(0);
                                //plate.foodOnPlate[0] = null;
                                plate.isDirtied();
                                ingredient.RespawnIngredient();
                                ingredient.gameObject.SetActive(true);
                                ingredient.isBeingUsed = false;
                                break;
                            }
                        }
                    }
                }
                //if (plate.foodOnPlate[0].Name == main.mainRecipe.Name ||
                //    plate.foodOnPlate[0].Name == main.sideRecipeTwo.Name||
                //    plate.foodOnPlate[0].Name == main.sideRecipeOne.Name ||
                //    plate.foodOnPlate[0].Name == main.sideRecipeThree.Name)
                //{
                //    Debug.Log("Order Complete");
                //    main.OrderComplete(plate.foodOnPlate[0].Name);
                //    if (plate.foodOnPlate[0].Name == "Omelet" || plate.foodOnPlate[0].Name == "Bacon" || plate.foodOnPlate[0].Name == "Toast")
                //    {
                //        Ingredients ingredient;
                //        ingredient = plate.foodOnPlate[0].GetComponent<Ingredients>();
                //        plate.foodOnPlate[0].transform.parent = null;
                //        plate.foodOnPlate[0].canInteract = true;
                //        plate.foodOnPlate[0] = null;
                //        plate.isDirtied();
                //        ingredient.RespawnIngredient();
                //        ingredient.gameObject.SetActive(true);
                //        ingredient.isBeingUsed = false;
                //    }
                //}
            }
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        orderWindow.GetComponent<CanvasGroup>().alpha = 1;
        DisplayIndicator(false);
    }

    public void DisplayIndicator(bool condition)
    {
        indicator.arrow.SetActive(condition);
    }
}
