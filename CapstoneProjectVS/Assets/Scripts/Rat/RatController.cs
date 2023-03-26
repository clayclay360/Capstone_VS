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
    public float scareDistance; //How close an object has to be to the rat to scare it
    public float scareIgnoreDistance;  //How close the rat has to be to its destination to ignore something that's scaring it
    public float scareTime; //The actual time for how long the rat is scared
    private float scareTimer; //The incrementing timer for the rats
    public bool isScared;
    public GameObject scareObject; //The object that is scaring the rat
    

    public int health = 2;
    public GameObject ratInventory, blood;
    //Dictionary<GameObject, bool> currActiveTargets;
    public List<GameObject> playerList;
    public GameObject closestPlayer;

    //Animations
    public Animator ratAnimator;
    public bool isGrabbing;


    //Outline
    [SerializeField] private Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        AssignTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 1 && !blood.activeInHierarchy)
        {
            blood.SetActive(true);
        }
        if(health <= 0)
        {
            Despawn();
        }
        CheckPlayers();
        DistToPlayer();
        CheckTarget();
        Movement();
        if (isActiveAndEnabled)
        {
            MovementAnimation();
        }
        if(ratInventory != null)
        {
            MakeInvDirty();
        }
    }

    public void CheckPlayers()
    {
        RatSpawn spawnScript = spawnHole.GetComponent<RatSpawn>();
        if(playerList != spawnScript.playerList)
        {
            playerList.Clear();
            playerList.AddRange(spawnScript.playerList);
        }
    }

    public void DistToPlayer()
    {
        if(playerList.Count != 0)
        {
            distToPlayer = 100;
            foreach(GameObject player in playerList)
            {
                if(Vector3.Distance(player.transform.position, transform.position) < distToPlayer)
                {
                    closestPlayer = player;
                    distToPlayer = Vector3.Distance(player.transform.position, transform.position);
                }
            }
            if(distToPlayer <= scareDistance)
            {
                isScared = true;
                scareObject = closestPlayer;
            }
        }
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
        NavMesh.SamplePosition(currTarget.transform.position, out NavMeshHit hit, 1f, NavMesh.AllAreas);
        navAgent.SetDestination(hit.position);
        //Debug.Log(currTarget);

    }

    private void CheckTarget()
    {
        if (!objectiveComplete)
        {
            //if currTarget is not valid, assign new target
            if (currTarget && (!currTarget.activeInHierarchy || (!currTarget.GetComponent<Item>().isValidTarget && !currTarget.GetComponent<Utilities>().isValidTarget)))
            {
                AssignTarget();
            }
            //if target is valid and rat is not scared, make sure rat is moving towards target
            else if (!isScared && currTarget)
            {
                NavMesh.SamplePosition(currTarget.transform.position, out NavMeshHit hit, 1f, NavMesh.AllAreas);
                navAgent.SetDestination(hit.position);
            }
        }
    }

    public void SetBraveness(Braveness braveness)
    {
        switch (braveness)
        {
            case (Braveness.Timid):
                scareDistance = 3;
                scareTime = 15;
                break;
            case (Braveness.Normal):
                scareDistance = 2;
                scareTime = 10;
                break;
            case (Braveness.Brave):
                scareDistance = 1;
                scareTime = 5;
                break;
            case (Braveness.Reckless):
                scareDistance = 0;
                scareTime = 0;
                break;
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
            NavMesh.SamplePosition(currTarget.transform.position, out NavMeshHit hit, 1f, NavMesh.AllAreas);
            //temporary
            if (navAgent.destination == hit.position || Vector3.Distance(navAgent.destination, transform.position) < 0.1)  //if the rat hasn't already abandoned its target or has reached its destination
            {
                Vector3 randomPoint = Random.insideUnitSphere * 10; //Get a random point within a 10 unit sphere of the rat
                Vector3 newDestination = transform.position + randomPoint; //Add the random point to the rat's curret position
                NavMesh.SamplePosition(newDestination, out NavMeshHit hit1, 10, NavMesh.AllAreas); //Find the closest point on the navmesh to the new destination
                newDestination = hit1.position; //set the rat's new destination to that point
                navAgent.SetDestination(newDestination); //Make the rat go to the new destination
            }
            scareTimer += Time.deltaTime;
            if (scareTimer >= scareTime)
            {
                isScared = false;
                scareTimer = 0f;
            }
        }
        else if (!objectiveComplete)
        {
            //Debug.Log(Vector2.Distance(currTarget.transform.position, transform.position)); 
            if (Vector2.Distance(navAgent.destination, transform.position) <= 0.3f)
            {
                navAgent.isStopped = true;
                AttackTarget();
            }

        }
        //The rat despawns when close to the vent and has completed its objective
        else if (Vector3.Distance(spawnHole.transform.position, transform.position) <= 1.0f)//DESPAWN
        {
            Despawn();
        }
    }

    private void MovementAnimation()
    {
        isGrabbing = ratAnimator.GetBool("onGrab");
        if (navAgent.isStopped)
        {
            ratAnimator.SetBool("isRunning", false);
        }
        else
        {
            ratAnimator.SetBool("isRunning", true);
        }
    }

    private void AttackTarget()
    {
        ratAnimator.SetTrigger("onGrab");
        //check if target is a utility
        if(currTarget && currTarget.GetComponent<IUtility>() != null)
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
                if (!isGrabbing)
                {
                    navAgent.isStopped = false;
                }
            }
        }
        //check if target is a tool or ingredient
        else if(currTarget && currTarget.GetComponent<ICollectable>() != null)
        {
            ICollectable collectableItem = currTarget.GetComponent<ICollectable>();
            collectableItem.Collect(null, this);
            objectiveComplete = true;
            currTarget = spawnHole;
            navAgent.SetDestination(currTarget.transform.position);
            if (!isGrabbing)
            {
                navAgent.isStopped = false;
            }
        }
        UpdateOutline();
    }

    public void MakeInvDirty()
    {
        //set item to spoiled if ingredient and not already spoiled
        if (ratInventory.GetComponent<Ingredients>() != null)
        {
            if(ratInventory.GetComponent<Ingredients>().cookingStatus != Ingredients.CookingStatus.spoiled)
            {
                ratInventory.GetComponent<Ingredients>().cookingStatus = Ingredients.CookingStatus.spoiled;
            }
        }
        //set item to dirty if tool and not already dirty
        else if (ratInventory.GetComponent<Tool>() != null)
        {
            if (ratInventory.GetComponent<Tool>().status != Tool.Status.dirty)
            {
                ratInventory.GetComponent<Tool>().status = Tool.Status.dirty;
            }
        }
    }

    private void Despawn()
    {
        //check if the rat is holding an item
        if (ratInventory != null)
        {
            //temporary
            ratInventory.transform.position = transform.position;
            ratInventory.SetActive(true);
            ratInventory.transform.SetParent(null);
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
        Destroy(gameObject);
    }

    private void UpdateOutline()
    {
        if (ratInventory != null)
        {
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 3f;
            outline.OutlineMode = Outline.Mode.OutlineAll;
        }
        else
        {
            outline.OutlineColor = Color.black;
            outline.OutlineWidth = 2f;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
    }

}
