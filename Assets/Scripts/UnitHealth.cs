using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private UnityEvent damageEvent;
    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private Avatar avatar;
    [SerializeField] private bool useHealthBar;
    [SerializeField] private string healthBarTag = "HealthBar";
    private int currentHealth;
    private bool alive = true;
    private int maxHealth;
    private EnergyBar healthBar;
   

    private void OnEnable()
    {
        alive = true;
        maxHealth = avatar.GetMaxHealth();
        currentHealth = maxHealth;
        if (useHealthBar)
        {
            healthBar = GameObject.FindGameObjectWithTag(healthBarTag).GetComponent<EnergyBar>();
        }
    }

    public void TakeDamage(int value)
    {
        if (!alive)
            return;
        currentHealth -= value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        damageEvent.Invoke();
        if (currentHealth <= 0)
        {
            alive = false;
            deathEvent.Invoke();
        }
        if (healthBar)
            healthBar.UpdateBar((float)currentHealth / maxHealth, false);
    }


    public bool isAlive()
    {
        return alive;
    }

    

    public void SetMaxHealth(int value) 
    {
        maxHealth = value;
        currentHealth = value;
    }
}
