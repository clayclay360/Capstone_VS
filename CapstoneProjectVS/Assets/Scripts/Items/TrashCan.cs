using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Utilities, IUtility, IInteractable
{
    public Item trashItem;
    public void Interact(Item item)
    {
        trashItem = item;
        //Remove item from player hand. Can we pass the playerController?
    }

    public void Interact(Item item, PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public void ratInteraction(RatController rat)
    {
        rat.ratInventory = trashItem.gameObject;
        trashItem = null;
    }
}
