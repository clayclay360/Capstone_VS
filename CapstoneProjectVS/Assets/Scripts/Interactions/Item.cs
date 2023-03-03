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
}

public class Item : MonoBehaviour, IInteractable
{
    [Header("Info")]
    public string Name;
    public bool canInteract;
    public bool isValidTarget;

    [Header("Icons")]
    public Sprite main;

    [HideInInspector]public string Interaction;

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
