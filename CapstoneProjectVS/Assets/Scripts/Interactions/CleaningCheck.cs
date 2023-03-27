using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleaningCheck : MonoBehaviour
{
    public bool test;
    public float fillSpeed;
    private Slider slider;
    private float targetProgress = 0;

    public void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        Debug.Log(1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
        Debug.Log(targetProgress);
    }
    
    public void Reset()
    {
        targetProgress = 0;
        slider.value = 0;
    }
}