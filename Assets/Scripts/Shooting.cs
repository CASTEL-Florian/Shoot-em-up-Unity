using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{

    [SerializeField] private List<Transform> firePoints;
    [SerializeField] private string bulletName;
    [SerializeField] private EnemyAvatar avatar;
    [SerializeField] private float bulletVelocity = 80f;
    [SerializeField] private float shotEnergyCost = 0;
    [SerializeField] private float rateOverTime;
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float angleBetweenShots = 0;
    [SerializeField] private float numberOfShotsCircle = 1;
    [SerializeField] private bool shootInNegativeAngle = false;
    private float angle = 0;


    private float clock = 0;
    private bool active = false;
    private void OnEnable()
    {
        if (avatar)
        {
            Vector2 minMaxShootRate = avatar.GetMinMaxShootRate();
            rateOverTime = Random.Range(minMaxShootRate.x, minMaxShootRate.y);
        }
    }
    private void Update()
    {
        clock -= Time.deltaTime;
        if (clock < 0 && active && (!energyManager || !energyManager.isFullReloading()))
        {
            clock = 1 / rateOverTime;
            Shoot();
        }

    }

    private void Shoot()
    {
        if (energyManager)
            energyManager.UseEnergy(shotEnergyCost);
        if (audioSource)
            audioSource.Play();
        float multipleShotsAngle = 360 / numberOfShotsCircle;
        for (int i = 0; i < firePoints.Count; i++)
        {
            for (int j = 0; j < numberOfShotsCircle; j++)
            {
                Quaternion rot = firePoints[i].rotation;
                rot = Quaternion.Euler(0, -0, angle + j * multipleShotsAngle) * rot;
                Rigidbody2D rb = ObjectPooler.Instance.Spawn(bulletName, firePoints[i].position, rot).GetComponent<Rigidbody2D>();
                rb.velocity = bulletVelocity * rb.transform.right;
                rb.gameObject.GetComponent<Projectile>().SetTag(gameObject.tag);
                if (shootInNegativeAngle)
                {
                    rot = firePoints[i].rotation;
                    rot = Quaternion.Euler(0, -0, -angle + j * multipleShotsAngle) * rot;
                    rb = ObjectPooler.Instance.Spawn(bulletName, firePoints[i].position, rot).GetComponent<Rigidbody2D>();
                    rb.velocity = bulletVelocity * rb.transform.right;
                    rb.gameObject.GetComponent<Projectile>().SetTag(gameObject.tag);
                }
            }
        }
        angle += angleBetweenShots;
    }

    public void SetActive(bool value)
    {
        active = value;
    }

}
