using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Plate : Item
{
    [HideInInspector]
    public Item foodOnPlate;
    public string orderName;
    public float timer;
    public int orderNumber;
    public Transform placement;
    public Slider sliderTimer;
    Bacon baconRespawn;
    Egg eggRespawn;
    Menu menuOrder;

    public Plate()
    {
        Name = "Plate";
        Type = "Tool";
        status = Status.clean;
    }

    public void Awake()
    {
        currUses = 0;
        usesUntilDirty = 1;
    }

    public void Start()
    {
        menuOrder = GameObject.Find("MenuWindow").GetComponentInChildren<Menu>(); //This line returns an error every time the game is started
        menuOrder.PlaceOrder(orderName);
    }

    public void Update()
    {
        baconRespawn = GameManager.bacon;
        eggRespawn = GameManager.egg;
    }

    public override void CheckHand(PlayerController.ItemInMainHand item, PlayerController chef)
    {
        
        switch (item)
        {
            case PlayerController.ItemInMainHand.empty:
                Interaction = "Grab Plate";
                if (chef.isInteracting)
                {
                    gameObject.SetActive(false);
                }
                break;
            case PlayerController.ItemInMainHand.pan:
                if (chef.hand[0].GetComponent<Pan>() != null && chef.hand[0].GetComponent<Pan>().Occupied && chef.hand[0].GetComponent<Pan>().foodInPan.status == Status.cooked)
                {
                    Interaction = "Place food on plate";
                    if (chef.isInteracting)
                    {
                        Pan pan = chef.hand[0].GetComponent<Pan>();
                        foodOnPlate = chef.hand[0].GetComponent<Pan>().foodInPan;
                        foodOnPlate.toolItemIsOccupying = this;
                        foodOnPlate.gameObject.transform.parent = transform;
                        foodOnPlate.gameObject.transform.localPosition = new Vector3(0, .15f, 0);
                        Occupied = true;
                        pan.foodInPan = null;
                        pan.Occupied = false;
                        pan.CheckIfDirty();
                        chef.isInteracting = false;
                        Interaction = "";
                    }
                }
                break;
        }
    }
}
