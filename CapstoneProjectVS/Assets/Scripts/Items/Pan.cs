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
                if (itemsInPan.Count > 0 && isHot)
                {
                    Debug.Log("Sptula Used");
                    cookingCheck.GetComponent<CookingCheckScript>().CheckAttempt();
                }
                else
                {
                    Collect(player);
                }
            }
            else if(itemInMainHand.GetComponent<Egg>() != null)
            {
                egg = itemInMainHand.GetComponent<Egg>();

                if (itemsInPan.Count <= containerSize && egg.cookingStatus == Ingredients.CookingStatus.uncooked) // if pan is not full and egg is not cooked
                {
                    itemsInPan.Add(itemsInPan.Count, egg); // add egg in the pan inventory.
                    egg.transform.position = transform.position; // put egg on pan
                    egg.transform.parent = transform; // have the pan be the parent of egg
                    egg.gameObject.SetActive(true); // display pan
                    egg.canInteract = false; // egg can't not be interacted
                    egg.state = Egg.State.yoked; // change state
                    egg.SwitchModel(Egg.State.yoked); // change model
                    player.inventory[0] = null; // item in main hand is null

                    if (isHot)
                    {
                        CookingCheck(cookingCheck, 2, egg); // start cooking // the cook time is 2 temporary
                    }
                }
            }
            else if (itemInMainHand.GetComponent<Bacon>() != null)
            {
                bacon = itemInMainHand.GetComponent<Bacon>();

                if (itemsInPan.Count <= containerSize && bacon.cookingStatus == Ingredients.CookingStatus.uncooked) // if pan is not full and egg is not cooked
                {
                    itemsInPan.Add(itemsInPan.Count, bacon); // add bacon in the pan inventory.
                    bacon.transform.position = transform.position; // put bacon on pan
                    bacon.transform.parent = transform; // have the pan be the parent of bacon
                    bacon.gameObject.SetActive(true); // display pan
                    bacon.canInteract = false; // bacon can't not be interacted
                    player.inventory[0] = null; // item in main hand is null

                    if (isHot)
                    {
                        CookingCheck(cookingCheck, 2, bacon); // start cooking // the cook time is 2 temporary
                    }
                }
            }
            else if(itemInMainHand.GetComponent<Toast>() != null)
            {
                toast = itemInMainHand.GetComponent<Toast>();

                if (itemsInPan.Count <= containerSize && toast.cookingStatus == Ingredients.CookingStatus.uncooked)// if pan is not full and toast is not toasted
                {
                    itemsInPan.Add(itemsInPan.Count, toast); // add toast to pan inventory
                    toast.transform.position = transform.position; // put toast on pan
                    toast.transform.parent = transform; // make toast child of pan
                    toast.gameObject.SetActive(true); // display toast
                    toast.canInteract = false; // make toast uninteractable
                    player.inventory[0] = null; // item in main hand null
                }

                if (isHot)
                {
                    CookingCheck(cookingCheck, 2, toast); // start cooking // the cook time is 2 temporary
                }

            }
            else if (itemInMainHand.GetComponent<HashBrown>() != null)
            {
                hashBrown = itemInMainHand.GetComponent<HashBrown>();

                if(itemsInPan.Count <= containerSize && hashBrown.cookingStatus == Ingredients.CookingStatus.uncooked) // if pan is not full and hashbornw is not cooked
                {
                    itemsInPan.Add(itemsInPan.Count, hashBrown); // adde hashbrown to pan inventory
                    hashBrown.transform.position = transform.position; // put hashbrown on pan
                    hashBrown.transform.parent = transform; // make hashbrown child of pan
                    toast.gameObject.SetActive(true); // display hashbrown
                    hashBrown.canInteract = false; // make hashbrown uninteractable
                    player.inventory[0] = null; // item in main hand null
                }

                if (isHot)
                {
                    CookingCheck(cookingCheck, 2, hashBrown); // start cooking // the cook time is 2 temporary
                }
            }
            else
            {
                //second hand is empty
                if (itemsInPan != null)
                {
                    Collect(player);
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

                // if the pan is occupying a stove than set it to false
                if(stove != null)
                {
                    stove.isOccupied = false;
                    stove = null;
                }
            }
        }
    }

    //public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController player)
    //{
    //    switch (item)
    //    {
    //        case PlayerController.ItemInMainHand.empty:
    //            Interaction = "Grab Pan";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.hashbrown:
    //            Interaction = "Grab Pan";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.spatula:
    //            Interaction = "Grab Pan";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.egg:
    //            Interaction = "Grab Pan";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //        case PlayerController.ItemInMainHand.bacon:
    //            Interaction = "Grab Pan";
    //            if (player.isInteracting)
    //            {
    //                player.canInteract = false;
    //                gameObject.SetActive(false);
    //                Interaction = "";
    //            }
    //            break;
    //    }
    //}

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
}
