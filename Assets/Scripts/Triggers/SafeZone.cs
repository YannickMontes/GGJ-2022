public class SafeZone : TriggerOnPlayer
{
    protected override void OnPlayerTrigger(Player player)
    {
        player.TriggerSafeZone();
    }
}
