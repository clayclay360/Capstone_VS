using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : Interactable, ICollectable
{
    [Header("Variables")]
    public bool isDirty;
    public int useBeforeDirty;
    public int timesUsed;

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

    public void Collect()
    {
        throw new System.NotImplementedException();
    }
}
