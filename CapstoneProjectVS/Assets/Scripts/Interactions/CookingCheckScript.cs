using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class CookingCheckScript : MonoBehaviour
{
    [Header("Variables")]
    public float cookTime;
    public float progressMeter, progressMeterMin, progressMeterMax;
    public float[] interactionMeterStart, interactionMeterEnd;

    [Header("UI")]
    public Sprite checkMark;
    public Sprite xMark;
    public Sprite spatula;
    public Image[] completeMark;
    public Slider progressSlider;

    public Ingredients food { get; set; }

    private int complete;
    private int interactionIndex = 0;
    private bool[] interactionAttemptReady;
    private enum Attempt { None, Failed, Completed };
    private Attempt[] attempt;
    Pan pan;

    private void Start()
    {
        attempt = new Attempt[interactionMeterEnd.Length];
        attempt[0] = Attempt.None;
        attempt[1] = Attempt.None;
        pan = transform.parent.gameObject.GetComponent<Pan>();
    }

    public void ResetAttempts()
    {
        interactionAttemptReady = new bool[interactionMeterEnd.Length];

       for (int i = 0; i < interactionAttemptReady.Length; i++)
        {
            interactionAttemptReady[i] = true;
        }
    }

    public void StartCooking()
    {
        foreach (Image img in completeMark)
        {
            img.gameObject.SetActive(false);
        }
        complete = 0;
        progressMeter = progressMeterMin;
        progressSlider.maxValue = progressMeterMax;
        progressSlider.minValue = progressMeterMin;
        progressSlider.value = progressMeter;
        food.isCooking = true;
        ResetAttempts();
        StartCoroutine(Cooking(cookTime));
    }

    public void CheckAttempt()
    {
        for (int i = 0; i < interactionMeterEnd.Length; i++)
        {
            if (interactionMeterEnd[i] < progressMeter)
            {
                interactionIndex++;
            }
        }

        //Attempts
        //if (progressMeter > progressMeterMax / 4)
        //{
        //    switch (foodInPan.Name)
        //    {
        //        case "Egg":
        //            foodInPan.GetComponent<Egg>().state = Egg.State.omelet;
        //            break;
        //        case "Bacon":
        //            foodInPan.GetComponent<Bacon>().status = Bacon.Status.cooked;
        //            break;
        //    }
        //}
        //else
        //{
        //    switch (foodInPan.Name)
        //    {
        //        case "Burnt Bacon":
        //            foodInPan.GetComponent<Bacon>().status = Bacon.Status.burnt;
        //            break;
        //    }
        //}

        if (interactionIndex < attempt.Length)
        {
            if (interactionAttemptReady[interactionIndex])
            {
                if (progressMeter > interactionMeterStart[interactionIndex] && progressMeter < interactionMeterEnd[interactionIndex])
                {
                    completeMark[interactionIndex].sprite = checkMark;
                    completeMark[interactionIndex].gameObject.SetActive(true);
                    attempt[interactionIndex] = Attempt.Completed;
                    complete++;
                }
                else if (progressMeter < interactionMeterStart[interactionIndex])
                {
                    completeMark[interactionIndex].sprite = xMark;
                    completeMark[interactionIndex].gameObject.SetActive(true);
                    attempt[interactionIndex] = Attempt.Failed;
                }
                else if (progressMeter > interactionMeterEnd[interactionIndex])
                {
                    completeMark[interactionIndex].sprite = xMark;
                    completeMark[interactionIndex].gameObject.SetActive(true);
                    attempt[interactionIndex] = Attempt.Failed;
                }
                interactionAttemptReady[interactionIndex] = false;
            }
        }
    }

    IEnumerator Cooking(float time)
    {
        float deltaTime = Time.unscaledTime;

        while (progressMeter < progressMeterMax)
        {
            progressMeter = (Time.unscaledTime - deltaTime) / time;
            progressSlider.value = progressMeter;

            for (int i = 0; i < interactionMeterEnd.Length; i++)
            {
                //Could also add a or to check if the attempt is complet or uncompleted
                if (interactionMeterEnd[i] < progressMeter)
                {
                    completeMark[i].gameObject.SetActive(true);
                    switch (attempt[i])
                    {
                        case Attempt.None:
                            completeMark[i].sprite = xMark;
                            break;
                        case Attempt.Failed:
                            completeMark[i].sprite = xMark;
                            break;
                        case Attempt.Completed:
                            completeMark[i].sprite = checkMark;
                            break;
                        default:
                            Debug.Log(attempt);
                            break;
                    }
                }
            }
            yield return null;
        }
        progressSlider.transform.parent.gameObject.SetActive(false);
        food.isCooking = false;

        QualityOfFood();
        food.ChangeStatus();

        pan.timesUsed++;
        pan.IsDirtied();

        //Reset
        attempt[0] = Attempt.None;
        attempt[1] = Attempt.None;
        interactionIndex = 0;

        // Tutorial Level
        if (GameManager.tutorialLevel)
        {
            Tutorial tutorial = FindObjectOfType<Tutorial>();

            // if on step four then complete task
            if (tutorial.playerOneCurrentStep == 7)
            {
                tutorial.playerOneSteps[7].isComplete = true;
                tutorial.playerOneCurrentStep++;
            }
        }
    }

    public void QualityOfFood()
    {
        // perfect - didn't miss any cooking checks
        if(complete == 2)
        {
            food.qualityRate = 3;
        }
        else if(complete == 1) // ok - missed only one cooking check
        {
            food.qualityRate = 2;
            food.cookingStatus = Ingredients.CookingStatus.burnt;
        }
        else // trash - you missed all cooking checks
        {
            food.qualityRate = 1;
        }

        complete = 0;
    }
}