public class RespawnTrigger : TriggerOnPlayer
{
    protected override void OnPlayerTrigger(Player player)
    {
        LevelManager.Instance.ReloadLevel();
    }
}
