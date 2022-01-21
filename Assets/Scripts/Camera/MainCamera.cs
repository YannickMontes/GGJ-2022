

public class MainCamera : Singleton<MainCamera>
{
    public Cinemachine.CinemachineVirtualCamera playerFollowVCam = null;
    public Cinemachine.CinemachineVirtualCamera godModeCam = null;

    public void Start()
    {
        GameManager.Instance.OnChangeStateAction += OnGameManagerStateChanged;
        PlayerController player = FindObjectOfType<PlayerController>();
        playerFollowVCam.Follow = player.transform;
        godModeCam.Follow = player.transform;
    }

    private void OnGameManagerStateChanged(GameManager.EGameState newState)
    {
        if(newState == GameManager.EGameState.PLATEFORMER)
        {
            playerFollowVCam.gameObject.SetActive(true);
            godModeCam.gameObject.SetActive(false);
        }
        else if(newState == GameManager.EGameState.GOD_MODE)
        {
            playerFollowVCam.gameObject.SetActive(false);
            godModeCam.gameObject.SetActive(true);
        }
    }
}
