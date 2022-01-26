using System;
using UnityEngine;

public class HealthController
{
    public Action OnDeathAction = null;
    public Action<bool> OnInvincibilityChange = null;

    public bool IsDead { get { return life <= 0; } }

    private int life = 0;
    private bool isInvincible = false;
    private float timeSinceInvincible = 0.0f;
    private HealthControllerData healthData = null;

    public HealthController(HealthControllerData data)
    {
        healthData = data;
        life = healthData.maxLife;
    }

    public void RefillLife()
    {
        this.life = healthData.maxLife;
    }

    public void TakeDamage(int damages)
    {
        if (isInvincible)
            return;

        life = Mathf.Clamp(life - damages, 0, healthData.maxLife);
        if(IsDead)
        {
            OnDeathAction?.Invoke();
        }
        else
        {
            isInvincible = true;
            timeSinceInvincible = 0.0f;
            OnInvincibilityChange?.Invoke(true);
        }
    }

    public void Update(float deltaTime)
    {
        if(isInvincible)
        {
            timeSinceInvincible += deltaTime;
            if(timeSinceInvincible >= healthData.invincibilityTime)
            {
                isInvincible = false;
                timeSinceInvincible = 0.0f;
                OnInvincibilityChange?.Invoke(false);
            }
        }
    }
}
