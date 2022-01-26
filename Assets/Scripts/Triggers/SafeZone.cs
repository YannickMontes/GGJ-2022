public class SafeZone : TriggerOnPlayer
{
    protected override void OnPlayerTriggerEnter(Player player)
    {
        player.TriggerSafeZone();
    }
}
