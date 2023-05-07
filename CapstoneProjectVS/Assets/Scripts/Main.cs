using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Main : MonoBehaviour
{
    [Header("Variables")]
    public bool isTutorialLevel;
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
    public Image FirstStar;
    public Image SecondStar;
    public Image ThirdStar;

    [HideInInspector]
    public Recipe mainRecipe, sideRecipeOne, sideRecipeTwo, sideRecipeThree;
    public Recipe[] sideRecipe;
    private GameObject mainOrder, sideOrderOne, sideOrderTwo, sideOrderThree;
    public GameObject[] sideOrder;
    public GameObject results;
    public GameObject showStars;
    public GameObject gameStar;
    public int Score { get; set; }

    private float timeInBetweenOrders;
    private int maxOrdersOfSides = 3;
    private int ordersCompleted;
    public float orderScore = 0;

    private bool startOrders;

    private void Start()
    {
        GameManager.tutorialLevel = isTutorialLevel;
    }

    public void StartGame()
    {
        sideRecipeOne = null;
        sideRecipeTwo = null;
        playersNeededUI.SetActive(false);
        GameManager.gameStarted = false;
        //showStars.SetActive(true);
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
                FindObjectOfType<Tutorial>().StartTutorial();
                GameManager.gameStarted = false;
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
            orderGameObject.GetComponent<Order>().AssignOrder(sideRecipe[i].Name, 120, i);

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

        orderGameObject.GetComponent<Order>().AssignOrder(sideRecipe[0].Name, 500, 0);
        sideOrder[0] = orderGameObject;
        FindObjectOfType<OrderManager>().DisplayIndicator(true);
    }

    public void OrderComplete(string orderName, Ingredients food = null)
    {
        //check to see which order this is
        if (orderName == mainRecipe.Name)
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
                    if (showStars != null)
                    {
                        showStars.GetComponent<Star>().DisplayStars(ordersCompleted);
                    }
                    Score += food.qualityRate;
                    
                }
                sideOrder[i]= null;
                ordersCompleted++;
                StartCoroutine(Complete());
                
            }
        }
       
    }

    IEnumerator Complete()
    {
        if (!GameManager.tutorialLevel)
        {
            if (ordersCompleted == 1)
            {
                Debug.Log("You got a star");
                showStars.SetActive(true);
                FirstStar.color = Color.yellow;
                gameStar.SetActive(true);
                yield return new WaitForSeconds(3);
                showStars.SetActive(false);
                gameStar.SetActive(false);
                orderScore += 1;
            }
            else if (ordersCompleted == 2)
            {
                Debug.Log("You got a star");
                showStars.SetActive(true);
                SecondStar.color = Color.yellow;
                gameStar.SetActive(true);
                yield return new WaitForSeconds(3);
                showStars.SetActive(false);
                gameStar.SetActive(false);
                orderScore += 1;
            }
            else if (ordersCompleted == 3)
            {
                Debug.Log("You got a star");
                orderScore += 1;
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
                showStars.SetActive(false);
                results.SetActive(true);
                results.GetComponent<Results>().DisplayResults(Score / maxOrdersOfSides);
                GameManager.gameStarted = false;
            }
        }
    }
}
