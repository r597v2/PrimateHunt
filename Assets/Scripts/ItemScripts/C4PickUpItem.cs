public class C4PickUpItem : PickUpItem
{
    public bool armed;

    protected override void Interact(int photonViewID)
    {
        armed = false;
        base.Interact(photonViewID);
    }
}