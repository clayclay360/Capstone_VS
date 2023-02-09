using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : Tool
{
    public override void Interact(Item itemInMainHand)
    {
        switch (itemInMainHand.Name)
        {
            case "pan":
                break;
            default:
                Collect();
                break;
        }
    }

    public override void Collect()
    {

    }
}
