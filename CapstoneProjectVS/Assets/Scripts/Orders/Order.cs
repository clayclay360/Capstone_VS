using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
public class Order : MonoBehaviour
{
    public bool Complete;
    public int Rating;

    public Text Name;
    public Slider TImer;

    private Main main;

    public void Start()
    {
        main = FindObjectOfType<Main>();
    }

    public void AssignOrder(string name, int time)
    {
        Name.text = name;
        TImer.maxValue = time;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        float maxTime = TImer.maxValue;
        float currentTime = Time.unscaledTime;
        float orderTime = 0;
        TImer.value = maxTime;

        while(maxTime - orderTime > 0)
        {
            orderTime = Time.unscaledTime - currentTime;
            yield return null;
            TImer.value = maxTime - orderTime;
        }

        main.OrderComplete(Name.text);
        Destroy(gameObject);
    }
}
