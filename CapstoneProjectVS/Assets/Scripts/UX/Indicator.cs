using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public float timer;
    public GameObject arrow;

    public void StartIndicator()
    {
        StartCoroutine(Indicate());
    }

    IEnumerator Indicate()
    {
        arrow.SetActive(true);
        yield return new WaitForSeconds(timer);
        arrow.SetActive(false);
    }
}
