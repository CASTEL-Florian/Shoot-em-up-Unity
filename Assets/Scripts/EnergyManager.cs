using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private float energyReloadTime = 2f;
    [SerializeField] private float energyfullReloadTime = 2.5f;
    private EnergyBar energyBar;
    private bool fullReloading;
    private float currentEnergy;

    private void Start()
    {
        energyBar = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<EnergyBar>();
        currentEnergy = 1;
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            fullReloading = true;
        }
    }

    private void Update()
    {
        if (currentEnergy < 1)
        {
            currentEnergy += Time.deltaTime / (fullReloading ? energyfullReloadTime : energyReloadTime);
            if (currentEnergy >= 1)
                fullReloading = false;
        }
        energyBar.UpdateBar(currentEnergy, fullReloading);
    }

    public bool isFullReloading()
    {
        return fullReloading;
    }
}
