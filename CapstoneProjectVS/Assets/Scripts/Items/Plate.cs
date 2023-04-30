using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Tool
{
    [Header("Plate Variables")]
    public bool Occupied;
    public Transform foodPlacement;
    public Dictionary<int, Item> foodOnPlate = new Dictionary<int, Item>();

    public void Start()
    {
        useBeforeDirty = 1;
    }

    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if (itemInMainHand != null)
        {
            // if item in hand is pan
            if(itemInMainHand.GetComponent<Pan>())
            {
                //get the food that's in the pan
                if (itemInMainHand.GetComponent<Pan>().itemsInPan.Count > 0)
                {
                    Ingredients food = itemInMainHand.GetComponent<Pan>().itemsInPan[0].GetComponent<Ingredients>();

                    if (food.cookingStatus == Ingredients.CookingStatus.cooked)
                    {
                        foodOnPlate.Add(foodOnPlate.Count, food);
                        food.transform.position = foodPlacement.transform.position; // put food on plate
                        food.transform.parent = transform; // have the food be the parent of egg
                        itemInMainHand.GetComponent<Pan>().itemsInPan.Remove(0);
                        itemInMainHand.GetComponent<Pan>().IsDirtied();

                        // Tutorial Level
                        DisplayIndicator(false);
                        if (GameManager.tutorialLevel)
                        {
                            Tutorial tutorial = FindObjectOfType<Tutorial>();
                            DisplayIndicator(false);

                            // if on step four then complete task
                            if (tutorial.playerOneCurrentStep == 9)
                            {
                                tutorial.playerOneSteps[tutorial.playerOneCurrentStep].isComplete = true;
                                tutorial.playerOneCurrentStep++;
                            }
                        }

                        return;
                    }
                }
                else if (!player.inventoryFull)
                {
                    Collect(player);
                    CheckCounterTop();
                    CheckSink();
                }
            }
            else if (itemInMainHand.GetComponent<Toast>())
            {
                Ingredients food = itemInMainHand.GetComponent<Ingredients>();

                if (food.cookingStatus == Ingredients.CookingStatus.cooked)
                {
                    foodOnPlate.Add(foodOnPlate.Count, food);
                    food.transform.position = foodPlacement.transform.position; // put food on plate
                    food.transform.parent = transform; // have the plate be the parent of food
                    food.canInteract = false;
                    food.gameObject.SetActive(true);
                    player.inventory[0] = null;
                    return;
                }
            }
            else if (!player.inventoryFull)
            {
                if (itemInMainHand.GetComponent<Ingredients>())
                {
                    Collect(player);
                    CheckCounterTop();
                    CheckSink();
                }
                else if (itemInMainHand.GetComponent<Tool>())
                {
                    Collect(player);
                    CheckCounterTop();
                    CheckSink();
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

    public override void Collect(PlayerController player = null, RatController rat = null)
    {
        base.Collect(player, rat);

        // Tutorial Level
        DisplayIndicator(false);

        if (GameManager.tutorialLevel)
        {
            Tutorial tutorial = FindObjectOfType<Tutorial>();

            // if on step four then complete task
            if (tutorial.playerTwoCurrentStep == 4)
            {
                tutorial.playerTwoSteps[4].isComplete = true;
                tutorial.playerTwoCurrentStep++;
                tutorial.playerTwoText.text = "Dirty dishes must be cleaned in the sink!";
            }
            else if(tutorial.playerTwoCurrentStep == 6)
            {
                tutorial.playerTwoSteps[6].isComplete = true;
                tutorial.playerTwoCurrentStep++;
                FindObjectOfType<SinkScript>().DisplayIndicator(false);
            }
            else if (tutorial.playerTwoCurrentStep == 8)
            {
                tutorial.playerTwoSteps[8].isComplete = true;
                tutorial.playerTwoCurrentStep++;
            }
        }
    }

    public IEnumerator TurnOffDisplay()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<Tutorial>().playerTwoText.text = "";
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "Placing food on Plate");
        Interaction = "";
        //First we check if the main hand is empty
        if (!player.inventory[0] && !player.isInteracting)
        {
            Interaction = "Grab Plate";
            willHideObjectAfterInteraction = true;
            isCheckingForInteraction = true;
            return;
        }
        //Next we check for interactions with items in the main hand.
        //Toast
        else if(player.inventory[0].TryGetComponent<Toast>(out Toast mToast))
        {
            if (mToast.cookingStatus != Ingredients.CookingStatus.uncooked)
            {
                Interaction = "Add Toast to plate";
                willHideObjectAfterInteraction = false;
                isCheckingForInteraction = true;
                return;
            }
        }
        //Pan
        else if (player.inventory[0].TryGetComponent<Pan>(out Pan mPan))
        {
            platePanInteraction(mPan);
            if (Interaction != "") //If the pan method set the text
            {
                Debug.Log(Interaction);
                return;
            }
        }
        //No interactions are possible with the item in the main hand.

        //Now we check if the offhand is empty
        if (!player.inventory[1] && !player.isInteracting)
        {
            Interaction = "Grab Plate";
            willHideObjectAfterInteraction = true;
            isCheckingForInteraction = true;
            return;
        }
        //Next we check for interactions with items in the off hand.
        //Toast
        else if (player.inventory[1].TryGetComponent<Toast>(out Toast oToast))
        {
            if (oToast.cookingStatus != Ingredients.CookingStatus.uncooked)
            {
                Interaction = $"Add Toast to plate";
                willHideObjectAfterInteraction = false;
                isCheckingForInteraction = true;
                return;
            }
        }
        //Pan
        else if (player.inventory[1].TryGetComponent<Pan>(out Pan oPan))
        {
            platePanInteraction(oPan);
            if (Interaction != "") //If the pan method set the text
            {
                Debug.Log(Interaction);
                return;
            }
        }
        if (player.inventoryFull)
        {
            //There is an item in the inventory we can't interact with
            Interaction = "Inventory Full";
            return;
        }

    }

    /// <summary>
    /// Helper function for when the player is holding a pan because god help me
    /// </summary>
    /// <param name="pan"></param>
    private void platePanInteraction(Pan pan)
    {
        //There is food in the pan
        if (pan.itemsInPan.Count > 0)
        {
            //There is food on the plate
            if (foodOnPlate.Count > 0)
            {
                Interaction = "Plate full";
                return;
            }
            //There is not food on the plate
            else
            {
                if (pan.itemsInPan[0].GetComponent<Ingredients>().cookingStatus == Ingredients.CookingStatus.burnt ||
                    pan.itemsInPan[0].GetComponent<Ingredients>().cookingStatus == Ingredients.CookingStatus.cooked ||
                    pan.itemsInPan[0].GetComponent<Ingredients>().cookingStatus == Ingredients.CookingStatus.spoiled)
                {
                    Interaction = $"Add {pan.itemsInPan[0].name} to plate";
                    willHideObjectAfterInteraction = false;
                    isCheckingForInteraction = true;
                    return;
                }
                else if (pan.itemsInPan[0].GetComponent<Ingredients>().cookingStatus == Ingredients.CookingStatus.uncooked)
                {
                    Interaction = "Food is not done!";
                    return;
                }
                else
                {
                    Interaction = "Something is wrong with this food!";
                    Debug.LogWarning("Hey. Either something that isn't an ingredient is in the pan (you should probably have hit an error before this message" +
                        "if that's the case), or there's an ingredient in the pan that is not burnt, cooked, spoiled, or uncooked, which should be impossible." +
                        "If there's a new 5th state, cool, add that. If there isn't one… Godspeed. You'll need all the luck you can get to figure this out.");
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Automatically increments timesUsed and then determines if the plate is dirty yet
    /// </summary>
    public override void IsDirtied()
    {
        timesUsed++;
        if (timesUsed >= useBeforeDirty)
        {
            status = Status.dirty;
            isDirty = true;
            Interaction = "Plate is dirty";
        }
    }


    public override void IsClean()
    {

        status = Status.clean;
        timesUsed = 0;
        isDirty = false;

    }
}
