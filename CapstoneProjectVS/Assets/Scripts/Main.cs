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
    public static Dictionary<int, Plate> Order = new Dictionary<int, Plate>();
    public int orderNumber;

    [Header("UI")]
    public GameObject orderWindowUI;

    [HideInInspector]
    public Recipe mainRecipe, sideRecipeOne, sideRecipeTwo;
    private GameObject mainOrder, sideOrderOne, sideOrderTwo;

    private float timeInBetweenOrders;
    private int maxOrdersOfSides = 2;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        GameManager.gameStarted = true;
        StartCoroutine(SideOrders());
        MainOrder();
    }

    void MainOrder()
    {
        mainRecipe = recipeManager[GameManager.currentLevel].mainRecipes[currentMainOrder];
        GameObject orderGameObject = Instantiate(orderPrefab, mainOrderWindow);
        orderGameObject.GetComponent<Order>().AssignOrder(mainRecipe.Name, 300);
        mainOrder = orderGameObject;
    }

    IEnumerator SideOrders()
    {
        while (GameManager.gameStarted)
        {
            //while the game started spawn a maximum of 2 side orders
            yield return new WaitForSeconds(2);

            if (maxOrdersOfSides > currentNumberOfSides)
            {
                timeInBetweenOrders = Random.Range(minTimeInBetweenOrders, maxTimeInBetweenOrders);
                int orderIndex = Random.Range(0, recipeManager[GameManager.currentLevel].sideRecipes.Length);

                if(sideRecipeOne == null)
                {
                    sideRecipeOne = recipeManager[GameManager.currentLevel].sideRecipes[orderIndex];
                    GameObject orderGameObject = Instantiate(orderPrefab, sideOrderWindow);
                    orderGameObject.GetComponent<Order>().AssignOrder(sideRecipeOne.Name, 120);
                    sideOrderOne = orderGameObject;
                }
                else
                {
                    sideRecipeTwo = recipeManager[GameManager.currentLevel].sideRecipes[orderIndex];
                    GameObject orderGameObject = Instantiate(orderPrefab, sideOrderWindow);
                    orderGameObject.GetComponent<Order>().AssignOrder(sideRecipeTwo.Name, 120);
                    sideOrderTwo = orderGameObject;
                }

                currentNumberOfSides++;
                yield return new WaitForSeconds(timeInBetweenOrders);
            }
        }
    }

    public void OrderComplete(string orderName)
    {
        //check to see which order this is
        if(orderName == mainRecipe.Name)
        {
            currentMainOrder++;
            Destroy(mainOrder);
            MainOrder();
        }
        else if (orderName == sideRecipeOne.Name)
        {
            currentNumberOfSides--;
            Destroy(sideOrderOne);
        }
        else if(orderName == sideRecipeTwo.Name)
        {
            currentNumberOfSides--;
            Destroy(sideOrderTwo);
        }
    }
}
