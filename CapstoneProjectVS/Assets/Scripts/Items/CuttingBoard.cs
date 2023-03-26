using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingBoard : Tool
{
    public Dictionary<int, Item> itemsOnCuttingBoard = new Dictionary<int, Item>();
    public Dictionary<int, Chop> chop = new Dictionary<int, Chop>();

    [SerializeField] private Transform knife;
    [SerializeField] private Slider slider;

    [Header("Placement")]
    public Transform itemPlacement;

    [Header("UI")]
    public GameObject ChopDisplay;
    public Image[] ingredientDisplay;
    public Text displayText;

    private bool isChopping;
    public string nameOfMixture { get; set; }
    private int numberOfIngredients;
    public override void Start()
    {
        base.Start();
        ChopList();
        isWashable = true;
    }

    public override void Interact(Item item, PlayerController player)
    {
        //base.Interact(item, player);
        if (item = null)
        {
            if (item.GetComponent<Ingredients>() != null)
            {
                Ingredients ingredient = item.GetComponent<Ingredients>(); // get ingredient

                if (ingredient.isCuttable)
                {
                    player.inventory[0] = null;
                    ingredientDisplay[itemsOnCuttingBoard.Count].sprite = item.mainSprite;
                    ingredientDisplay[itemsOnCuttingBoard.Count].gameObject.SetActive(true);
                    itemsOnCuttingBoard.Add(itemsOnCuttingBoard.Count, item);
                    item.gameObject.SetActive(true); // set active true
                    item.canInteract = false; // no interaction
                    item.transform.position = itemPlacement.position; // change position
                    item.transform.parent = transform; // change parent
                    
                    CheckChopping();

                    if (ingredient.GetComponent<Egg>() != null)
                    {
                        ingredient.GetComponent<Egg>().SwitchModel(Egg.State.yoked);
                    }
                }

            }
            else
            {
                Collect(player);
                CheckCounterTop();
                CheckSink();
            }
        }

       
    }

    public void CheckChopping()
    {
        numberOfIngredients = 0;

        // for each possible mixture
        for (int i = 0; chop.Count > i; i++)
        {
            // for each ingredient in the mixture
            for (int c = 0; chop[i].ingredients.Length > c; c++)
            {
                // for each ingredient in the bowl
                for (int x = 0; x < itemsOnCuttingBoard.Count; x++)
                {
                    // if the ingredients name equals the mixture ingredients name
                    if (itemsOnCuttingBoard[x].Name == chop[i].ingredients[c])
                    {
                        numberOfIngredients++;
                    }
                }
            }

            /* check to see if the number of correct ingredients equal the number of ingredients needed to make the ingredients and
            make sure the number of ingredients in the bowl is the same number of ingredients needed to make the mixture*/
            if (numberOfIngredients == chop[i].ingredients.Length && itemsOnCuttingBoard.Count == chop[i].ingredients.Length)
            {
                nameOfMixture = chop[i].NameOfRecipe;
                //mixtureDisplay.SetActive(true);
                displayText.text = nameOfMixture + "Chopped Ingredients";
            }
        }
    }

    public void ChopList()
    {
        chop.Add(0, new Chop());
        chop[0].NameOfRecipe = "Omelet";
        string[] omeletIngredients = { "Egg", "Shredded Cheese" };
        chop[0].ingredients = omeletIngredients;
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        //Both hands are empty, pick up the cutting board
        if (!player.inventory[0] && !player.inventory[1])
        {
            Interaction = $"Grab {Name}";
        }
        //Check hands for ingredients: if either has one, we can add it to the board
        else if (player.inventory[0] && player.inventory[0].TryGetComponent<Ingredients>(out Ingredients ingredientMH))
        {
            Interaction = $"Add {ingredientMH.Name} to cutting board";
        }
        else if (player.inventory[1] && player.inventory[1].TryGetComponent<Ingredients>(out Ingredients ingredientOH))
        {
            Interaction = $"Add {ingredientOH.Name} to cutting board";
        }
        //Player doesn't have an ingredient in their hands. If either hand is empty, they can pick up the board
        else if (!player.inventory[0] || !player.inventory[1])
        {
            Interaction = $"Grab {Name}";
        }

    }

}
