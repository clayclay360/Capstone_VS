using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Utilities, IUtility, IInteractable
{
    private Tool dishBeingCleaned;
    public Tool[] sinkInv = { null, null, null };
    //private bool[] isInvSlotEmpty = { true, true, true };
    public override void Interact(Item item, PlayerController player)
    {
        //Pick up item
        if (item == null)
        {
            if (!IsInvEmpty())
            {
                //We need to get the playerController to add to their inv
                ClearFirstItemFromInv();
            }
            else
            {
                Debug.LogWarning("All sink slots are empty");
            }
        }

        //Place item
        Tool tool = item.GetComponent<Tool>();
        if (!tool.isDirty) //Probably can change the check once the player interaction is set
        {
            return;
        }

        if (!IsInvFull())
        {
            sinkInv[GetFirstEmptyFromInv()] = tool;
        }
        else
        {
            Debug.LogWarning("No empty sink slots?");
        }
    }

    public void RatInteraction(RatController rat)
    {
        if (!IsInvEmpty())
        {
            //Rats will try stealing clean items from the sink first
            rat.ratInventory = GetFirstItemFromInv(true).gameObject;
            if(rat.ratInventory == null)
            {
                //If there are no clean items, then rats will steal dirty ones
                rat.ratInventory = GetFirstItemFromInv().gameObject;
            }
            ClearFirstItemFromInv();
        }

    }

    /// <summary>
    /// Returns the index of the first empty inventory slot
    /// We get the index so that we can assign to it
    /// </summary>
    /// <returns></returns>
    private int GetFirstEmptyFromInv()
    {
        for (int i = 0; i < sinkInv.Length; i++)
        {
            if (!sinkInv[i])
            {
                return i;
            }
        }
        return 10; //Not sure what to do for out of bounds
    }

    /// <summary>
    /// Helper function retrieves the first item from the inventory that is not null
    /// Optional bool mustBeClean will only return the first clean item and skip the dirty ones
    /// </summary>
    /// <returns></returns>
    private Item GetFirstItemFromInv(bool mustBeClean = false)
    {
        for (int i = 0; i < sinkInv.Length; i++)
        {
            if (sinkInv[i])
            {
                if (mustBeClean && sinkInv[i].isDirty)
                {
                    continue;
                }
                return sinkInv[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Helper function returns the bool true if all sink inv slots are not null
    /// </summary>
    /// <returns></returns>
    private bool IsInvFull()
    {
        for (int i = 0; i < sinkInv.Length; i++)
        {
            if (!sinkInv[i])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Helper function returns the bool true if all sink inv slots are null
    /// </summary>
    /// <returns></returns>
    private bool IsInvEmpty()
    {
        for (int i = 0; i < sinkInv.Length; i++)
        {
            if (sinkInv[i])
            {
                isValidTarget = true;
                return false;
            }
        }
        isValidTarget = false;
        return true;
    }

    /// <summary>
    /// Removes the first item that is not null from the sink inventory by 
    /// making it null. Be sure to only call this AFTER you have assigned the
    /// item to somewhere else, like the rat or player inventory
    /// </summary>
    private void ClearFirstItemFromInv()
    {
        for (int i = 0; i < sinkInv.Length; i++)
        {
            if (sinkInv[i])
            {
                sinkInv[i] = null;
            }
        }
    }

    public override void CanInteract(bool condition)
    {
        throw new System.NotImplementedException();
    }
}
