using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    enum CookingStatus { uncooked, cooked, spoiled, burnt };
    Dictionary<string, GameObject[]> needNume = new Dictionary<string, GameObject[]>(); // this variable needs a name
}
