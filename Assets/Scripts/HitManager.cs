using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField] private UnitHealth health;
    [SerializeField] private float invinsibilityTime = 0f;
    private bool isInvinsible = false;

    public void Hit(int value)
    {
        if (isInvinsible)
            return;
        health.TakeDamage(value);
        if (value > 0 && gameObject.active)
            StartCoroutine(SetInvisible(invinsibilityTime));
    }

    public IEnumerator SetInvisible(float duration)
    {
        isInvinsible = true;
        yield return new WaitForSeconds(duration);
        isInvinsible = false;
    }
}
