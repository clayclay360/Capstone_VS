using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUtility
{
    public void ratInteraction(RatController rat);
}
public class Utilities : MonoBehaviour
{
    [Header("Info")]
    public string Name;
    public bool canInteract;
    public bool doesHaveRatInteraction;
}
