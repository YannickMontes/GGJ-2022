public class BouncingPlatform : TriggerOnPlayer
{
    public BouncingPlatformData data = null;

    protected override void OnPlayerTriggerEnter(Player player)
    {
        player.playerController.JumpWithOverrideForce(data.bouncingForce);
    }
}
