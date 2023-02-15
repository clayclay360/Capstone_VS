using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item, IInteractable
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

    public virtual void Interact(Item itemInMainHand, PlayerController player)
    {
        
    }
}
