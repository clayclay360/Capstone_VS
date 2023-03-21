using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingBoard : Tool
{
    public Dictionary<int, Item> itemsOnCuttingBoard = new Dictionary<int, Item>();

    [SerializeField] private Transform knife;
    [SerializeField] private Slider slider;

    private int numberOfIngridents;
    private float currentProgressTime;
    private float finalProgressTime;
    private Coroutine cutCoroutine;
    private Ingredients ingredients1;
    private bool isChopping;
    public override void Start()
    {
        base.Start();
        //CuttingList();
    }

    public override void Interact(Item item, PlayerController player)
    {
        base.Interact(item, player);
        if (item = null)
        {
            if (item.GetComponent<Ingredients>() != null)
            {
                Ingredients ingredients = item.GetComponent<Ingredients>(); // get ingredient

                if (ingredients.isCuttable)
                {
                    itemsOnCuttingBoard.Add(itemsOnCuttingBoard.Count, item);
                    //StartCoroutine(CheckCutting());
                }
            }
            else
            {
                Collect(player);
            }
        }

        if (cutCoroutine == null)
        {
            finalProgressTime = ingredients1.ProgressTime;
            currentProgressTime = 0f;
            slider.value = 0f;
            slider.gameObject.SetActive(true);
            StartChopCoroutine();
            return;
        }

        if (isChopping == false)
        {
            StartChopCoroutine();
        }
    }

    private void StartChopCoroutine()
    {
        cutCoroutine = StartCoroutine(CheckCutting());
    }

    private void StopChopCoroutine()
    {
        isChopping = false;
        if (cutCoroutine != null)
        {
            StopCoroutine(cutCoroutine);
        }
    }

    private IEnumerator CheckCutting()
    {
        numberOfIngridents = 1;
        isChopping = true;
        while (currentProgressTime < finalProgressTime)
        {
            slider.value = currentProgressTime / finalProgressTime;
            currentProgressTime += Time.deltaTime;
            yield return null;
        }

        //Finished
        slider.gameObject.SetActive(false);
        isChopping = false;
        cutCoroutine = null;

    }

    //public void CuttingList()
    //{
    //    return;
    //}

}
