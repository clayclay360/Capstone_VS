using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawn : MonoBehaviour
{
    public GameObject rat;
    public GameObject ratSpawnLoc;
    public List<GameObject> targetList;
    public List<GameObject> playerList; //We'll add players to this list when they spawn
    public GameObject playerManager;
    private GameObject closestPlayer;
    private float distToPlayer;
    public int maxRats;
    public int ratCounter;

    private const float SPAWNTIME = 10f;
    public float spawnTimer = 0f;
    private bool canSpawn = true;

    //Rat variables
    private float[] scareTimes = { 2.0f, 1.5f, 1.0f, 0.0f };
    private float[] scareDistances = { 3.0f, 2.0f, 1.0f, 0.0f };
    public Material timidMat, normalMat, braveMat, recklessMat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ratCounter = GameManager.ratCount;
        if(playerList.Count == playerManager.GetComponent<PlayerManager>().playersNeeded && GameManager.ratCount < maxRats)
        {
            SpawnTimer();
        }
    }

    public void SpawnRat()
    {
        GameObject newRat = rat;
        RatController ratScript = newRat.GetComponent<RatController>();
        ratScript.spawnHole = this.gameObject;
        ratScript.playerList.AddRange(playerList);
        AssignBraveness(ratScript);
        Instantiate(newRat, ratSpawnLoc.transform.position, ratSpawnLoc.transform.rotation);
        GameManager.ratCount++;
    }

    public bool DistToPlayerCanSpawn()
    {
        //if (GameManager.gameStarted) 
        //{
            foreach(GameObject player in playerList)
            {
                if(closestPlayer == null || Vector3.Distance(player.transform.position, transform.position) < Vector3.Distance(closestPlayer.transform.position, transform.position))
                {
                    closestPlayer = player;
                }
            }
            distToPlayer = Vector3.Distance(closestPlayer.transform.position, transform.position);

            if (distToPlayer <= 3.0f)
            {
                return false;
            }
            else
            {
                return true;
            }
        //}
        //return false;
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
        int randNum = Random.Range(0, 4);
        rat.scareDistance = scareDistances[randNum];
        rat.scareTime = scareTimes[randNum];
        switch (randNum) //Assign the braveness. Not sure if we use this outside of here?
        {
            case 0:
                rat.ratBraveness = RatController.Braveness.Timid;
                rat.body.GetComponent<SkinnedMeshRenderer>().material = timidMat;
                break;
            case 1:
                rat.ratBraveness = RatController.Braveness.Normal;
                rat.body.GetComponent<SkinnedMeshRenderer>().material = normalMat;
                break;
            case 2:
                rat.ratBraveness = RatController.Braveness.Brave;
                rat.body.GetComponent<SkinnedMeshRenderer>().material = braveMat;
                break;
            case 3:
                rat.ratBraveness = RatController.Braveness.Reckless;
                rat.body.GetComponent<SkinnedMeshRenderer>().material = recklessMat;
                break;
        }
        
    }

}
