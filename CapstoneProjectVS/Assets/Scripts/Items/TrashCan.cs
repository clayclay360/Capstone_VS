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
        Debug.Log($"Trash can called with {item.name}");
        trashItem = item;
        //player.inventory[0] = null; Whoever added this line: I hate you.
        if (item.name == "Pan")
        {
            Pan pan;
            pan = item.GetComponent<Pan>();
            if (pan.itemsInPan.Count != 0)
            {
                pan.itemsInPan[0].GetComponent<Ingredients>().RespawnIngredient();
                pan.itemsInPan[0].GetComponent<Ingredients>().transform.parent = null;
                pan.itemsInPan[0].GetComponent<Ingredients>().isBeingUsed = false;

                //if/else checks for going into the listed scripts to change the cooking status to uncooked
                if (pan.itemsInPan[0].Name == "Omelet")
                {
                    Egg egg;
                    egg = pan.itemsInPan[0].GetComponent<Egg>();
                    egg.ChangeToUncooked();
                }
                else if (pan.itemsInPan[0].Name == "Bacon")
                {
                    Bacon bacon;
                    bacon = pan.itemsInPan[0].GetComponent<Bacon>();
                    bacon.ChangeToUncooked();
                }
                else if (pan.itemsInPan[0].Name == "Toast")
                {
                    Toast toast;
                    toast = pan.itemsInPan[0].GetComponent<Toast>();
                    toast.ChangeToUncooked();
                }
                pan.itemsInPan.Clear();
            }
            pan.RespawnTool();
            player.inventory[0] = null;
            trashItem = null;
        } else if (item.name == "Plate")
        {
            Debug.Log("Item name is plate");
            Plate plate;
            plate = item.GetComponent<Plate>();
            if (plate.foodOnPlate.Count != 0)
            {
                plate.foodOnPlate[0].GetComponent<Ingredients>().RespawnIngredient();
                plate.foodOnPlate[0].GetComponent<Ingredients>().transform.parent = null;
                plate.foodOnPlate[0].GetComponent<Ingredients>().isBeingUsed = false;
                //if/else checks for going into the listed scripts to change the cooking status to uncooked
                if (plate.foodOnPlate[0].Name == "Omelet")
                {
                    Egg egg;
                    egg = plate.foodOnPlate[0].GetComponent<Egg>();
                    egg.ChangeToUncooked();
                    egg.RespawnIngredient();
                    plate.foodOnPlate.Clear();
                    return;
                }
                else if (plate.foodOnPlate[0].Name == "Bacon")
                {
                    Bacon bacon;
                    bacon = plate.foodOnPlate[0].GetComponent<Bacon>();
                    bacon.ChangeToUncooked();
                    bacon.RespawnIngredient();
                    plate.foodOnPlate.Clear();
                    return;
                }
                else if (plate.foodOnPlate[0].Name == "Toast" || plate.foodOnPlate[0].Name == "Bread")
                {
                    Toast toast;
                    toast = plate.foodOnPlate[0].GetComponent<Toast>();
                    toast.ChangeToUncooked();
                    toast.RespawnIngredient();
                    plate.foodOnPlate.Clear();
                    return;
                }
                plate.foodOnPlate.Clear();
            }
            plate.RespawnTool();
            player.inventory[0] = null;
            trashItem = null;
        } 
        else if (item.name == "Spatula") //all scripts that derive from Tool must have their item.name added to this if statement for throwing things out
        {
            Tool tool;
            tool = item.GetComponent<Tool>();
            tool.RespawnTool();
            player.inventory[0] = null;
            trashItem = null;
        } 
        else if (item.name == "Egg" || item.name == "Bacon")
        {
            Debug.Log("Name checks out!");
            Ingredients ingredient;
            ingredient = item.GetComponent<Ingredients>();
            ingredient.RespawnIngredient();
            if (item.TryGetComponent<Egg>(out Egg egg))
            {
                egg.SwitchModel(egg.state);
            }
            player.inventory[0] = null;
            trashItem = null;
        }
        else if (item.name == "Toast" || item.name == "Bread")
        {
            Toast toast = item.GetComponent<Toast>();
            toast.RespawnIngredient();
            toast.state = Toast.State.slice;
            player.inventory[0] = null;
        }
        //Respawn item
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "Throwing Away");

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

    public void RatInteraction(RatController rat)
    {
        rat.ratInventory = trashItem.gameObject;
        trashItem = null;
    }
}
