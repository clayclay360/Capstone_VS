using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //variables for movement and cameras
    [Header("Movement")]
    private Vector3 moveVec;
    private Vector3 rotateVec;
    private float rightAnalogMagnitude;
    public float movingSpeed;
    private const float MOVESPEED = 7f;

    [Header("Camera")]
    public Camera playerCamera;
    private Vector3 camOffset = new Vector3(0, 10, -3);
    private Vector3 camRotation = new Vector3(70, 0, 0);

    //Animator
    [Header("Animator")]
    public Animator animator;

    [Header("Highlight")]
    private Color highlightColor;

    [Header("Interaction")]
    public Text interactionText;
    public bool canInteract;
    public bool canCollect;
    public bool isInteracting;
    public Image interactButtonIcon;

    [Header("Knife Throw")]
    public GameObject knifePrefab;
    public Transform knifeSpawnTransform;
    public float power;
    public float throwCooldown;
    public bool readyToThrow;

    //Inventory
    public enum ItemInMainHand {empty, egg, spatula, pan, bacon, hashbrown, toast };
    public ItemInMainHand itemInMainHand;
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    [SerializeField] private GameObject interactableObject;
    [Header("Inventory")]
    public Text Inv1;
    public Text Inv2;
    public bool inventoryFull = false;

    private Cookbook cookbook;

    [Header("Icons")]
    public Image[] Icon;
    public Sprite EggIcon;
    public Sprite BaconIcon;
    public Sprite PanIcon;
    public Sprite SpatulaIcon;
    public Sprite HashBrownIcon;

    [Header("Prefabs")]
    public GameObject pan;
    public GameObject spatula;
    public GameObject egg;
    public GameObject bacon;
    public GameObject hashbrown;

    [Header("Popout")]
    public Animator popOutAnimator;
    public GameObject helpGuide;

    private bool helpIndicatorOpen;
    private bool helpAvailable;
    private string nameOfGuide;

    void Awake()
    {
        playerCamera.transform.position = gameObject.transform.position + camOffset;
        playerCamera.transform.eulerAngles = camRotation;

        inventory.Add(0, null);
        inventory.Add(1, null);
        inventory.Add(2, null); // this is just for the player to be able to switch hand

        AssignHighlightColor();

        RatSpawn[] holeList = FindObjectsOfType<RatSpawn>();
        foreach(RatSpawn hole in holeList)
        {
            hole.playerList.Add(gameObject);
        }
    }

    public void Update()
    {
        foreach (Image img in Icon)
        {
            img.GetComponent<Image>();
        }
        UpdateInteractionIcon();
    }

    #region movement
    void FixedUpdate()
    {
        PlayerMovement();
        CheckInventory();
        GetNameInMain();
        Icons();
    }

    //Collects input from the controller
    public void OnMove(InputValue value)
    {
        Vector2 moveVal = value.Get<Vector2>();
        moveVec = new Vector3(moveVal.x, 0, moveVal.y);
    }
    //Collects input from the controller
    public void OnLook(InputValue value)
    {
        Vector2 rotateVal = value.Get<Vector2>();
        rotateVec = new Vector3(rotateVal.x, 0f, rotateVal.y).normalized;
        rightAnalogMagnitude = rotateVec.magnitude;
    }

    public void PlayerMovement()
    {
        //Set the animator to be 0 if the player is still or to the movement speed
        //Movement speed slows when player is in front of objects
        float animatorSpeed = moveVec.magnitude <= 0.05f ? 0 : movingSpeed;
        animator.SetFloat("BlendX", animatorSpeed);

        #region Raycast Set Speed
        //Raycast checks for an object in front of the player and its distance
        //The closer an object is in front of the player the lower their speed
        float range = 7f;
        Vector3 direction = new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized;
        Ray slowSpeedRay = new Ray(transform.position + new Vector3(0f, 1.0f, 0f), direction * range);
        if (Physics.Raycast(slowSpeedRay, out RaycastHit hit, range))
        {

            //forDist gets the current distance between the point and the player.
            //newDist is the distance between them AFTER the player moves.
            //We calculate this before movement so that the player can still
            //move away from the object even if they're facing it.
            float rayDist = hit.distance;
            float forDist = Vector3.Distance(transform.position, hit.point);
            float newDist = Vector3.Distance(transform.position + MOVESPEED * Time.deltaTime * moveVec, hit.point);
            if (rayDist < 0.65f && newDist < forDist)
            {
                movingSpeed = 0f;
            }
            else if (rayDist < range)
            {
                //(1/3)(x+1)^3. Don't ask.
                movingSpeed = Mathf.Clamp((Mathf.Pow((rayDist + 1f), 3) * .3f), 0.0f, MOVESPEED);
            }
        }
        else
        {
            movingSpeed = MOVESPEED;
        }
        #endregion

        //Movement
        if (Mathf.Abs(moveVec.x) > 0 || Mathf.Abs(moveVec.z) > 0)
        {
            if (moveVec.magnitude > 0.1f)
            { 
                transform.position += movingSpeed * Time.deltaTime * moveVec;
            }
        }

        //Rotation
        rotateVec = rightAnalogMagnitude < 0.05f ? moveVec : rotateVec;
        if (Mathf.Abs(rotateVec.x) > 0 || Mathf.Abs(rotateVec.z) > 0)
        {
            Vector3 rotateDirection = (Vector3.right * rotateVec.x) + (Vector3.forward * rotateVec.z);
            if (Mathf.Abs(rotateDirection.magnitude) > 0.05f)
            {
                float angle = Mathf.Atan2(rotateVec.x, rotateVec.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }

        //Camera
        playerCamera.transform.position = gameObject.transform.position + camOffset;
    }
    #endregion

    private void GetNameInMain()
    {
        if(inventory[0] != null)
        {
            switch (inventory[0].Name)
            {
                case "Egg":
                    itemInMainHand = ItemInMainHand.egg;
                    break;
                case "Spatula":
                    itemInMainHand = ItemInMainHand.spatula;
                    break;
                case "Pan":
                    itemInMainHand = ItemInMainHand.pan;
                    break;
                case "Bacon":
                    itemInMainHand = ItemInMainHand.bacon;
                    break;
                case "Hashbrown":
                    itemInMainHand = ItemInMainHand.hashbrown;
                    break;
                default:
                    itemInMainHand = ItemInMainHand.empty;
                    break;
            }
        }
        else
        {
            itemInMainHand = ItemInMainHand.empty;
        }
    }

    public void Icons()
    {
        for(int i = 0; Icon.Length > i; i++)
        {
            if (inventory[i] != null)
            {
                Icon[i].sprite = inventory[i].mainSprite;
            }
            else
            {
                Icon[i].sprite = null;
            }
        }
    }

    public void CheckInventory()
    {
        if(inventory[0] != null && inventory[1] != null)
        {
            inventoryFull = true;
        }
        else
        {
            inventoryFull = false;
        }
        
        if (inventory[0] != null)
        {
            Inv1.text = inventory[0].name;
        }
        else
        {
            Inv1.text = "";
        }

        if (inventory[1] != null)
        {
            Inv2.text = inventory[1].name;
        }
        else
        {
            Inv2.text = "";
        }
    }

    //switch hands
   public void OnSwitchHand()
    {
        Debug.Log("Inventory 1: " + inventory[0] + " Inventory 2: " + inventory[1]);
        inventory[2] = inventory[0];
        inventory[0] = inventory[1];
        inventory[1] = inventory[2];
    }

    public void OnInteract()
    {
        if (canInteract)
        {
            isInteracting = true;
        }
        //check if the player is near a interactable object
        if (interactableObject != null)
        {
            if (interactableObject.GetComponent<IInteractable>() != null)
            {
                Item item = interactableObject.GetComponent<Item>();
                item.Interact(inventory[0], this);
                HelpIndicator(false);
                interactableObject = null;
                
            }
        }
    }

    //player is ready to interact
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Item>() != null)
        {
            canInteract = true;
            interactableObject = other.gameObject;
            other.gameObject.GetComponent<Item>().CheckHand(itemInMainHand, this);
            interactionText.text = other.gameObject.GetComponent<Item>().Interaction;

            //Change highlight of the object to the player color
            if (other.TryGetComponent<Outline>(out Outline ol))
            {
                ol.enabled = true;
                ol.OutlineColor = highlightColor;
                ol.OutlineWidth = 3f;
            }

            if(other.gameObject.GetComponent<ICollectable>() != null)
            {
                if (!inventoryFull)
                {
                    //If the player's inventory isn't full then they can collect
                    canCollect = true;
                    interactableObject = other.gameObject;
                    interactionText.text = other.gameObject.GetComponent<Item>().Interaction;
                }
            }
        }
    }

    //player is not ready to interact
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Item>() != null)
        {
            interactionText.text = "";
            isInteracting = false;
            canInteract = false;
            interactableObject = null;
            canCollect = false;
            HelpIndicator(false);

            if (other.TryGetComponent<Outline>(out _))
            {
                other.GetComponent<Item>().ResetHighlight();
            }
        }        

        if (other.TryGetComponent<Cookbook>(out Cookbook cookbook))
        {
            if (!cookbook.isDestroyed)
            {
                cookbook.PlayerLeftBook();
            }
        }

        if(other.TryGetComponent<OrderManager>(out OrderManager orderManager))
        {
            orderManager.orderWindow.GetComponent<CanvasGroup>().alpha = 0;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        //Honestly this is the only non-annoying way I could figure to get the cookbook assigned. Sue me.
        if (!cookbook && other.TryGetComponent<Cookbook>(out Cookbook cb))
        {
            cookbook = cb;
        }
    }

    private void UpdateInteractionIcon() 
    {
        interactButtonIcon.gameObject.SetActive(interactionText.text == "" ? false : true);
    }

    private void AssignHighlightColor()
    {
        //Player 1
        if (GameManager.numberOfPlayers == 1)
        {
            highlightColor = Color.blue;
        }
        //Player 2
        else if (GameManager.numberOfPlayers == 2)
        {
            highlightColor = Color.green;
        }
        else { Debug.Log($"Something is wrong with GameManager.numberOfPlayers! It is not 1 or 2, but {GameManager.numberOfPlayers}."); }

    }

    public void OnKnifeThrow()
    {
        if ((inventory[0] == null || inventory[1] == null) && readyToThrow)
        {
            readyToThrow = false;

            // instantiate object to throw
            GameObject projectile = Instantiate(knifePrefab, knifeSpawnTransform.position, transform.rotation);

            // get rigidbody component
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            // calculate direction
            KnifeScript kscript = projectile.GetComponent<KnifeScript>();

            kscript.forward = transform.forward;

            // implement throwCooldown
            Invoke("ResetThrow", throwCooldown);
        }
    }

    public void OnHelp()
    {

        if (helpAvailable)
        {
            helpGuide.SetActive(!helpGuide.activeSelf);

            HelpGuideManager helpGuideManager = FindObjectOfType<HelpGuideManager>();
            for(int i = 0; i < helpGuideManager.helpGuides.Length; i++)
            {
                if (helpGuideManager.helpGuides[i].Name == nameOfGuide)
                {
                    helpGuideManager.videoPlayer.clip = helpGuideManager.helpGuides[i].clip;
                    helpGuideManager.title.text = helpGuideManager.helpGuides[i].Name;
                    helpGuideManager.content.text = helpGuideManager.helpGuides[i].content;
                }
            }
        }

        if (!helpAvailable && helpGuide.activeSelf == true)
        {
            helpGuide.SetActive(false);
        }
    }

    public void HelpIndicator(bool popUp = false, string guideName = "")
    {
        popOutAnimator.SetBool("Open", popUp);
        helpAvailable = popUp;
        nameOfGuide = guideName;
    }

    public void ResetThrow()
    {
        readyToThrow = true;
    }

    /// <summary>
    /// Method from inputs which calls a method in the cookbook object which calls a method
    /// in the cookbook UI. This is because the player has the controls, and only a reference
    /// to the cookbook object, which has a reference to the cookbook UI. Fucking stupid I know.
    /// </summary>
    public void OnNextRecipe()
    {
        if (cookbook)
        {
            cookbook.OnNextRecipe();
        }
        
    }

    /// <summary>
    /// Method from inputs which calls a method in the cookbook object which calls a method
    /// in the cookbook UI. This is because the player has the controls, and only a reference
    /// to the cookbook object, which has a reference to the cookbook UI. Fucking stupid I know.
    /// </summary>
    public void OnPreviousRecipe()
    {
        if (cookbook)
        {
            cookbook.OnPreviousRecipe();
        }
    }
}
