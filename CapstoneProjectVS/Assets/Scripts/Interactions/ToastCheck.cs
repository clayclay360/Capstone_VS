using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ToastCheck : MonoBehaviour
{
    [Header("Variables")]
    public float cookTime;
    public float progressMeter, progressMeterMin, progressMeterMax;

    [Header("UI")]
    public Slider progressSlider;

    public Ingredients food { get; set; }

    public void StartCooking()
    {
        progressMeter = progressMeterMin;
        progressSlider.maxValue = progressMeterMax;
        progressSlider.minValue = progressMeterMin;
        progressSlider.value = progressMeter;
        food.isCooking = true;
        StartCoroutine(Toasting(cookTime));
    }

    IEnumerator Toasting(float time)
    {
        float deltaTime = Time.unscaledTime;

        while (progressMeter < progressMeterMax)
        {
            progressMeter = (Time.unscaledTime - deltaTime) / time;
            progressSlider.value = progressMeter;
            yield return null;
        }
        progressSlider.gameObject.SetActive(false);
        food.ChangeStatus();
        food.isCooking = false;
    }
}