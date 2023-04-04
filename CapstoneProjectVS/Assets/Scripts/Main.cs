using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Main : MonoBehaviour
{
    [Header("Variables")]
    public RecipeManager[] recipeManager;
    public int currentMainOrder;
    
    public bool startingOrders;
    public float maxTimeInBetweenOrders, minTimeInBetweenOrders;
    public static int currentNumberOfSides;

    [Header("Orders")]
    public GameObject orderPrefab;
    public Transform sideOrderWindow, mainOrderWindow;
    public int orderNumber;

    [Header("UI")]
    public GameObject orderWindowUI;
    public GameObject playersNeededUI;

    [HideInInspector]
    public Recipe mainRecipe, sideRecipeOne, sideRecipeTwo, sideRecipeThree;
    public Recipe[] sideRecipe;
    private GameObject mainOrder, sideOrderOne, sideOrderTwo, sideOrderThree;
    public GameObject[] sideOrder;
    public GameObject results;
    public int Score { get; set; }

    private float timeInBetweenOrders;
    private int maxOrdersOfSides = 3;
    private int ordersCompleted;

    private bool startOrders;

    private void Start()
    {
        
    }

    public void StartGame()
    {
        sideRecipeOne = null;
        sideRecipeTwo = null;
        playersNeededUI.SetActive(false);
        StartCoroutine(SideOrders());
        //MainOrder();
    }

    public void Update()
    {
        StartCoroutine(CheckGameStatus());

        // start the orders
        if(GameManager.gameStarted && !startingOrders)
        {
            // if the tutorial level, start bacon order 
            if (GameManager.tutorialLevel)
            {
                TutorialOrder();
            }
            else // start normal game orders
            {
                StartGame();
                startingOrders = true;
            }
        }

        // 
        if (!GameManager.gameStarted)
        {
            playersNeededUI.GetComponentInChildren<Text>().text = " Players Needed: " + (2 - GameManager.numberOfPlayers);
        }
    }

    IEnumerator SideOrders()
    {
        //while the game started spawn a maximum of 2 side orders
        yield return new WaitForSeconds(2);

        for(int i = 0; i < maxOrdersOfSides; i++)
        {
            sideRecipe[i] = recipeManager[GameManager.currentLevel].sideRecipes[i];

            GameObject orderGameObject = Instantiate(orderPrefab, sideOrderWindow);
            orderGameObject.GetComponent<Order>().AssignOrder(sideRecipe[i].Name, 120);

            sideOrder[i] = orderGameObject;

            currentNumberOfSides++;
            FindObjectOfType<OrderManager>().DisplayIndicator(true);

            timeInBetweenOrders = Random.Range(minTimeInBetweenOrders, maxOrdersOfSides);
            yield return new WaitForSeconds(timeInBetweenOrders);
        }
    }

    public void TutorialOrder()
    {
        sideRecipe[0] = recipeManager[0].sideRecipes[1];
        GameObject orderGameObject = Instantiate(orderPrefab, sideOrderWindow);

        orderGameObject.GetComponent<Order>().AssignOrder(sideRecipe[0].Name, 120);
        sideOrder[0] = orderGameObject;
    }

    public void OrderComplete(string orderName, Ingredients food = null)
    {
        //check to see which order this is
        if(orderName == mainRecipe.Name)
        {
            currentMainOrder++;
            Destroy(mainOrder);
            //MainOrder();
        }
        for (int i = 0; i < sideRecipe.Length; i++)
        {
            if (sideRecipe[i].Name == orderName)
            {
                currentNumberOfSides--;
                Destroy(sideOrder[i],3);
                if(food != null)
                {
                    sideOrder[i].GetComponent<Order>().StarRating(food.qualityRate);
                    Score += food.qualityRate;
                }
                sideOrder[i]= null;
                ordersCompleted++;
            }
        }
    }

    public IEnumerator CheckGameStatus()
    {
        while (GameManager.gameStarted)
        {
            yield return null;
            if(ordersCompleted == 3)
            {
                results.SetActive(true);
                results.GetComponent<Results>().DisplayResults(Score / maxOrdersOfSides);
                GameManager.gameStarted = false;
            }
        }
    }
}
