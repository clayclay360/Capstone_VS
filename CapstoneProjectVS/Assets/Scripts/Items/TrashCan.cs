using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Utilities, IUtility, IInteractable
{
    public Item trashItem;

    public override void Interact(Item item, PlayerController player)
    {
        trashItem = item;
        player.inventory[0] = null;
        if (item.name == "Pan" || item.name == "Spatula" || item.name == "Plate") //all scripts that derive from Tool must have their item.name added to this if statement for throwing things out
        {
            Tool tool;
            tool = item.GetComponent<Tool>();
            tool.RespawnTool();
            tool.gameObject.SetActive(true);
        } else if (item.name == "Egg" || item.name == "Toast")
        {
            Ingredients ingredient;
            ingredient = item.GetComponent<Ingredients>();
            ingredient.RespawnIngredient();
            ingredient.gameObject.SetActive(true);
            ingredient.isBeingUsed;
        }
        //Respawn item
    }

    public void ratInteraction(RatController rat)
    {
        rat.ratInventory = trashItem.gameObject;
        trashItem = null;
    }
}
