using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : Item, ICollectable, ICookable
{

    public enum Status { clean, dirty}
    [Header("Status")]
    public Status status;

    [Header("Variables")]
    public bool isDirty;
    public bool isHot;
    public int useBeforeDirty;
    public int timesUsed;
    public int containerSize;
    [Header("Models")]
    public GameObject cleanModel;
    public GameObject dirtyModel;

    [Header("Sprites")]
    public Sprite clean;
    public Sprite dirty;

    public virtual void isDirtied()
    {
        timesUsed += 1;

        if(timesUsed >= useBeforeDirty)
        {
            isDirty = true;
        }
    }

    public virtual void SwitchModel(bool dirty)
    {
        if (isDirty)
        {
            cleanModel.SetActive(false);
            dirtyModel.SetActive(true);
        }
        else
        {
            dirtyModel.SetActive(false);
            cleanModel.SetActive(true);
        }
    }
    public virtual void Collect(PlayerController player = null, RatController rat = null)
    {
        //check if player is trying to collect this item
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
        //check if rat is trying to collect this item
        else if(rat != null)
        {
            rat.ratInventory = gameObject;

            //temporary
            gameObject.SetActive(false);
            Debug.Log(rat.name + " collected: " + gameObject.name);
        }
        isValidTarget = false;
    }

    public void CookingCheck(GameObject cookingCheck, float cookTime)
    {
        // reset everything
        cookingCheck.SetActive(true); // display cooking check
        CookingCheckScript cookingCheckScript = cookingCheck.GetComponent<CookingCheckScript>(); // get cooking script
        cookingCheckScript.StartCooking();
    }
}
