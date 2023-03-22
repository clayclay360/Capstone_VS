using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkScript : Utilities
{
    public bool isOccupied;
    public GameObject sinkGO;
    public GameObject sinkFilledGO;

    public enum State { empty, filled };
    public State state;

    public void Start()
    {
        state = State.empty;
        SwitchModel(state);
    }

    // Start is called before the first frame update
    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            if (itemInMainHand.GetComponent<Pan>() != null)
            {
                Pan pan = itemInMainHand.GetComponent<Pan>();

                pan.transform.position = gameObject.transform.position; // position pan
                pan.gameObject.SetActive(true); // activate pan
                pan.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
                state = State.filled;
                SwitchModel(state);
            }

            if (itemInMainHand.GetComponent<Spatula>() != null)
            {
                Spatula spatula = itemInMainHand.GetComponent<Spatula>();

                spatula.transform.position = gameObject.transform.position; // position pan
                spatula.gameObject.SetActive(true); // activate pan
                spatula.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
                state = State.filled;
                SwitchModel(state);
            }

            if (itemInMainHand.GetComponent<Plate>() != null)
            {
                Plate plate = itemInMainHand.GetComponent<Plate>();

                plate.transform.position = gameObject.transform.position;
                plate.gameObject.SetActive(true); // activate pan
                plate.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
                state = State.filled;
                SwitchModel(state);
            }

            if (itemInMainHand.GetComponent<CuttingBoard>() != null)
            {
                CuttingBoard cuttingBoard = itemInMainHand.GetComponent<CuttingBoard>();

                cuttingBoard.transform.position = gameObject.transform.position;
                cuttingBoard.gameObject.SetActive(true); // activate pan
                cuttingBoard.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
                state = State.filled;
                SwitchModel(state);
            }
        }
        player.isInteracting = false;
    }

    public override void Update()
    {
        base.Update();
        canInteract = Interactivity();
        gameObject.GetComponent<BoxCollider>().enabled = Interactivity();

        if (isOccupied)
        {

        }
    }

    public bool Interactivity()
    {
        if (isOccupied)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        if (!isOccupied)
        {
            if (player.inventory[0] && player.inventory[0].isWashable)
            {
                Interaction = $"Place {player.inventory[0].Name} in sink";
                return;
            }
            else if (player.inventory[1] && player.inventory[1].isWashable)
            {
                Interaction = $"Place {player.inventory[1].Name} in sink";
                return;
            }
        }
        Interaction = ""; //Only reach this if we fall through everything else i.e. no items in inventory or occupied
    }

    public virtual void SwitchModel(State currentState)
    {
        switch (currentState)
        {
            case State.empty:
                sinkFilledGO.SetActive(false);
                sinkGO.SetActive(true);
                break;
            case State.filled:
                sinkGO.SetActive(false);
                sinkFilledGO.SetActive(true);
                break;
        }
        state = currentState;
    }
}
