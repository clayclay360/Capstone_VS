using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Tool
{
    public Dictionary<int, Item> itemsInPan = new Dictionary<int, Item>();
    

    [Header("CookingCheck")]
    public GameObject cookingCheck;

    [HideInInspector]
    public Stove stove; // this variable is to get what stove the pan is occupying

    Egg egg;
    Toast toast;
    HashBrown hashBrown;
    Bacon bacon;

    public Pan()
    {
        Name = "Pan";
        Interaction = "";
        isWashable = true;
        useBeforeDirty = 1;
        status = Status.clean;
    }

    public void Update()
    {
        //if (timesUsed >= useBeforeDirty)
        //{
        //    isDirty = true;
        //    Interaction = "Pan is dirty!";
        //} else
        //{
        //    isDirty = false;
        //}

        switch (status)
        {
            case Status.clean:
                mainSprite = clean;
                break;

            case Status.dirty:
                mainSprite = dirty;
                break;
        }
    }


    public override void Interact(Item itemInMainHand, PlayerController player)
    {
        //check to see if there's anything in the mainhand
        if(itemInMainHand != null)
        {
            //if spatula is in main hand
            if(itemInMainHand.GetComponent<Spatula>() != null)
            {
                //if Pan is not empty and is hot
                if (itemsInPan.Count > 0 && isHot && itemsInPan[0].GetComponent<Ingredients>().cookingStatus == Ingredients.CookingStatus.uncooked)
                {
                    Debug.Log("Spatula Used");
                    cookingCheck.GetComponent<CookingCheckScript>().CheckAttempt();
                    itemInMainHand.GetComponent<Spatula>().timesUsed += 1;
                    itemInMainHand.GetComponent<Spatula>().IsDirtied();
                }
                else
                {
                    if (!player.inventoryFull)
                    {
                        Collect(player);
                    }
                    CheckCounterTop();
                    CheckSink();
                }
            }
            else if(itemInMainHand.GetComponent<Egg>() != null && !isDirty)
            {
                egg = itemInMainHand.GetComponent<Egg>();

                if (itemsInPan.Count < containerSize && egg.cookingStatus == Ingredients.CookingStatus.uncooked) // if pan is not full and egg is not cooked
                {
                    itemsInPan.Add(itemsInPan.Count, egg); // add egg in the pan inventory.
                    egg.transform.position = transform.position; // put egg on pan
                    egg.transform.parent = transform; // have the pan be the parent of egg
                    egg.gameObject.SetActive(true); // display pan
                    egg.canInteract = false; // egg can't not be interacted
                    egg.SetCollider(); //sets the collider of the food
                    egg.state = Egg.State.yoked; // change state
                    egg.SwitchModel(Egg.State.yoked); // change model
                    player.inventory[0] = null; // item in main hand is null
                    player.isInteracting = false; //player is no longer interacting
                    player.canCollect = false; //player cannot collect items

                    if (isHot)
                    {
                        CookingCheck(cookingCheck, 10, egg); // start cooking // the cook time is 2 temporary
                    }
                }

                
            }
            else if (itemInMainHand.GetComponent<Bacon>() != null && !isDirty)
            {
                bacon = itemInMainHand.GetComponent<Bacon>();

                if (itemsInPan.Count < containerSize && bacon.cookingStatus == Ingredients.CookingStatus.uncooked) // if pan is not full and egg is not cooked
                {
                    itemsInPan.Add(itemsInPan.Count, bacon); // add bacon in the pan inventory.
                    bacon.transform.position = transform.position; // put bacon on pan
                    bacon.transform.parent = transform; // have the pan be the parent of bacon
                    bacon.gameObject.SetActive(true); // display pan
                    bacon.canInteract = false; // bacon can't not be interacted
                    bacon.SetCollider(); //sets the collider of the food
                    player.inventory[0] = null; // item in main hand is null
                    player.isInteracting = false; //player is no longer interacting
                    player.canCollect = false; //player cannot collect items

                    if (isHot)
                    {
                        // Tutorial Level
                        if (GameManager.tutorialLevel)
                        {
                            Tutorial tutorial = FindObjectOfType<Tutorial>();
                            DisplayIndicator(false);

                            // if on step four then complete task
                            if (tutorial.playerOneCurrentStep == 6)
                            {
                                tutorial.playerOneController.isDisplayingInformation = true;
                                tutorial.playerOneText.text = "Use the spatula when the bar is below the spatula icon!";
                                tutorial.playerOneSteps[6].isComplete = true;
                                tutorial.playerOneCurrentStep++;
                                Invoke("StartCooking", 3);
                            }
                        }
                        else
                        {
                            CookingCheck(cookingCheck, 10, bacon); // start cooking // the cook time is 2 temporary
                        }
                    }
                }
            }
            else if(itemInMainHand.GetComponent<Toast>() != null)
            {
                toast = itemInMainHand.GetComponent<Toast>();

                if (itemsInPan.Count < containerSize && toast.cookingStatus == Ingredients.CookingStatus.uncooked)// if pan is not full and toast is not toasted
                {
                    itemsInPan.Add(itemsInPan.Count, toast); // add toast to pan inventory
                    toast.transform.position = transform.position; // put toast on pan
                    toast.transform.parent = transform; // make toast child of pan
                    toast.gameObject.SetActive(true); // display toast
                    toast.canInteract = false; // make toast uninteractable
                    player.inventory[0] = null; // item in main hand null
                    player.isInteracting = false; //player is no longer interacting
                    player.canCollect = false; //player cannot collect items
                }

                if (isHot)
                {
                    CookingCheck(cookingCheck, 2, toast); // start cooking // the cook time is 2 temporary
                }

            }
            else if (itemInMainHand.GetComponent<HashBrown>() != null)
            {
                hashBrown = itemInMainHand.GetComponent<HashBrown>();

                if(itemsInPan.Count < containerSize && hashBrown.cookingStatus == Ingredients.CookingStatus.uncooked) // if pan is not full and hashbornw is not cooked
                {
                    itemsInPan.Add(itemsInPan.Count, hashBrown); // adde hashbrown to pan inventory
                    hashBrown.transform.position = transform.position; // put hashbrown on pan
                    hashBrown.transform.parent = transform; // make hashbrown child of pan
                    hashBrown.gameObject.SetActive(true); // display hashbrown
                    hashBrown.canInteract = false; // make hashbrown uninteractable
                    player.inventory[0] = null; // item in main hand null
                    player.isInteracting = false; //player is no longer interacting
                    player.canCollect = false; //player cannot collect items
                }

                if (isHot)
                {
                    CookingCheck(cookingCheck, 2, hashBrown); // start cooking // the cook time is 2 temporary
                }
            }
            else if(itemInMainHand.GetComponent<MixingBowl>() != null)
            {
                MixingBowl mixingBowl = itemInMainHand.GetComponent<MixingBowl>();

                // check what's in the mixing bowl, this swtich statement also might not be necessary
                switch (mixingBowl.nameOfMixture)
                {
                    case "Omelet":
                        for(int i = 0; mixingBowl.itemsInMixingBowl.Count > i; i++)
                        {
                            mixingBowl.itemsInMixingBowl[i].transform.position = transform.position; // change position
                            mixingBowl.itemsInMixingBowl[i].transform.parent = transform; // change parent
                            itemsInPan.Add(i, mixingBowl.itemsInMixingBowl[i]); // add ingredient to pan
                            mixingBowl.nameOfMixture = ""; // change mixture name
                            for(int c = 0; c < mixingBowl.itemsInMixingBowl.Count; c++)
                            {
                                mixingBowl.itemsInMixingBowl.Remove(c); // remove ingredients from bowl
                            }
                            player.isInteracting = false; //player is no longer interacting
                            player.canCollect = false; //player cannot collect items
                        }
                        break;
                }

                if (isHot)
                {
                    CookingCheck(cookingCheck, 2, itemsInPan[0].GetComponent<Ingredients>()); // start cooking // the cook time is 2 temporary
                }
            }
            else if (itemInMainHand.GetComponent<CuttingBoard>() != null)
            {
                CuttingBoard cuttingBoard = itemInMainHand.GetComponent<CuttingBoard>();

                // check what's in the mixing bowl, this swtich statement also might not be necessary
                switch (cuttingBoard.nameOfMixture)
                {
                    case "Omelet":
                        for (int i = 0; cuttingBoard.itemsOnCuttingBoard.Count > i; i++)
                        {
                            cuttingBoard.itemsOnCuttingBoard[i].transform.position = transform.position; // change position
                            cuttingBoard.itemsOnCuttingBoard[i].transform.parent = transform; // change parent
                            itemsInPan.Add(i, cuttingBoard.itemsOnCuttingBoard[i]); // add ingredient to pan
                            player.isInteracting = false; //player is no longer interacting
                            player.canCollect = false; //player cannot collect items
                        }
                        break;
                }

                if (isHot)
                {
                    CookingCheck(cookingCheck, 2, itemsInPan[0].GetComponent<Ingredients>()); // start cooking // the cook time is 2 temporary
                }
            }
            else
            {
                //second hand is empty
                if (itemsInPan.Count == 0 && !IsCookingFood())
                {
                    Collect(player);
                    isHot = false;
                    CheckCounterTop();
                    CheckSink();
                }
            }
        }
        else
        {
            //main hand is empty
            if(!IsCookingFood())
            {
                // if the food is not cooking
                Collect(player);
                isHot = false;
                CheckCounterTop();
                CheckSink();
            }
        }
    }

    public void StartCooking()
    {
        FindObjectOfType<Tutorial>().playerOneController.interactionText.text = "";
        FindObjectOfType<Tutorial>().playerOneController.isDisplayingInformation = false;
        CookingCheck(cookingCheck, 10, bacon); // start cooking

    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    {
        player.HelpIndicator(true, "Cooking");

        if (IsCookingFood())
        {
            if ((player.inventory[0] && player.inventory[0].TryGetComponent<Spatula>(out _)) || 
                (player.inventory[1] && player.inventory[1].TryGetComponent<Spatula>(out _)))
            {
                Interaction = "Use Spatula";
            }
            else
            {
                Interaction = "Need Spatula!";
            }
            return;
        }
        //When the pan not cooking food and empty
        else if (!IsCookingFood())
        {
            //There is an item in the pan (done cooking)
            if (!player.inventory[0] || !player.inventory[1])
            {
                Interaction = "Grab Pan";
                if (player.isInteracting)
                {
                    player.isInteracting = false;
                    player.canInteract = false;
                    Interaction = "";
                }
            }
            //There is no item in the pan(pre-cooking)
            if (player.inventoryFull && !player.inventory[0].GetComponent<Ingredients>())
            {
                Interaction = "Inventory Full";
                return;
            }
            else if(itemsInPan.Count >= containerSize)
            {
                if (player.inventory[0] != null && player.inventory[0].GetComponent<Ingredients>())
                {
                    Interaction = "Pan is Full";
                    return;
                }
            }

            else if (player.inventory[0] && player.inventory[0].TryGetComponent<Ingredients>(out Ingredients ingredientMH) && !isDirty)
                {
                    Interaction = $"Add {ingredientMH.Name} to pan";
                    if (player.isInteracting)
                    {
                        player.isInteracting = false;
                        player.canInteract = false;
                    }
                }
            else if (isDirty)
            {
                Interaction = "Pan is dirty";
            }
            //else if (player.inventory[1] && player.inventory[1].TryGetComponent<Ingredients>(out Ingredients ingredientOH))
            //{
            //    Interaction = $"Add {ingredientOH.Name} to pan";
            //    if (player.isInteracting)
            //    {
            //        player.isInteracting = false;
            //        player.canInteract = false;
            //    }
            //}
            //else if (player.inventory[0] && player.inventory[0].TryGetComponent<MixingBowl>(out MixingBowl mixingBowlMH))
            //{
            //    if(mixingBowlMH.nameOfMixture != "")
            //    {
            //        Interaction = $"Add {mixingBowlMH.nameOfMixture} mixture to pan";
            //    }
            //}
            //else if (player.inventory[1] && player.inventory[1].TryGetComponent<MixingBowl>(out MixingBowl mixingBowlOH))
            //{
            //    if (mixingBowlOH.nameOfMixture != "")
            //    {
            //        Interaction = $"Add {mixingBowlOH.nameOfMixture} mixture to pan";
            //    }
            //}
            return;
        }        
    }

    // this function is to get information whether the pan is cooking for or not
    public bool IsCookingFood()
    {
        if(itemsInPan.Count > 0)
        {
            if (!itemsInPan[0].GetComponent<Ingredients>().isCooking)
            {
                // pan is not cooking food
                return false;
            }
        }
        else
        {
            // pan empty
            return false;
        }
        return true;
    }

    public override void Collect(PlayerController player = null, RatController rat = null)
    {
        base.Collect(player, rat);

        if (GameManager.tutorialLevel)
        {
            DisplayIndicator(false);
        }

        if (GameManager.tutorialLevel)
        {
            Tutorial tutorial = FindObjectOfType<Tutorial>();

            // if on step four then complete task
            if(tutorial.playerOneCurrentStep == 2)
            {
                tutorial.playerOneSteps[tutorial.playerOneCurrentStep].isComplete = true;
                tutorial.playerOneCurrentStep++;
            }

            if(tutorial.playerOneCurrentStep == 8)
            {
                tutorial.playerOneSteps[tutorial.playerOneCurrentStep].isComplete = true;
                tutorial.playerOneCurrentStep++;
            }
        }

        // if the pan is occupying a stove than set it to false
        if (stove != null)
        {
            stove.isOccupied = false;
            stove.canInteract = true;
            stove = null;
        }

    }


    public void DisplayIndicator(bool condition)
    {
        indicator.SetActive(condition);
    }

    public override void IsDirtied()
    {
        if (timesUsed >= useBeforeDirty)
        {
            status = Status.dirty;
            isDirty = true;
            mainSprite = dirty;
            Interaction = "Pan is dirty";
        }
    }


    public override void IsClean()
    {

        status = Status.clean;
        timesUsed = 0;
        isDirty = false;
        mainSprite = clean;
    }

    public void Start()
    {
        base.Start();
        useBeforeDirty = 1;
    }
}
