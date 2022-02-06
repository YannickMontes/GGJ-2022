using UnityEngine;

[ExecuteInEditMode]
public class CheatManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Player player = FindObjectOfType<Player>();
            if(player != null)
            {
                player.transform.position = Utils.GetScreenPosFromMouse();
            }
        }
    }
}
