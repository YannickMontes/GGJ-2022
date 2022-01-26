using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : TriggerOnPlayer
{
    public enum EState { IDLE, FALLING, DISABLE }

    public FallingPlatformData fallPlatformData = null;

    public List<Collider2D> colliders = new List<Collider2D>();
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    private Vector3 startPosition = Vector3.zero;
    private EState currentState = EState.IDLE;

    private void Awake()
    {
        startPosition = transform.position;
        LevelManager.Instance.OnReloadLevelAction += Respawn;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnReloadLevelAction -= Respawn;
    }

    protected override void OnPlayerTrigger(Player player)
    {
        if(currentState == EState.IDLE)
        {
            currentState = EState.FALLING;
            Invoke("Fall", fallPlatformData.timeBeforeFall);
        }
    }

    private void Fall()
    {
        StartCoroutine(FallTreatment());
    }

    private IEnumerator FallTreatment()
    {
        float elapsedTime = 0.0f;
        Vector3 endPos = new Vector3(startPosition.x, startPosition.y - fallPlatformData.fallDistance, startPosition.z);
        while (elapsedTime <= fallPlatformData.fallTime)
        {
            float time = elapsedTime / fallPlatformData.fallTime;
            transform.position = Vector3.Lerp(startPosition, endPos, time);
            float alpha = Mathf.Lerp(1f, 0f, time);
            ApplyAlphaToRenderers(alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        ApplyAlphaToRenderers(0.0f);
        EnableColliders(false);
        currentState = EState.DISABLE;
        if (fallPlatformData.willRespawn)
            Invoke("Respawn", fallPlatformData.respawnTime);
    }

    private IEnumerator RespawnCoroutine()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime <= fallPlatformData.respawnAlphaTime)
        {
            float time = elapsedTime / fallPlatformData.respawnAlphaTime;
            float alpha = Mathf.Lerp(0f, 1f, time);
            ApplyAlphaToRenderers(alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        ApplyAlphaToRenderers(1.0f);
    }

    public void Respawn()
    {
        CancelInvoke();
        currentState = EState.IDLE;
        StopAllCoroutines();
        StartCoroutine(RespawnCoroutine());
        EnableColliders(true);
        ApplyAlphaToRenderers(1.0f);
        transform.position = startPosition;
    }

    private void ApplyAlphaToRenderers(float alpha)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }

    private void EnableColliders(bool enable)
    {
        foreach (Collider2D collider2D in colliders)
        {
            collider2D.enabled = enable;
        }
    }
}
