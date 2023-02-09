using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    public void Collect();
}

public interface IInteractable
{
    public void Interact(Item item);
}

public class Item : MonoBehaviour
{
    [Header("Info")]
    public string Name;
    public bool canInteract;
}
