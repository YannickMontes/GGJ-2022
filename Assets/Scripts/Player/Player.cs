using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController = null;
    public IAController iaController = null;

    public PlayerControllerData playerData = null;
    public PlayerControllerData iaData = null;

    public Vector3 LastCheckpoint { get; set; }

    private void Awake()
    {
        LastCheckpoint = transform.position;
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
        LevelManager.Instance.ClearDynamicSpawnObjects();
        transform.position = LastCheckpoint;
    }
}
