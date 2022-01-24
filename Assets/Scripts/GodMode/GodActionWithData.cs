using UnityEngine;

//We need this class to reference it without breaking anything, because templates are not serializable
public abstract class GodAction : MonoBehaviour
{
    public Animator animator = null;

    public abstract void SetActive(bool active);
    public abstract void TryExecute();
    public abstract bool CanExecute();
    protected abstract void Execute();
}

public abstract class GodActionWithData<T> : GodAction where T : GodActionData
{
    [SerializeField]
    private T actionData = null;

    protected T Data { get { return actionData; } }

    public bool IsActive { get; private set; } = false;

    public bool IsOnCooldown { get { return timeLeftForNextUse > 0; } }

    private float timeLeftForNextUse = 0.0f; 

    public override void SetActive(bool active)
    {
        IsActive = active;
    }

    public override void TryExecute()
    {
        if(CanExecute())
        {
            timeLeftForNextUse = Data.cooldown;
            Execute();
        }
    }

    public override bool CanExecute()
    {
        return !IsOnCooldown;
    }

    protected virtual void Update()
    {
        if(IsActive)
            timeLeftForNextUse -= Time.deltaTime;
    }
}
