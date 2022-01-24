public class BouncingPlatform : TriggerOnPlayer
{
    public BouncingPlatformData data = null;

    protected override void OnPlayerTrigger(Player player)
    {
        player.playerController.JumpWithOverrideForce(data.bouncingForce);
    }
}
