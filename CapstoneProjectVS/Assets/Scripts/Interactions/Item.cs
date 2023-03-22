using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICookable
{
    public void CookingCheck(GameObject cookingCheck, float cookTime, Ingredients food);
}
public interface ICollectable
{
    public void Collect(PlayerController player = null, RatController rat = null);
}

public interface IInteractable
{
    public void Interact(Item item, PlayerController player);
    public void CanInteract(bool condition);

    public void CheckHand(PlayerController.ItemInMainHand item, PlayerController player);
}

public class Item : MonoBehaviour, IInteractable
{
    [Header("Info")]
    public string Name;
    public bool canInteract;
    public bool isValidTarget;
    public bool isWashable;

    [Header("Icons")]
    public Sprite mainSprite;

    [Header("Outline")]
    public Outline outline;

    [HideInInspector]public string Interaction;

    public virtual void Update()
    {
        CanInteract(canInteract);
    }

    public virtual void Start()
    {
        StartHightlight();

    }

    /// <summary>
    /// This is the method that sets the interaction text for the player.
    /// This base method should work for the ingredients, since you only ever pick
    /// them up without any special interactions. Hopefully implementing this base
    /// method makes things a lot easier for us.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="player"></param>
    public virtual void CheckHand(PlayerController.ItemInMainHand item, PlayerController player) 
    {
        if (player.inventoryFull)
        {
            Interaction = "Inventory Full";
            return;
        }
        else if (!player.inventory[0] || !player.inventory[1])
        {
            Interaction = $"Grab {Name}";
            if (player.isInteracting)
            {
                player.isInteracting = false;
                player.canInteract = false;
                Interaction = "";
                gameObject.SetActive(false);
            }
        }
        else
        {
            Interaction = "";
        }


    }

    public virtual void Interact(Item item, PlayerController player){}
    public virtual void CanInteract(bool condition)
    {
        if(GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = condition;
        }
    }

    /// <summary>
    /// Adds a highlight component and gives it the appropriate color and width 
    /// for the beginning of the game
    /// </summary>
    private void StartHightlight()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        //Add a black outline to ingredients and tools
        if (TryGetComponent<Ingredients>(out _) || TryGetComponent<Tool>(out _))
        {
            ResetHighlight();
        }
    }

    /// <summary>
    /// Changes the highlight back to normal after the player leaves interact range
    /// </summary>
    public void ResetHighlight()
    {

        if (TryGetComponent<Ingredients>(out _) || TryGetComponent<Tool>(out _))
        {
            outline.OutlineColor = Color.black;
            outline.OutlineWidth = 2f;
        }
        else if (TryGetComponent<Utilities>(out _))
        {
            outline.enabled = false;
        }

    }
}
