using UnityEngine;

public abstract class TriggerOnPlayer : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.transform.root.GetComponentInChildren<Player>();
        if (player != null)
        {
            OnPlayerTrigger(player);
        }
    }

    protected abstract void OnPlayerTrigger(Player player);
}
