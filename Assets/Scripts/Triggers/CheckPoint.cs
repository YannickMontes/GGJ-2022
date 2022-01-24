public class CheckPoint : TriggerOnPlayer
{
    protected override void OnPlayerTrigger(Player player)
    {
        player.LastCheckpoint = transform.position;
    }
}
