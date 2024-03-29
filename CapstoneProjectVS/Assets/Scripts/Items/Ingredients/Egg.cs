using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Ingredients, IInteractable
{
    public enum State { shell, omelet, scrambled, yoked };
    [Header("State")]
    public State state;

    [Header("Models")]
    public GameObject shellModel;
    public GameObject omeletModel;
    public GameObject scrambledModel;
    public GameObject yokedModel;

    public Egg() 
    {
        Name = "Egg";
        Interaction = "";
        cookingStatus = CookingStatus.uncooked;
    }

    public override void Update()
    {
        base.Update();
        switch (cookingStatus)
        {
            case CookingStatus.uncooked:
                mainSprite = uncooked;
                break;

            case CookingStatus.cooked:
                mainSprite = cooked;
                break;

            case CookingStatus.spoiled:
                mainSprite = spoiled;
                break;

            case CookingStatus.burnt:
                mainSprite = burnt;
                break;
        }
    }
    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            //if spatula is in main hand
            if (itemInMainHand.GetComponent<Spatula>() != null)
            {
                Collect(player);
                CheckCounterTop();
            }
            //if pan is in main hand
            else if (itemInMainHand.GetComponent<Pan>() != null)
            {
                Collect(player);
                CheckCounterTop();
            }
            else
            {
                //second hand is empty
                Collect(player);
                CheckCounterTop();
            }
        }
        else
        {
            //main hand is empty
            Collect(player);
            CheckCounterTop();
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "Placing food on Pan");
        base.CheckHand(item, player);
        //if (player.inventoryFull)
        //{
        //    Interaction = "Inventory Full";
        //    return;
        //}

        //switch (item)
        //{
        //    case PlayerController.ItemInMainHand.empty:
        //        Interaction = "Grab Egg";
        //        if (player.isInteracting)
        //        {
        //            player.isInteracting = false;
        //            player.canInteract = false;
        //            Interaction = "";
        //            gameObject.SetActive(false);
        //        }
        //        break;
        //    case PlayerController.ItemInMainHand.pan:
        //        Interaction = "Grab Egg";
        //        if (player.isInteracting)
        //        {
        //            player.isInteracting = false;
        //            player.canInteract = false;
        //            Interaction = "";
        //            gameObject.SetActive(false);
        //        }
        //        break;
        //    case PlayerController.ItemInMainHand.spatula:
        //        Interaction = "Grab Egg";
        //        if (player.isInteracting)
        //        {
        //            player.isInteracting = false;
        //            player.canInteract = false;
        //            Interaction = "";
        //            gameObject.SetActive(false);
        //        }
        //        break;
        //    case PlayerController.ItemInMainHand.hashbrown:
        //        Interaction = "Grab Egg";
        //        if (player.isInteracting)
        //        {
        //            player.isInteracting = false;
        //            player.canInteract = false;
        //            Interaction = "";
        //            gameObject.SetActive(false);
        //        }
        //        break;
        //    case PlayerController.ItemInMainHand.bacon:
        //        Interaction = "Grab Egg";
        //        if (player.isInteracting)
        //        {
        //            player.isInteracting = false;
        //            player.canInteract = false;
        //            Interaction = "";
        //            gameObject.SetActive(false);
        //        }
        //        break;
        //}
    }

    public virtual void SwitchModel(State currentState)
    {
        switch (currentState)
        {
            case State.yoked:
                shellModel.SetActive(false);
                yokedModel.SetActive(true);
                break;
            case State.omelet:
                yokedModel.SetActive(false);
                omeletModel.SetActive(true);
                break;
            case State.scrambled:
                yokedModel.SetActive(false);
                scrambledModel.SetActive(true);
                break;

            case State.shell:
                shellModel.SetActive(true);
                yokedModel.SetActive(false);
                omeletModel.SetActive(false);
                break;
        }
        state = currentState;
    }

    // this is temporary for now
    public override void ChangeStatus()
    {
        state = State.omelet;
        cookingStatus = CookingStatus.cooked;
        Name = "Omelet";
        SwitchModel(state);
    }

    //Method used simply for changing the status of food to uncooked
    public void ChangeToUncooked()
    {
        state = State.shell;
        cookingStatus = CookingStatus.uncooked;
        Name = "Egg";
        SwitchModel(state);
    }
}
