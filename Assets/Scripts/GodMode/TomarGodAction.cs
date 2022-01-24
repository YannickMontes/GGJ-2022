public class TomarGodAction : GodActionWithData<TomarGodActionData>
{
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
