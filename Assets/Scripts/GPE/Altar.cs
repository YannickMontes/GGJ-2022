using UnityEngine;

public class Altar : TriggerOnPlayer
{
    public enum ETriggerBehavior
    {
        INPUT,
        TRIGGER
    }

    public ETriggerBehavior triggerBehavior = ETriggerBehavior.TRIGGER;

    protected bool isClose = true;

    protected override void OnPlayerTriggerEnter(Player player)
    {
        if (triggerBehavior == ETriggerBehavior.TRIGGER)
        {
            player.TriggerAltar();
        }
        else if (triggerBehavior == ETriggerBehavior.INPUT)
        {
            isClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerBehavior == ETriggerBehavior.INPUT)
        {
            Player player = collision.transform.root.GetComponentInChildren<Player>();
            if (player != null)
            {
                isClose = false;
            }
        }
    }
}
