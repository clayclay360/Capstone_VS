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
    public GameObject platePrefab;
    public Transform[] spawnPosition;
    public static Dictionary<int, Plate> Order = new Dictionary<int, Plate>();
    public int orderNumber;

    private Recipe mainRecipe, sideRecipeOne, sideRecipeTwo;

    private float timeInBetweenOrders;
    private int maxOrdersOfSides;

    public void StartGame()
    {
        GameManager.gameStarted = true;
    }

    IEnumerator MainOrder()
    {
        while (GameManager.gameStarted)
        {
            mainRecipe = recipeManager[GameManager.currentLevel].mainRecipes[currentMainOrder];
            yield return null;
        }
    }

    IEnumerator SideOrders()
    {
        while (GameManager.gameStarted)
        {
            yield return new WaitForSeconds(2);

            if (GameManager.currentLevel == 0 && maxOrdersOfSides > currentNumberOfSides)
            {
                timeInBetweenOrders = Random.Range(minTimeInBetweenOrders, maxTimeInBetweenOrders);

                int orderIndex = Random.Range(0, recipeManager[GameManager.currentLevel].sideRecipes.Length);

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

        }
        else if (orderName == sideRecipeOne.Name)
        {

        }
        else if(orderName == sideRecipeTwo.Name)
        {

        }
    }
}
