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
    //public static Dictionary<int, Plate> Order = new Dictionary<int, Plate>();
    public int orderNumber;

    [Header("UI")]
    public GameObject orderWindowUI;

    [HideInInspector]
    public Recipe mainRecipe, sideRecipeOne, sideRecipeTwo, sideRecipeThree;
    public Recipe[] sideRecipe;
    private GameObject mainOrder, sideOrderOne, sideOrderTwo, sideOrderThree;
    public GameObject[] sideOrder;

    private float timeInBetweenOrders;
    private int maxOrdersOfSides = 3;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        GameManager.gameStarted = true;
        sideRecipeOne = null;
        sideRecipeTwo = null;
        StartCoroutine(SideOrders());
        //MainOrder();
    }

    //void MainOrder()
    //{
    //    mainRecipe = recipeManager[GameManager.currentLevel].mainRecipes[currentMainOrder];
    //    GameObject orderGameObject = Instantiate(orderPrefab, mainOrderWindow);
    //    orderGameObject.GetComponent<Order>().AssignOrder(mainRecipe.Name, 300);
    //    mainOrder = orderGameObject;
    //}

    IEnumerator SideOrders()
    {
        //while (GameManager.gameStarted)
        //{
            //while the game started spawn a maximum of 2 side orders
            yield return new WaitForSeconds(2);

            for(int i = 0; i < maxOrdersOfSides; i++)
            {
                sideRecipe[i] = recipeManager[GameManager.currentLevel].sideRecipes[i];

                yield return new WaitForSeconds(3);
                GameObject orderGameObject = Instantiate(orderPrefab, sideOrderWindow);
            orderGameObject.GetComponent<Order>().AssignOrder(sideRecipe[i].Name, 120);

                sideOrder[i] = orderGameObject;
                //if(i == 0)
                //{
                //    sideOrderOne = orderGameObject;
                //}
                //else if(i == 1)
                //{
                //    sideOrderTwo = orderGameObject;
                //}
                //else
                //{
                //    sideOrderThree = orderGameObject;
                //}
                currentNumberOfSides++;
                FindObjectOfType<OrderManager>().DisplayIndicator(true);
            }

            // Old Don't Delete
            //if (maxOrdersOfSides > currentNumberOfSides)
            //{
            //    timeInBetweenOrders = Random.Range(minTimeInBetweenOrders, maxTimeInBetweenOrders);
            //    int orderIndex = Random.Range(0, recipeManager[GameManager.currentLevel].sideRecipes.Length);

            //    if(sideRecipeOne == null)
            //    {
            //        sideRecipeOne = recipeManager[GameManager.currentLevel].sideRecipes[orderIndex];
            //        GameObject orderGameObject = Instantiate(orderPrefab, sideOrderWindow);
            //        orderGameObject.GetComponent<Order>().AssignOrder(sideRecipeOne.Name, 120);
            //        sideOrderOne = orderGameObject;
            //    }
            //    else
            //    {
            //        sideRecipeTwo = recipeManager[GameManager.currentLevel].sideRecipes[orderIndex];
            //        GameObject orderGameObject = Instantiate(orderPrefab, sideOrderWindow);
            //        orderGameObject.GetComponent<Order>().AssignOrder(sideRecipeTwo.Name, 120);
            //        sideOrderTwo = orderGameObject;
            //    }

            //    currentNumberOfSides++;
            //    FindObjectOfType<OrderManager>().DisplayIndicator(true);
            //    yield return new WaitForSeconds(timeInBetweenOrders);
            //}
        //}
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
                }
                sideOrder[i]= null;
            }
        }
    }
}
