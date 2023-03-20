using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixingBowl : Tool
{
    public Dictionary<int, Item> itemsInMixingBowl = new Dictionary<int, Item>();
    public Dictionary<int, Mix> mixes = new Dictionary<int, Mix>();

    [Header("Placement")]
    public Transform itemPlacement;
    
    [Header("UI")]
    public GameObject mixtureDisplay;
    public Text displayText;

    public string nameOfMixture { get; set; }
    private int numberOfCorrectIngredients;

    public override void Start()
    {
        base.Start();
        MixingList();
    }

    public override void Interact(Item item, PlayerController player)
    {
        // if item in main hand is not null
        if(item != null)
        {
            // if item is an ingredient
            if(item.GetComponent<Ingredients>() != null)
            {
                Ingredients ingredients = item.GetComponent<Ingredients>(); // get ingredient

                // if ingredient is mixable
                if (ingredients.isMixable)
                {
                    player.inventory[0] = null;
                    itemsInMixingBowl.Add(itemsInMixingBowl.Count, item);
                    item.gameObject.SetActive(true);
                    item.canInteract = false;
                    item.transform.position = itemPlacement.position;
                    item.transform.parent = transform;
                    CheckMix();
                }
            }
        }
        else
        {
            // collect if item in main is null
            Collect(player);
        }
    }

    public void CheckMix()
    {
        numberOfCorrectIngredients = 0;

        // for each possible mixture
        for (int i = 0; mixes.Count > i; i++)
        {
            // for each ingredient in the mixture
            for (int c = 0; mixes[i].ingredients.Length > c; c++)
            {
                // for each ingredient in the bowl
                for (int x = 0; x < itemsInMixingBowl.Count; x++)
                {
                    // if the ingredients name equals the mixture ingredients name
                    if (itemsInMixingBowl[x].Name == mixes[i].ingredients[c])
                    {
                        numberOfCorrectIngredients++;
                    }
                }
            }

            /* check to see if the number of correct ingredients equal the number of ingredients needed to make the ingredients and
            make sure the number of ingredients in the bowl is the same number of ingredients needed to make the mixture*/
            if (numberOfCorrectIngredients == mixes[i].ingredients.Length && itemsInMixingBowl.Count == mixes[i].ingredients.Length)
            {
                nameOfMixture = mixes[i].NameOfRecipe;
                mixtureDisplay.SetActive(true);
                displayText.text = nameOfMixture + " Mixture";
            }
        }
    }

    public void MixingList()
    {
        // chessy omelet
        mixes.Add(0, new Mix());
        mixes[0].NameOfRecipe = "Omelet";
        string[] omeletIngredients = { "Egg", "Shredded Cheese" };
        mixes[0].ingredients = omeletIngredients;
    }
}
