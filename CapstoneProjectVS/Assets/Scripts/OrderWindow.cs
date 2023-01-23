using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderWindow : Item
{
    private Plate plate;
    private Main main;


    public void Start()
    {
        
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController chef)
    {
        main = FindObjectOfType<Main>();
        main.orderWindowUI.SetActive(true);

        switch (item)
        {
            case PlayerController.ItemInMainHand.plate:

                if (chef.hand[0].GetComponent<Plate>() != null)
                {
                    plate = chef.hand[0].GetComponent<Plate>();

                    if (chef.hand[0].GetComponent<Plate>().Occupied && chef.hand[0].GetComponent<Plate>().foodOnPlate.status == Status.cooked) 
                    {
                        Interaction = "Submit " + plate.foodOnPlate.Name;

                        if (chef.isInteracting)
                        {
                            Debug.Log(plate.foodOnPlate.Name);

                            if(plate.foodOnPlate.Name == main.mainRecipe.Name)
                            {
                                main.OrderComplete(plate.foodOnPlate.Name);
                            }
                            else if(plate.foodOnPlate.Name == main.sideRecipeOne.Name)
                            {
                                main.OrderComplete(plate.foodOnPlate.Name);
                            }
                            else if(plate.foodOnPlate.Name == main.sideRecipeTwo.Name)
                            {
                                main.OrderComplete(plate.foodOnPlate.Name);
                            }
                            chef.hand[0].Name = "";
                        }
                    }
                }
                break;
            default:
                Debug.Log(item);
                break;
        }
    }
}
