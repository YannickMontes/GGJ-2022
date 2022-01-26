
public class Spikes : TriggerOnPlayer
{
    public SpikesData data = null;

    protected override void OnPlayerTriggerEnter(Player player)
    {
        player.healthController.TakeDamage(data.damages);
    }

    private void Update()
    {
        if (PlayerIsStillOnTrigger)
            Player.healthController.TakeDamage(data.damages);
    }
}
