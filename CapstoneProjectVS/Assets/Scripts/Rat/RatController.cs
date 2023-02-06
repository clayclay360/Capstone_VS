using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    //Movement
    public float speed;
    GameObject ratHoleSpawn;
    GameObject target;
    float distToPlayer;

    //Behavior
    enum Braveness {Timid = 3, Normal = 2, Brave = 1, Reckless = 0};
    Braveness ratBraveness;
    float scareDistance;
    float scareTimer;
    float scareTime; //couldnt remember what this variable is for, but its in the Capstone VSDD Rats document so it is here

    int health = 2;
    GameObject ratInventory;
    GameObject possibleTargets;
    Dictionary<GameObject, bool> currActiveTargets;
    GameObject closestPlayer;

    // Start is called before the first frame update
    void Start()
    {
        AssignBraveness();
    }

    // Update is called once per frame
    void Update()
    {
        DistToPlayer();
    }

    public void DistToPlayer()
    {
        closestPlayer = GameObject.Find("PlayerControler");
        distToPlayer = Vector3.Distance(closestPlayer.transform.position, transform.position);
    }

    public void AssignBraveness()
    {
        int randNum = Random.Range(0, 3);
        if (randNum == 3)
        {
            ratBraveness = Braveness.Timid;
            scareDistance = 5.0f;
            scareTimer = 3.0f;
        } else if (randNum == 2)
        {
            ratBraveness = Braveness.Normal;
            scareDistance = 3.0f;
            scareTimer = 2.0f;
        } else if (randNum == 1)
        {
            ratBraveness = Braveness.Brave;
            scareDistance = 1.5f;
            scareTimer = 1.0f;
        } else
        {
            ratBraveness = Braveness.Reckless;
            scareDistance = 0.0f;
            scareTimer = 0.0f;
        }
    }
}
