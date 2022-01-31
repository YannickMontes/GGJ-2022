using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public EventInstance? StartEvent(EventReference evt, Transform transform = null)
    {
        if (evt.IsNull)
            return null;

        EventInstance evtInstance = RuntimeManager.CreateInstance(evt);

        if(transform != null)
        {
            RuntimeManager.AttachInstanceToGameObject(evtInstance, transform);
        }

        evtInstance.start();
        return evtInstance;
    }

    public void StopEvent(EventInstance evtInstance, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        evtInstance.stop(stopMode);
    }

    public void SetGlobalParameter(string paramName, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(paramName, value);
    }
}
