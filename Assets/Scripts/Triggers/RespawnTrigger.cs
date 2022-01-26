public class RespawnTrigger : TriggerOnPlayer
{
    protected override void OnPlayerTriggerEnter(Player player)
    {
        LevelManager.Instance.ReloadLevel();
    }
}
