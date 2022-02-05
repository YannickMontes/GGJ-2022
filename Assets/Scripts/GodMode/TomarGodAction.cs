public class TomarGodAction : GodActionWithData<TomarGodActionData>
{
    public override void SetActive(bool active)
    {
        base.SetActive(active);
        UIManager.Instance?.ChangeCursorSprite(active ? Data.cursorSprite : null);
    }

    public override bool CanExecute()
    {
        if(base.CanExecute())
        {
            return Utils.GetBreakableWallFromMousePos() != null;
        }
        return false;
    }
    protected override void Execute()
    {
        BreakableWall breakableWall = Utils.GetBreakableWallFromMousePos();
        breakableWall.Container.ApplyDamages(Data.damageByAction);
    }
}
