using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : Item, ICollectable, ICookable
{

    public enum Status { clean, dirty}
    [Header("Status")]
    public Status status;

    [Header("Variables")]
    public bool isDirty;
    public bool isHot;
    public int useBeforeDirty;
    public int timesUsed;
    public int containerSize;
    [Header("Models")]
    public GameObject cleanModel;
    public GameObject dirtyModel;

    [Header("Sprites")]
    public Sprite clean;
    public Sprite dirty;

    public virtual void isDirtied()
    {
        timesUsed += 1;

        if(timesUsed >= useBeforeDirty)
        {
            isDirty = true;
        }
    }

    public virtual void SwitchModel(bool dirty)
    {
        if (isDirty)
        {
            cleanModel.SetActive(false);
            dirtyModel.SetActive(true);
        }
        else
        {
            dirtyModel.SetActive(false);
            cleanModel.SetActive(true);
        }
    }
    public virtual void Collect(PlayerController player)
    {
        //check to see which inventory is empty
        if (player.inventory[0] == null)
        {
            player.inventory[0] = this;
        }
        else
        {
            player.inventory[1] = this;
        }

        //temporary
        gameObject.SetActive(false);
        Debug.Log("Inventory 1: " + player.inventory[0] + " Inventory 2: " + player.inventory[1]);

    }

    public void CookingCheck(GameObject cookingCheck, float cookTime)
    {
        // reset everything
        cookingCheck.SetActive(true); // display cooking check
        CookingCheckScript cookingCheckScript = cookingCheck.GetComponent<CookingCheckScript>(); // get cooking script
        foreach (Image img in cookingCheckScript.img)
        {
            img.gameObject.SetActive(false); // disable check images
        }
        cookingCheckScript.progressSlider.value = 0; // value equal zero
        StartCoroutine(Timer(cookTime, 0,0,cookingCheckScript.progressSlider));
    }
    public IEnumerator Timer(float cookTime, float progressMeter, float progressMeterMax, Slider progressSlider)
    {
        float deltaTime = Time.unscaledTime;

        while (progressMeter < progressMeterMax)
        {
            progressMeter = (Time.unscaledTime - deltaTime) / cookTime;
            progressSlider.value = progressMeter;
            yield return null;
        }
    }
}
