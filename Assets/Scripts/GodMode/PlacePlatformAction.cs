using UnityEngine;

public class PlacePlatformAction : GodActionWithData<PlacePlatformActionData>
{
    protected PreviewPlatform previewObject = null;

    private void Awake()
    {
        GameObject instance = Instantiate(Data.previewObject);
        previewObject = instance.GetComponentInChildren<PreviewPlatform>();
        previewObject.transform.position = Vector3.zero;
        previewObject.gameObject.SetActive(false);
    }

    public override void SetActive(bool active)
    {
        base.SetActive(active);
        previewObject?.gameObject?.SetActive(active);
    }

    public override bool CanExecute()
    {
        if(base.CanExecute())
        {
            MovePreviewObjectToMousePos();
            return !previewObject.IsCollidingGround;
        }
        return false;
    }

    protected override void Execute()
    {
        LevelManager.Instance.SpawnDynamicObject(Data.platformToPlace, Utils.GetCellCenterFromMouse(LevelManager.Instance.GroundTilemap));
    }

    protected override void Update()
    {
        base.Update();
        if(IsActive)
        {
            MovePreviewObjectToMousePos();
        }
    }

    protected void MovePreviewObjectToMousePos()
    {
        previewObject.transform.position = Utils.GetCellCenterFromMouse(LevelManager.Instance.GroundTilemap);
    }
}
