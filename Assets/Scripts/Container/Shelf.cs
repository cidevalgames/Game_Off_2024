public class Shelf : Container
{
    protected override bool AddScroll(Scroll scroll)
    {
        if (!base.AddScroll(scroll)) return false;
        scroll.HideCompleteSprite();
        return true;
    }
}
