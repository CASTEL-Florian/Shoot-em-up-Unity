using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] float dashEnergy = 0.3f;
    private bool isDashing = false;
    public void Move(Vector2 move)
    {
        Vector3 targetVelocity = move;
        rb.velocity = move;
    }

    public IEnumerator Dodge(float speed, float duration)
    {
        if (rb.velocity.magnitude > 0 && !energyManager.isFullReloading())
        {
            energyManager.UseEnergy(dashEnergy);
            isDashing = true;
            rb.velocity = rb.velocity.normalized * speed;
            yield return new WaitForSeconds(duration);
            rb.velocity = Vector2.zero;
            isDashing = false;
        }
    }

    public void Lerp(Vector2 from, Vector2 to, float t)
    {
        rb.position = Vector2.Lerp(from, to, t);
    }
    public bool IsDashing()
    {
        return isDashing;
    }
}
