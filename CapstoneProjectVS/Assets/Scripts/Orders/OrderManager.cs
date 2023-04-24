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
                                plate.IsDirtied();
                                ingredient.RespawnIngredient();
                                ingredient.gameObject.SetActive(true);
                                ingredient.isBeingUsed = false;
<<<<<<< Updated upstream
                                plate.timesUsed += 1;
                                plate.IsDirtied();
=======

                                // if in the tutorial level
                                if (GameManager.tutorialLevel)
                                {
                                    DisplayIndicator(false);
                                    Tutorial tutorial = FindObjectOfType<Tutorial>();

                                    if (tutorial.playerTwoCurrentStep == 9)
                                    {
                                        tutorial.playerTwoSteps[9].isComplete = true;
                                        //tutorial.playerTwoCurrentStep++;
                                    }
                                }
>>>>>>> Stashed changes
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
        if (player.inventory[0] && player.inventory[0].TryGetComponent<Plate>(out Plate plate))
        {
            plate = player.inventory[0].GetComponent<Plate>();
            Debug.Log("Plate in Hand");

            if(plate.foodOnPlate.Count > 0)
            {
                Interaction = "Submit " + plate.foodOnPlate[0].Name + " Order";
            }
            else
            {
                Interaction = "";
            }
        }
        orderWindow.GetComponent<CanvasGroup>().alpha = 1;
        DisplayIndicator(false);

        // if in the tutorial level, if the current setp is 1 then mark it complete
        if (GameManager.tutorialLevel)
        {
            Tutorial tutorial = FindObjectOfType<Tutorial>();

            if(tutorial.playerTwoCurrentStep == 1)
            {
                tutorial.playerTwoSteps[tutorial.playerTwoCurrentStep].isComplete = true;
                tutorial.playerTwoCurrentStep++;
            }
        }
    }

    public void DisplayIndicator(bool condition)
    {
        indicator.arrow.SetActive(condition);
    }
}
