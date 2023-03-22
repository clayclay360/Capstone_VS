using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class CounterTop : Utilities
{
    public Transform itemPlacement;
    public bool isOccupied;

    // Start is called before the first frame update
    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            if (itemInMainHand.GetComponent<Pan>() != null)
            {
                Pan pan = itemInMainHand.GetComponent<Pan>();

                pan.transform.position = itemPlacement.position; // position pan
                pan.gameObject.SetActive(true); // activate pan
                pan.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Spatula>() != null)
            {
                Spatula spatula = itemInMainHand.GetComponent<Spatula>();

                spatula.transform.position = itemPlacement.position; // position pan
                spatula.gameObject.SetActive(true); // activate pan
                spatula.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Egg>() != null)
            {
                Egg egg = itemInMainHand.GetComponent<Egg>();

                egg.transform.position = itemPlacement.position; // position pan
                egg.gameObject.SetActive(true); // activate pan
                egg.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Bacon>() != null)
            {
                Bacon bacon = itemInMainHand.GetComponent<Bacon>();

                bacon.transform.position = itemPlacement.position; // position pan
                bacon.gameObject.SetActive(true); // activate pan
                bacon.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<HashBrown>() != null)
            {
                HashBrown hashBrown = itemInMainHand.GetComponent<HashBrown>();

                hashBrown.transform.position = itemPlacement.position; // position pan
                hashBrown.gameObject.SetActive(true); // activate pan
                hashBrown.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Toast>() != null)
            {
                Toast toast = itemInMainHand.GetComponent<Toast>();

                toast.transform.position = itemPlacement.position; // position pan
                toast.gameObject.SetActive(true); // activate pan
                toast.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if(itemInMainHand.GetComponent<Cheese>() != null)
            {
                Cheese cheese = itemInMainHand.GetComponent<Cheese>();

                cheese.transform.position = itemPlacement.position;
                cheese.gameObject.SetActive(true); // activate pan
                cheese.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if(itemInMainHand.GetComponent<Plate>() != null)
            {
                Plate plate = itemInMainHand.GetComponent<Plate>();

                plate.transform.position = itemPlacement.position;
                plate.gameObject.SetActive(true); // activate pan
                plate.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if(itemInMainHand.GetComponent<MixingBowl>() != null)
            {
                MixingBowl mixingBowl = itemInMainHand.GetComponent<MixingBowl>();

                mixingBowl.transform.position = itemPlacement.position;
                mixingBowl.gameObject.SetActive(true); // activate pan
                mixingBowl.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if(itemInMainHand.GetComponent<CuttingBoard>() != null)
            {
                CuttingBoard cuttingBoard = itemInMainHand.GetComponent<CuttingBoard>();

                cuttingBoard.transform.position = itemPlacement.position;
                cuttingBoard.gameObject.SetActive(true); // activate pan
                cuttingBoard.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }
        }
        player.isInteracting = false;
    }

    public override void Update()
    {
        base.Update();
        canInteract = Interactivity();
        gameObject.GetComponent<BoxCollider>().enabled = Interactivity();
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
            if (player.inventory[0])
            {
                Interaction = $"Place {player.inventory[0].Name} on counter";
                return;
            }
            else if (player.inventory[1])
            {
                Interaction = $"Place {player.inventory[1].Name} on counter";
                return;
            }
        }
        Interaction = ""; //Only reach this if we fall through everything else i.e. no items in inventory or occupied
    }

}
