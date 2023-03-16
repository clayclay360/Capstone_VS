using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTop : Utilities
{
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

                pan.transform.position = gameObject.transform.position; // position pan
                pan.gameObject.SetActive(true); // activate pan
                pan.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Spatula>() != null)
            {
                Spatula spatula = itemInMainHand.GetComponent<Spatula>();

                spatula.transform.position = gameObject.transform.position; // position pan
                spatula.gameObject.SetActive(true); // activate pan
                spatula.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Egg>() != null)
            {
                Egg egg = itemInMainHand.GetComponent<Egg>();

                egg.transform.position = gameObject.transform.position; // position pan
                egg.gameObject.SetActive(true); // activate pan
                egg.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Bacon>() != null)
            {
                Bacon bacon = itemInMainHand.GetComponent<Bacon>();

                bacon.transform.position = gameObject.transform.position; // position pan
                bacon.gameObject.SetActive(true); // activate pan
                bacon.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<HashBrown>() != null)
            {
                HashBrown hashBrown = itemInMainHand.GetComponent<HashBrown>();

                hashBrown.transform.position = gameObject.transform.position; // position pan
                hashBrown.gameObject.SetActive(true); // activate pan
                hashBrown.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Toast>() != null)
            {
                Toast toast = itemInMainHand.GetComponent<Toast>();

                toast.transform.position = gameObject.transform.position; // position pan
                toast.gameObject.SetActive(true); // activate pan
                toast.counterTop = gameObject;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }
        }
        player.isInteracting = false;
    }

    public void Update()
    {
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
}
