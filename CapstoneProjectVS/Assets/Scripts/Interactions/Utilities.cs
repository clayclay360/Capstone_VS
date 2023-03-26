using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUtility
{
    public void ratInteraction(RatController rat);
}
public class Utilities : Item
{ 
    [Header("Rat Interaction")]
    public bool doesHaveRatInteraction;
}
