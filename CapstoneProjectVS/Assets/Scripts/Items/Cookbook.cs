using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookbook : Utilities, IInteractable
{
    [Header("Status")]
    public bool isDestroyed;
    public enum cookBookState { opened, closed, destroyed}
    public cookBookState currState;

    [Header("Objects")]
    [SerializeField] private GameObject cookbookUI;
    private CookbookUI UIScript;

    [Header("Models")]
    [SerializeField] private GameObject openModel;
    [SerializeField] private GameObject closedModel;
    [SerializeField] private GameObject destroyedModel;

    [Header("UX")]
    public GameObject indicator;

    private void Awake()
    {
        UIScript = cookbookUI.GetComponent<CookbookUI>();
    }

    public override void Interact(Item item, PlayerController player)
    {
        switch (currState)
        {
            case cookBookState.closed: //Switch book from open to closed
                currState = cookBookState.opened;
                closedModel.SetActive(false);
                openModel.SetActive(true);
                cookbookUI.SetActive(true);
                UIScript.isOpen = true;
                break;
            case cookBookState.opened: //Switch book from closed to opened
                currState = cookBookState.closed;
                closedModel.SetActive(true);
                openModel.SetActive(false);
                cookbookUI.SetActive(false);
                UIScript.isOpen = false;
                break;
            case cookBookState.destroyed:
                //Unreachable until rats can destroy the cookbook
                //We'll need to add the cookbook page(s) for repair then too
                break;
        }

        // if the player is on the tutorial level, once they flips to the bacon page, turn off indicator
        if (GameManager.tutorialLevel)
        {
            FindObjectOfType<Cookbook>().DisplayIndicator(false);
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "CookBook");

        switch (currState)
        {
            case cookBookState.closed: //Switch book from open to closed
                Interaction = "Open cookbook";
                break;
            case cookBookState.opened: //Switch book from closed to opened
                Interaction = "Close cookbook";
                break;
            case cookBookState.destroyed:
                //Unreachable until rats can destroy the cookbook
                //We'll need to add the cookbook page(s) for repair then too
                break;
        }
        if (player.isInteracting)
        {
            player.isInteracting = false;
            player.canInteract = false;
        }
    }

    /// <summary>
    /// Called from the player script when they leave the book's interaction range
    /// </summary>
    public void PlayerLeftBook()
    {
        currState = cookBookState.closed;
        cookbookUI.SetActive(false);
        openModel.SetActive(false);
        closedModel.SetActive(true);
        UIScript.isOpen = false;
    }

    /// <summary>
    /// Called from the player script when they press the next recipe button
    /// </summary>
    public void OnNextRecipe()
    {
        UIScript.NextRecipe();
    }

    /// <summary>
    /// Called from the player script when the press the previous recipe button
    /// </summary>
    public void OnPreviousRecipe()
    {
        UIScript.PreviousRecipe();
    }

    public void DisplayIndicator(bool condition)
    {
        indicator.SetActive(condition);
    }
}
