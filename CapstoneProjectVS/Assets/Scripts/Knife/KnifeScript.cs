using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public int damage;

    public int speed;
    public Vector3 forward;

    private Rigidbody rb;

    private bool targetHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.name != "PlayerControler")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RatController>() != null)
        {
            Debug.Log("Rat hit");
            RatController rat = other.gameObject.GetComponent<RatController>();
            rat.health -= damage;

            Destroy(gameObject);
        }
    }

    private void DestroyKnife()
    {
        Destroy(gameObject);
    }
}
