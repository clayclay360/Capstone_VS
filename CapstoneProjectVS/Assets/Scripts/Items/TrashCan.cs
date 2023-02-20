using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Utilities, IUtility, IInteractable
{
    public Item trashItem;

    public void CanInteract(bool condition)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Item item, PlayerController player)
    {
        trashItem = item;
        player.inventory[0] = null;
        //Respawn item
    }

    public void ratInteraction(RatController rat)
    {
        rat.ratInventory = trashItem.gameObject;
        trashItem = null;
    }
}
