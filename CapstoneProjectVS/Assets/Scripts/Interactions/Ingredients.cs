using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : Item, ICollectable
{
    public enum CookingStatus { uncooked, cooked, spoiled, burnt };
    public CookingStatus cookingStatus;

    [Header("Icons")]
    public Sprite uncooked;
    public Sprite cooked;
    public Sprite spoiled;
    public Sprite burnt;

    Dictionary<string, GameObject[]> needNume = new Dictionary<string, GameObject[]>(); // this variable needs a name
    public bool isCooking { get; set; }

    public virtual void Collect(PlayerController player = null, RatController rat = null)
    {
        //check if the player is trying to collect this item
        if(player != null)
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
        }
        //check if a rat is trying to collect this item
        else if(rat != null)
        {
            rat.ratInventory = gameObject;

            //temporary
            gameObject.SetActive(false);
            Debug.Log(rat.name + " collected: " + gameObject.name);
        }
        isValidTarget = false;
    }

    public virtual void ChangeStatus()
    {

    }
}
