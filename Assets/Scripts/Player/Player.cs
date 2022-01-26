using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController = null;
    public IAController iaController = null;

    public PlayerControllerData playerData = null;
    public PlayerControllerData iaData = null;

    public HealthController healthController = null;

    public Vector3 LastCheckpoint { get; set; }

    private void Awake()
    {
        LastCheckpoint = transform.position;
        LevelManager.Instance.OnReloadLevelAction += RespawnLastCheckpoint;
        healthController = new HealthController(playerData.healthData);
        healthController.OnDeathAction += LevelManager.Instance.ReloadLevel;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnReloadLevelAction -= RespawnLastCheckpoint;
        healthController.OnDeathAction -= LevelManager.Instance.ReloadLevel;
    }

    public void TriggerAltar()
    {
        GameManager.Instance.ChangeState(GameManager.EGameState.GOD_MODE);
        playerController.SetHasControl(false);
        playerController.controllerData = iaData;
        iaController.SetActive(true);
    }

    public void TriggerSafeZone()
    {
        GameManager.Instance.ChangeState(GameManager.EGameState.PLATEFORMER);
        playerController.SetHasControl(true);
        playerController.controllerData = playerData;
        iaController.SetActive(false);
    }

    public void RespawnLastCheckpoint()
    {
        transform.position = LastCheckpoint;
        healthController.RefillLife();
    }
}