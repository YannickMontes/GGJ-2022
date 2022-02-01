using UnityEngine;

public class AnimEventCatcher : MonoBehaviour
{
    public GameObject toCall = null;

    public void ReceiveEvent(string functionName)
    {
        toCall.SendMessage(functionName);
    }
}