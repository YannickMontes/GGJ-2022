using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject plateformerUI = null;
    public GameObject godModeUI = null;
    public DialogManagerUI dialogManagerUI = null;
    public Image cursorImage = null;

    public void ChangeCursorSprite(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.enabled = sprite != null;
    }

    public void LaunchDialog(DialogueInfosData dialog)
    {
        dialogManagerUI.gameObject.SetActive(true);
        dialogManagerUI.LaunchDialog(dialog);
        dialogManagerUI.OnDialogEnd += OnDialogEnd;
    }

    private void Start()
    {
        Cursor.visible = false;
        GameManager.Instance.OnChangeStateAction += OnGameManagerStateChanged;
        OnGameManagerStateChanged(GameManager.Instance.CurrentState);
        dialogManagerUI.gameObject.SetActive(false);
    }

    private void OnGameManagerStateChanged(GameManager.EGameState newState)
    {
        if(newState == GameManager.EGameState.GOD_MODE)
        {
            plateformerUI.SetActive(false);
            godModeUI.SetActive(true);
            cursorImage.enabled = cursorImage.sprite != null;
        }
        else if(newState == GameManager.EGameState.PLATEFORMER)
        {
            plateformerUI.SetActive(true);
            godModeUI.SetActive(false);
            cursorImage.enabled = false;
        }
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentState == GameManager.EGameState.GOD_MODE)
        {
            cursorImage.transform.position = Input.mousePosition;
        }
    }

    private void OnDialogEnd()
    {
        dialogManagerUI.gameObject.SetActive(false);
    }
}
