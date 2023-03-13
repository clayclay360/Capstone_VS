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
        }
    }

    // this is temporary for now
    public override void ChangeStatus()
    {
        state = State.omelet;
        SwitchModel(state);
    }
}
