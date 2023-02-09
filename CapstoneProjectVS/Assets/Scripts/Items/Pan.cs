public class Pan : Tool
{
    public override void Interact(Item itemInMainHand)
    {
        switch (itemInMainHand.Name)
        {
            case "spatula":
                break;
            case "egg":
                break;
            case "bacon":
                break;
            case "butter":
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
