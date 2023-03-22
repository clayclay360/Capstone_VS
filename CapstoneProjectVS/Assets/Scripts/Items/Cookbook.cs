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

    [Header("Models")]
    [SerializeField] private GameObject openModel;
    [SerializeField] private GameObject closedModel;
    [SerializeField] private GameObject destroyedModel;

    public override void Interact(Item item, PlayerController player)
    {
        switch (currState)
        {
            case cookBookState.closed: //Switch book from open to closed
                currState = cookBookState.opened;
                closedModel.SetActive(false);
                openModel.SetActive(true);
                cookbookUI.SetActive(true);
                break;
            case cookBookState.opened: //Switch book from closed to opened
                currState = cookBookState.closed;
                closedModel.SetActive(true);
                openModel.SetActive(false);
                cookbookUI.SetActive(false);
                break;
            case cookBookState.destroyed:
                //Unreachable until rats can destroy the cookbook
                //We'll need to add the cookbook page(s) for repair then too
                break;
        }
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
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

    public void PlayerLeftBook()
    {
        currState = cookBookState.closed;
        cookbookUI.SetActive(false);
        openModel.SetActive(false);
        closedModel.SetActive(true);
    }
}
