using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public List<GameAction> onLevelStart = new List<GameAction>();

    private void Start()
    {
        onLevelStart?.Execute(this);
    }

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
