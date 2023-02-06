using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawn : MonoBehaviour
{
    public GameObject rat;
    public GameObject ratSpawnLoc;
    GameObject closestPlayer;
    float distToPlayer;

    public float timeRemaining = 5;
    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnRat();
    }

    // Update is called once per frame
    void Update()
    {
        DistToPlayer();
        Timer();
    }

    public void SpawnRat()
    {
        Instantiate(rat, ratSpawnLoc.transform.position, ratSpawnLoc.transform.rotation);
    }

    public void DistToPlayer()
    {
        closestPlayer = GameObject.Find("PlayerControler");
        distToPlayer = Vector3.Distance(closestPlayer.transform.position, transform.position);

        if (distToPlayer <= 3.0f)
        {
            canSpawn = false;
        } else
        {
            canSpawn = true;
        }
    }

    public void Timer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        } else
        {
            if (canSpawn)
            {
                Debug.Log("Spawn Rat");
                SpawnRat();
            }
            timeRemaining = 5;
        }
    }
}
