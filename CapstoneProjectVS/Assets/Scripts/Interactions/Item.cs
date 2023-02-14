using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    public void Collect(PlayerController player);
}

public interface IInteractable
{
    public void Interact(Item item, PlayerController player);
}

public class Item : MonoBehaviour, ICollectable
{
    [Header("Info")]
    public string Name;
    public bool canInteract;

    public virtual void Collect(PlayerController player)
    {
        //check to see which inventory is empty
        if (player.inventory[0] == null)
        {
            player.inventory[0] = this;
        }
        else
        {
            player.inventory[1] = this;
        }

        //temporary
        gameObject.SetActive(false);
        Debug.Log("Inventory 1: " + player.inventory[0] + " Inventory 2: " + player.inventory[1]);

    }
}
