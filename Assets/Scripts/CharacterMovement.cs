using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    public void Move(Vector2 move)
    {
        Vector3 targetVelocity = move;
        rb.velocity = move;
    }

    public IEnumerator Dodge(Vector2 direction, float speed, float duration)
    {
        rb.velocity = direction * speed;
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector2.zero;
    }

    public void Lerp(Vector2 from, Vector2 to, float t)
    {
        rb.position = Vector2.Lerp(from, to, t);
    }

}
