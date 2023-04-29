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
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 3f;
        outline.enabled = true;

        ingredient.isBeingUsed = false;
    }

    public override void Update()
    {
        //base.Update();
        SetCanBeUsed();
    }

    private void SetCanBeUsed()
    {
        canInteract = CanGetItem();
    }

    public bool CanGetItem()
    {
        return !ingredient.isBeingUsed;
    }

    public void SetOutlineColor()
    {
        outline.OutlineColor = CanGetItem() ? Color.white : Color.red;
    }

    public override void ResetHighlight()
    {
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
