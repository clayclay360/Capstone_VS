using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawn : MonoBehaviour
{
    public GameObject rat;
    public GameObject ratSpawnLoc;
    public List<GameObject> targetList;
    public List<GameObject> playerList; //We'll add players to this list when they spawn
    private GameObject closestPlayer;
    private float distToPlayer;

    private const float SPAWNTIME = 7.5f;
    public float spawnTimer = 0f;
    private bool canSpawn = true;

    //Rat variables
    private float[] scareTimes = { 6.0f, 3.0f, 1.5f, 0.0f };
    private float[] scareDistances = { 7.5f, 5.0f, 3.0f, 0.0f };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer();
    }

    public void SpawnRat()
    {
        GameObject newRat = rat;
        RatController ratScript = newRat.GetComponent<RatController>();
        ratScript.spawnHole = this.gameObject;
        ratScript.playerList.AddRange(playerList);
        AssignBraveness(ratScript);
        Instantiate(newRat, ratSpawnLoc.transform.position, ratSpawnLoc.transform.rotation); 
    }

    public bool DistToPlayerCanSpawn()
    {
        if (GameManager.numberOfPlayers > 0) 
        {
            closestPlayer = GameObject.Find("PlayerControler");
            distToPlayer = Vector3.Distance(closestPlayer.transform.position, transform.position);

            if (distToPlayer <= 3.0f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public void SpawnTimer()
    {
        if (spawnTimer <= SPAWNTIME)
        {
            spawnTimer += Time.deltaTime;
        } 
        else
        {
            if (DistToPlayerCanSpawn() && canSpawn)
            {
                Debug.Log("Spawn Rat");
                SpawnRat();
            }
            spawnTimer = 0;
        }
    }

    public void AssignBraveness(RatController rat)
    {
        int randNum = Random.Range(3, 4);
        rat.scareDistance = scareDistances[randNum];
        rat.scareTime = scareTimes[randNum];
        switch (randNum) //Assign the braveness. Not sure if we use this outside of here?
        {
            case 0:
                rat.ratBraveness = RatController.Braveness.Timid;
                break;
            case 1:
                rat.ratBraveness = RatController.Braveness.Normal;
                break;
            case 2:
                rat.ratBraveness = RatController.Braveness.Brave;
                break;
            case 3:
                rat.ratBraveness = RatController.Braveness.Reckless;
                break;
        }
        
    }

}
