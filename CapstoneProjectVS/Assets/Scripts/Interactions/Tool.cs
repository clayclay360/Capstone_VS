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
    public Vector3 startLocation;
    public GameObject counterTop;
    public GameObject sink;

    [Header("Models")]
    public GameObject cleanModel;
    public GameObject dirtyModel;

    [Header("Sprites")]
    public Sprite clean;
    public Sprite dirty;

    [Header("UX")]
    public GameObject indicator;

    public virtual void IsDirtied()
    {
        timesUsed += 1;

        if(timesUsed >= useBeforeDirty)
        {
            status = Status.dirty;
            isDirty = true;
            Interaction = $"{Name} is dirty";
        }
    }

    public virtual void IsClean()
    {
        status = Status.clean;
        timesUsed = 0;
        isDirty = false;
    }

    public override void Start()
    {
        startLocation = gameObject.transform.position;
        base.Start();
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

    public void DisplayIndicator(bool condition)
    {
        indicator.SetActive(condition);
    }

    public virtual void Collect(PlayerController player = null, RatController rat = null)
    {
        // turn off indicator display
        if (indicator != null)
        {
            DisplayIndicator(false);
        }

        //check if player is trying to collect this item
        if(player != null)
        {
            //check to see which inventory is empty
            if (player.inventory[0] == null)
            {
                player.inventory[0] = this;
                Interaction = "";
                player.interactionText.text = "";
                player.isInteracting = false;
                player.ChangeCanInteract();
                CheckCounterTop();
                CheckSink();
                gameObject.SetActive(false);
            }
            else if (player.inventory[1] == null)
            {
                player.inventory[1] = this;
                Interaction = "";
                player.interactionText.text = "";
                player.isInteracting = false;
                player.ChangeCanInteract();
                CheckCounterTop();
                CheckSink();
                gameObject.SetActive(false);
            }

            //temporary
            gameObject.SetActive(false);
        }
        //check if rat is trying to collect this item
        else if(rat != null)
        {
            rat.ratInventory = gameObject;
            CheckCounterTop();
            CheckSink();
            //temporary
            gameObject.SetActive(false);
            Debug.Log(rat.name + " collected: " + gameObject.name);
        }
        isValidTarget = false;
    }

    public void CookingCheck(GameObject cookingCheck, float cookTime, Ingredients food)
    {
        // reset everything
        cookingCheck.SetActive(true); // display cooking check
        CookingCheckScript cookingCheckScript = cookingCheck.GetComponent<CookingCheckScript>(); // get cooking script
        cookingCheckScript.food = food;
        cookingCheckScript.StartCooking();
    }

    public void RespawnTool()
    {
        gameObject.transform.position = startLocation;
        gameObject.SetActive(true);
    }

    public void CheckCounterTop()
    {
        if (counterTop != null)
        {
            counterTop.GetComponent<CounterTop>().isOccupied = false;
            counterTop = null;
        }
    }

    public void CheckSink()
    {
        if (sink != null)
        {
            sink.GetComponent<SinkScript>().isOccupied = false;
            sink = null;
        }
    }
}
