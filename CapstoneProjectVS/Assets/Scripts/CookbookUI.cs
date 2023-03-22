using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookbookUI : MonoBehaviour
{
    [Header("Variables")]
    public bool isOpen;

    [Header("Elements")]
    [SerializeField] private Text recipeTitle;
    [SerializeField] private Text leftPageText;
    [SerializeField] private Text rightPageText;
    //These are used when the player flips the pages
    [SerializeField] private string pageOneLeftText;
    [SerializeField] private string pageOneRightText;
    [SerializeField] private string pageTwoLeftText;
    [SerializeField] private string pageTwoRightText;

    [Header("Pages")]
    private int currPageNum;
    private int currRecipeNum;

    private Dictionary<int, string> recipeList;
    /// <summary>
    /// Dictionary says how many pages each recipe takes.
    /// The first int is the recipe number.
    /// The second int is the number  of pages that recipe has.
    /// </summary>
    private Dictionary<int, int> pagesPerRecipe;

    // Start is called before the first frame update
    void Start()
    {
        currPageNum = 1;
        currRecipeNum = 1;
        SetRecipeDictionaries();
    }

    /// <summary>
    /// Adds all of the recipe references to the two dictionaries
    /// </summary>
    private void SetRecipeDictionaries()
    {
        recipeList.Add(1, "Bacon");
        pagesPerRecipe.Add(1, 1);
    }

    /// <summary>
    /// Changes the text on the pages for recipes with more than one page.
    /// </summary>
    private void SetPageText()
    {
        if (currPageNum == 1)
        {
            leftPageText.text = pageOneLeftText;
            rightPageText.text = pageOneRightText;
        }
        else if (currPageNum == 2)
        {
            leftPageText.text = pageTwoLeftText;
            rightPageText.text = pageTwoRightText;
        }
    }

    /// <summary>
    /// Moves up through the recipe if it has more than one page
    /// </summary>
    private void FlipPageUp()
    {
        if (isOpen) //This check might not be necessary depending on how we do the player controls
        {
            if (currPageNum < pagesPerRecipe[currRecipeNum])
            {
                currPageNum += 1;
            }
            else
            {
                currPageNum = 1;
            }
        }
        SetPageText();
    }

    /// <summary>
    /// Moves down through the recipe if it has more than one page
    /// </summary>
    private void FlipPageDown()
    {
        if (!isOpen) { return; }

        if (currPageNum > 1)
        {
            currPageNum -= 1;
        }
        else
        {
            currPageNum = pagesPerRecipe[currRecipeNum];
        }
        SetPageText();
    }

    /// <summary>
    /// Moves to the next recipe, or the first recipe if the player is on the last
    /// </summary>
    private void NextRecipe()
    {
        if (!isOpen) { return; }
        if (currRecipeNum < recipeList.Count)
        {
            currRecipeNum += 1;
        }
        else
        {
            currRecipeNum = 1;
        }
        SetRecipe();
    }

    /// <summary>
    /// Moves to the previous recipe, or the last recipe if the player is on the first
    /// </summary>
    private void PreviousRecipe()
    {
        if (!isOpen) { return; }
        if (currRecipeNum == 1)
        {
            currRecipeNum = recipeList.Count;
        }
        else
        {
            currRecipeNum -= 1;
        }
        SetRecipe();
    }

    /// <summary>
    /// Initializes the recipe text when the recipe number changes
    /// </summary>
    private void SetRecipe()
    {
        switch (recipeList[currRecipeNum])
        {
            case "Bacon":
                ShowBaconRecipe();
                break;
        }
    }

    private void ShowBaconRecipe()
    {
        currPageNum = 1;
        recipeTitle.text = "Bacon";
        pageOneLeftText= "•Pan\n•Bacon\n•Spatula";
        pageOneRightText = "1. Place pan on stove\n" +
                           "2. Add bacon to pan\n" +
                           "3. Use Spatula to flip bacon at correct times\n" +
                           "4. Add bacon to plate\n" +
                           "5.Serve";
        SetPageText();
    }
}
