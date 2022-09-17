using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Avatar : MonoBehaviour
{
    [SerializeField] int maxHealth = 1;
    [SerializeField] protected float speed = 300;
    [SerializeField] private GameObject deathEffect;


    public float GetSpeed()
    {
        return speed;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void InstantiateDeathEffect()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }

}
