using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHolder : Item, IInteractable
{
    [Header("Ingredient References")]
    [SerializeField] private Ingredients ingredient;

    public override void Start()
    {
        base.Start();
        ingredient.SetIngredientSource(this);
    }

    public override void Update()
    {
        base.Update();
        SetCanBeUsed();
    }

    private void SetCanBeUsed()
    {
        canInteract = !CanGetItem();
    }

    public bool CanGetItem()
    {
        return !ingredient.isBeingUsed;
    }

    public void SetOutlineColor()
    {
        Debug.Log("Set outline color is called!");
        outline.OutlineColor = CanGetItem() ? Color.white : Color.red;
    }

    public override void ResetHighlight()
    {
        Debug.Log("ResetHighlight is called!");
        SetOutlineColor();
    }

    #region Interaction
    public override void Interact(Item item, PlayerController player)
    {
        if (!player.inventory[1] || !player.inventory[0])
        {
            ingredient.Collect(player);
            ingredient.isBeingUsed = true;
            SetOutlineColor();
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        if (!CanGetItem())
        {
            Interaction = $"There is already a {ingredient.name}!";
            return;
        }
        if (!player.inventory[0] || !player.inventory[1])
        {
            Interaction = $"Grab {ingredient.name}";
            if (player.isInteracting)
            {
                player.isInteracting = false;
                player.canInteract = false;
                Interaction = "";
            }
        }
        else
        {
            Interaction = "Inventory Full";
        }
    }

    #endregion

}
