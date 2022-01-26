using UnityEngine;

public abstract class TriggerOnPlayer : MonoBehaviour 
{
    protected bool PlayerIsStillOnTrigger { get; set; }

    protected Player Player { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.transform.root.GetComponentInChildren<Player>();
        if (player != null)
        {
            Player = player;
            OnPlayerTriggerEnter(player);
            PlayerIsStillOnTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.transform.root.GetComponentInChildren<Player>();
        if (player != null)
        {
            OnPlayerTriggerExit(player);
            PlayerIsStillOnTrigger = false;
            Player = null;
        }
    }

    protected abstract void OnPlayerTriggerEnter(Player player);
    protected virtual void OnPlayerTriggerExit(Player player)
    {

    }
}
