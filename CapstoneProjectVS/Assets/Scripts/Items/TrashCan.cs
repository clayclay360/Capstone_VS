using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Utilities, IUtility, IInteractable
{
    public Item trashItem;

    private void Update()
    {
        if(trashItem != null)
        {
            isValidTarget = true;
        }
        else
        {
            isValidTarget = false;
        }
    }

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
            ingredient.isBeingUsed = false;
        }
        //Respawn item
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        if (player.inventory[0])
        {
            Interaction = CheckForContainerAndSetText(player.inventory[0]);
        }
        else if (player.inventory[1])
        {
            Interaction = CheckForContainerAndSetText(player.inventory[1]);
        }
        else
        {
            Interaction = "";
        }
    }

    private string CheckForContainerAndSetText(Item item)
    {
        #region containers
        if (item.TryGetComponent<Pan>(out Pan pan))
        {
            if (pan.itemsInPan.Count > 0) //This is how we check for items in the pan without breaking anything I guess
            {
                return $"Throw out {pan.itemsInPan[0].Name}";
            }
            else
            {
                return "Throw out pan";
            }
        }
        else if (item.TryGetComponent<MixingBowl>(out MixingBowl bowl))
        {
            if (bowl.itemsInMixingBowl.Count > 0)
            {
                return "Throw out items in mixing bowl";
            }
            else
            {
                return "Throw out mixing bowl";
            }
        }
        else if (item.TryGetComponent<Plate>(out Plate plate)) 
        {
            if (plate.foodOnPlate.Count > 0)
            {
                return "Throw out food";
            }
            else
            {
                return "Throw out plate";
            }
        }
        #endregion
        else
        {
            return $"Throw out {item.Name}";
        }
    }

    public void ratInteraction(RatController rat)
    {
        rat.ratInventory = trashItem.gameObject;
        trashItem = null;
    }
}
