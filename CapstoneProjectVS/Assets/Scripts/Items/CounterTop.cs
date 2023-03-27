using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class CounterTop : Utilities, IUtility
{
    public Transform itemPlacement;
    public bool isOccupied;
    public Item item1;

    // Start is called before the first frame update
    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        itemPlacement = gameObject.transform;
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            if (itemInMainHand.GetComponent<Pan>() != null)
            {
                Pan pan = itemInMainHand.GetComponent<Pan>();

                pan.transform.position = itemPlacement.position; // position pan
                pan.gameObject.SetActive(true); // activate pan
                pan.counterTop = gameObject;
                item1 = pan;
                player.inventory[0] = null; // item in main hand is null
                pan.canInteract = true;
                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Spatula>() != null)
            {
                Spatula spatula = itemInMainHand.GetComponent<Spatula>();

                spatula.transform.position = itemPlacement.position; // position pan
                spatula.gameObject.SetActive(true); // activate pan
                spatula.counterTop = gameObject;
                item1 = spatula;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Egg>() != null)
            {
                Egg egg = itemInMainHand.GetComponent<Egg>();

                egg.transform.position = itemPlacement.position; // position egg
                egg.gameObject.SetActive(true); // activate egg
                egg.counterTop = gameObject;
                item1 = egg;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Bacon>() != null)
            {
                Bacon bacon = itemInMainHand.GetComponent<Bacon>();

                bacon.transform.position = itemPlacement.position; // position pan
                bacon.gameObject.SetActive(true); // activate pan
                bacon.counterTop = gameObject;
                item1 = bacon;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<HashBrown>() != null)
            {
                HashBrown hashBrown = itemInMainHand.GetComponent<HashBrown>();

                hashBrown.transform.position = itemPlacement.position; // position pan
                hashBrown.gameObject.SetActive(true); // activate pan
                hashBrown.counterTop = gameObject;
                item1 = hashBrown;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if (itemInMainHand.GetComponent<Toast>() != null)
            {
                Toast toast = itemInMainHand.GetComponent<Toast>();

                toast.transform.position = itemPlacement.position; // position pan
                toast.gameObject.SetActive(true); // activate pan
                toast.counterTop = gameObject;
                item1 = toast;
                toast.canInteract = true;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if(itemInMainHand.GetComponent<Cheese>() != null)
            {
                Cheese cheese = itemInMainHand.GetComponent<Cheese>();

                cheese.transform.position = itemPlacement.position;
                cheese.gameObject.SetActive(true); // activate pan
                cheese.counterTop = gameObject;
                item1 = cheese;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if(itemInMainHand.GetComponent<Plate>() != null)
            {
                Plate plate = itemInMainHand.GetComponent<Plate>();

                plate.transform.position = itemPlacement.position;
                plate.gameObject.SetActive(true); // activate plate
                plate.counterTop = gameObject;
                item1 = plate;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }

            if(itemInMainHand.GetComponent<MixingBowl>() != null)
            {
                MixingBowl mixingBowl = itemInMainHand.GetComponent<MixingBowl>();

                mixingBowl.transform.position = itemPlacement.position;
                mixingBowl.gameObject.SetActive(true); // activate pan
                mixingBowl.counterTop = gameObject;
                item1 = mixingBowl;
                player.inventory[0] = null; // item in main hand is null

                isOccupied = true;
            }
        }
        player.isInteracting = false;
        isValidTarget = true;
        outline.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        canInteract = Interactivity();
        isValidTarget = !Interactivity();
        gameObject.GetComponent<BoxCollider>().enabled = true;
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

    public void RatInteraction(RatController rat)
    {
        if (isOccupied)
        {
            if(item1.TryGetComponent<Ingredients>(out Ingredients ingredient))
            {
                ingredient.Collect(null, rat);
                ingredient.counterTop = null;
                isOccupied = false;
            }
            else if (item1.TryGetComponent<Tool>(out Tool tool))
            {
                tool.Collect(null, rat);
                tool.counterTop = null;
                isOccupied = false;
            }
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "Placing Items");
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
