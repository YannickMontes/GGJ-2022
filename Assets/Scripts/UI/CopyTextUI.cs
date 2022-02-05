using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class CopyTextUI : MonoBehaviour
{
    public TextMeshProUGUI originText = null;
    public TextMeshProUGUI destinationText = null;

    private void OnEnable()
    {
        if (originText != null)
            destinationText.text = originText.text;
    }

    private void OnDisable()
    {
        destinationText.text = "";
    }

    void Update()
    {
        if (originText != null)
            destinationText.text = originText.text;
    }
}
