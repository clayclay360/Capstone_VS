using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : Item, ICollectable
{
    public enum CookingStatus { uncooked, cooked, spoiled, burnt };
    public CookingStatus cookingStatus;

    Dictionary<string, GameObject[]> needNume = new Dictionary<string, GameObject[]>(); // this variable needs a name

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
