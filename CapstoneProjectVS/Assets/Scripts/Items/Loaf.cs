using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loaf : Ingredients, IInteractable
{
    public enum State { slice, toast };
    [Header("State")]
    public State state;

    [Header("Models")]
    public GameObject sliceModel;
    public GameObject toastModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            case State.toast:
                sliceModel.SetActive(false);
                toastModel.SetActive(true);
                break;
        }
    }
}
