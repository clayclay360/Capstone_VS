using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    public void Collect(PlayerController player);
}

public interface IInteractable
{
    public void Interact(Item item, PlayerController player);
    public void CanInteract(bool condition);
}

public class Item : MonoBehaviour, IInteractable
{
    [Header("Info")]
    public string Name;
    public bool canInteract;

    public void Update()
    {
        CanInteract(canInteract);
    }
    public virtual void Interact(Item item, PlayerController player){}
    public virtual void CanInteract(bool condition)
    {
        if(GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = condition;
        }
    }
}
