using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RatController : MonoBehaviour
{
    //Movement
    public float speed;
    public NavMeshAgent navAgent;

    public List<GameObject> targetList;
    public GameObject currTarget;
    public GameObject spawnHole; //where the rat spawned from
    private float distToPlayer;
    public bool objectiveComplete = false;

    //Behavior
    public enum Braveness {Timid, Normal, Brave, Reckless};
    public Braveness ratBraveness;
    public float scareDistance;
    public float scareTime; //The actual time for how long the rat is scared
    private float scareTimer; //The incrementing timer for the rats
    public bool isScared;
    

    public int health = 2;
    public GameObject ratInventory;
    //Dictionary<GameObject, bool> currActiveTargets;
    public GameObject closestPlayer;

    // Start is called before the first frame update
    void Start()
    {
        AssignTarget();
    }

    // Update is called once per frame
    void Update()
    {
        //DistToPlayer();
        CheckTarget();
        Movement();
    }

    public void DistToPlayer()
    {
        closestPlayer = GameObject.Find("PlayerControler");
        distToPlayer = Vector3.Distance(closestPlayer.transform.position, transform.position);
    }

    private void AssignTarget()
    {
        //Get target list from spawnhole
        targetList.Clear();
        targetList.AddRange(spawnHole.GetComponent<RatSpawn>().targetList);
        //Check that our list is valid
        if (targetList.Count == 0)
        {
            Debug.LogError("Rat target list is empty!");
            return;
        }
        //Remove any items from the list that aren't valid targets
        List<GameObject> removeList = new List<GameObject>();
        foreach(GameObject target in targetList)
        {
            if(target.GetComponent<Item>() != null && !target.GetComponent<Item>().isValidTarget)
            {
                removeList.Add(target);
            }
            else if(target.GetComponent<Utilities>() != null && !target.GetComponent<Utilities>().isValidTarget)
            {
                removeList.Add(target);
            }
        }
        foreach(GameObject item in removeList)
        {
            if (targetList.Contains(item))
            {
                targetList.Remove(item);
            }
        }
        //Assign a target from the target list
        int randTargetNum = Random.Range(0, targetList.Count);
        currTarget = targetList[randTargetNum];
        navAgent.SetDestination(currTarget.transform.position);
        Debug.Log(currTarget);

    }

    private void CheckTarget()
    {
        if (!objectiveComplete)
        {
            //if currTarget is not valid, assign new target
            if (!currTarget.activeInHierarchy || (!currTarget.GetComponent<Item>().isValidTarget && !currTarget.GetComponent<Utilities>().isValidTarget))
            {
                AssignTarget();
            }
        }
    }

    /// <summary>
    /// Logic for rat movement. If the rat is scared, it runs from its source of fear.
    /// If the rat has not completed its objective, it moves towards it.
    /// If the rat has completed its objective, it returns to its spawn hole and despawns.
    /// </summary>
    private void Movement()
    {
        if (isScared)
        {
            navAgent.enabled = false; //We will make the rats move away from the closest player/source of fear
            scareTimer += Time.deltaTime;
            if (scareTimer >= scareTime)
            {
                isScared = false;
                scareTimer = 0f;
            }
        }
        else if (!objectiveComplete)
        {
            if (Vector2.Distance(currTarget.transform.position, transform.position) <= 0.625f)
            {
                navAgent.isStopped = true;
                AttackTarget();
            }

        }
        //The rat despawns when close to the vent and has completed its objective
        else if (Vector3.Distance(spawnHole.transform.position, transform.position) <= 1.0f)//DESPAWN
        {
            //check if the rat is holding an item
            if(ratInventory != null)
            {
                //temporary
                ratInventory.transform.position = transform.position;
                ratInventory.SetActive(true);
                if (ratInventory.GetComponent<Ingredients>() != null)
                {
                    ratInventory.GetComponent<Ingredients>().isValidTarget = true;
                }
                else if (ratInventory.GetComponent<Tool>() != null)
                {
                    ratInventory.GetComponent<Tool>().isValidTarget = true;
                }
                ratInventory = null;
            }
            Debug.Log("Destroying " + this);
            Destroy(gameObject);
        }
    }

    private void AttackTarget()
    {
        //check if target is a utility
        if(currTarget.GetComponent<IUtility>() != null)
        {
            Utilities interactionCheck = currTarget.GetComponent<Utilities>();
            IUtility utility = currTarget.GetComponent<IUtility>();
            //check if the rat can interact with this utility
            if (interactionCheck.doesHaveRatInteraction)
            {
                utility.ratInteraction(this);
                objectiveComplete = true;
                currTarget = spawnHole;
                navAgent.SetDestination(currTarget.transform.position);
                navAgent.isStopped = false;
            }
        }
        //check if target is a tool or ingredient
        else if(currTarget.GetComponent<ICollectable>() != null)
        {
            ICollectable collectableItem = currTarget.GetComponent<ICollectable>();
            collectableItem.Collect(null, this);
            objectiveComplete = true;
            //set item to spoiled if ingredient
            if(currTarget.GetComponent<Ingredients>() != null)
            {
                currTarget.GetComponent<Ingredients>().cookingStatus = Ingredients.CookingStatus.spoiled;
            }
            //set item to dirty if tool
            else if (currTarget.GetComponent<Tool>() != null)
            {
                currTarget.GetComponent<Tool>().status = Tool.Status.dirty;
            }
            currTarget = spawnHole;
            navAgent.SetDestination(currTarget.transform.position);
            navAgent.isStopped = false;
        }
    }
}
