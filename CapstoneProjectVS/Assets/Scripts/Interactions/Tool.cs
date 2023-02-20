using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item, ICollectable
{
    [Header("Variables")]
    public bool isDirty;
    public bool isHot;
    public int useBeforeDirty;
    public int timesUsed;
    public int containerSize;
    [Header("Models")]
    public GameObject cleanModel;
    public GameObject dirtyModel;

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
