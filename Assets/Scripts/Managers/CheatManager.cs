#if UNITY_EDITOR || DEVELOPPEMENT_BUILD
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CheatManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneView.duringSceneGui += view =>
        {
            var e = Event.current;
            if (e != null)
            {
                if (e.keyCode == KeyCode.F4)
                    MovePlayerToCursor(view);
            }
        };
    }

    private void MovePlayerToCursor(SceneView scene)
    {
        Debug.Log("lol");
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            Vector3 mousePosition = Event.current.mousePosition;
            mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
            mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            Debug.Log(mousePosition);
            player.transform.position = mousePosition;
        }
    }
}
#endif