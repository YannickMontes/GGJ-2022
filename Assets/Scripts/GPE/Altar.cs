using System.Collections;
using UnityEngine;

public class Altar : TriggerOnPlayer
{
    public Transform playerPos = null;
    public float timeBeforeGodMode = 2.0f;
    public float timeToMoveToPosition = 1.5f;

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
            Execute(player);
        }
        else if (triggerBehavior == ETriggerBehavior.INPUT)
        {
            isClose = true;
        }
    }

    private void Execute(Player player)
    {
        player.playerController.SetHasControl(false);
        StartCoroutine(MovePlayerToPosition(player));
    }

    private IEnumerator MovePlayerToPosition(Player player)
    {
        float elapsedTime = 0.0f;
        Vector3 beginPos = player.transform.position;
        while(elapsedTime <= timeToMoveToPosition)
        {
            Vector3 pos = Vector3.Lerp(beginPos, playerPos.position, elapsedTime / timeToMoveToPosition);
            player.transform.position = pos;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        player.playerController.animator.SetBool("IsPraying", true);
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
