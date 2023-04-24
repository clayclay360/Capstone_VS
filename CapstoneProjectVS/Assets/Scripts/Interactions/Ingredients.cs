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

    [Header("Spawning")]
    private IngredientHolder source;
    public bool isBeingUsed;
    public Vector3 startLocation;
    public GameObject counterTop;
    public float ProgressTime;
 
    [Header("Mixing")]
    public bool isMixable;

    [Header("Chopping")]
    public bool isCuttable;

    [Header("UX")]
    public GameObject indicator;

    Dictionary<string, GameObject[]> needNume = new Dictionary<string, GameObject[]>(); // this variable needs a name
    public bool isCooking { get; set; }
    public int qualityRate { get; set; }

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

        //check if the player is trying to collect this item
        if (player != null)
        {
            //check to see which inventory is empty
            if (player.inventory[0] == null)
            {
                player.inventory[0] = this;
                Interaction = "";
                player.interactionText.text = "";
                player.isInteracting = false;
                player.ChangeCanInteract();
                Debug.Log("InEgg");
                gameObject.SetActive(false);
            }
            else if (player.inventory[1] == null)
            {
                player.inventory[1] = this;
                Interaction = "";
                player.interactionText.text = "";
                player.isInteracting = false;
                player.ChangeCanInteract();
                gameObject.SetActive(false);
            }

            
        }
        //check if a rat is trying to collect this item
        else if(rat != null)
        {
            rat.ratInventory = gameObject;
            CheckCounterTop();
            //temporary
            gameObject.SetActive(false);
            Debug.Log(rat.name + " collected: " + gameObject.name);
        }
        isValidTarget = false;
    }

    public override void Start()
    {
        startLocation = gameObject.transform.position;
        base.Start();
        canInteract = true;
        SetCollider();
    }

    public void RespawnIngredient()
    {
        Debug.Log($"{name} is respawning!");
        gameObject.transform.position = startLocation;
        gameObject.SetActive(true);
        isBeingUsed = false;
        canInteract = true;
        SetCollider();
        source.SetOutlineColor();
    }

    public virtual void ChangeStatus()
    {

    }

    public void CheckCounterTop()
    {
        if (counterTop != null)
        {
            counterTop.GetComponent<CounterTop>().isOccupied = false;
            counterTop = null;
        }
    }

    public void SetIngredientSource(IngredientHolder holder)
    {
        source = holder;
    }

    public void SetCollider()
    {
        if (canInteract)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
        Debug.Log("Collider has been set");
    }
}
