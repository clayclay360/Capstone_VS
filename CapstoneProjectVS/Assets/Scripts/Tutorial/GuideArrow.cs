using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideArrow : MonoBehaviour
{
    public Transform target;
    public GameObject mesh;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target); // look at target

        if(target != null && Vector3.Distance(transform.position, target.transform.position) < 3)
        {
            mesh.SetActive(false);
        }
        else
        {
            mesh.SetActive(true);
        }
    }
}
