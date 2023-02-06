using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICollectable
{
    void Collect();
}

public class Interactable : MonoBehaviour
{
    public string Name;
    public bool canInteract;
}
