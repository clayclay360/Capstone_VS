using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : Ingredients
{
    [Header("Models")]
    public GameObject uncookedModel;
    public GameObject cookedModel;
    public GameObject burntModel;

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
            else if (itemInMainHand.GetComponent<Pan>() != null)
            {
                Collect(player);
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

    //public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    //{
    //    switch (item)
    //    {
    //        case PlayerController.ItemInMainHand.empty:
    //            Interaction = "Grab Bacon";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.pan:
    //            Interaction = "Grab Bacon";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.spatula:
    //            Interaction = "Grab Bacon";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.egg:
    //            Interaction = "Grab Bacon";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.hashbrown:
    //            Interaction = "Grab Bacon";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //    }
    //}



    // this is temporary for now
    public override void ChangeStatus()
    {
        cookingStatus = CookingStatus.cooked;

        switch (cookingStatus)
        {
            case CookingStatus.cooked:
                cookedModel.SetActive(true);
                uncookedModel.SetActive(false);
                break;
            case CookingStatus.burnt:
                cookedModel.SetActive(false);
                burntModel.SetActive(true);
                break;
        }
    }
}
