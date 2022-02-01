using System.Collections;
using UnityEngine;

public class Altar : TriggerOnPlayer
{
    public Transform playerPos = null;
    public float timeBeforeGodMode = 2.0f;

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
            StartCoroutine(TriggerAfterTime(player));
        }
        else if (triggerBehavior == ETriggerBehavior.INPUT)
        {
            isClose = true;
        }
    }

    private IEnumerator TriggerAfterTime(Player player)
    {
        player.playerController.SetHasControl(false);
        player.playerController.animator.SetBool("IsPraying", true);
        player.transform.position = playerPos.position;
        yield return new WaitForSeconds(timeBeforeGodMode);
        player.playerController.animator.SetBool("IsPraying", false);
        player.TriggerAltar();
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
