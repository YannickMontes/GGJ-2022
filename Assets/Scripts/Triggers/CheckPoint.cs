public class CheckPoint : TriggerOnPlayer
{
    protected override void OnPlayerTriggerEnter(Player player)
    {
        player.LastCheckpoint = transform.position;
    }
}
