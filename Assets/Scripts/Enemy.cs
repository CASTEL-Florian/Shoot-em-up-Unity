using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterMovement controller;
    [SerializeField] private Shooting shooting;
    [SerializeField] private EnemyAvatar avatar;
    [SerializeField] private AnimationCurve horizontalMovement;
    [SerializeField] private float minX = -10;
    private float speed;
    private float time = 0.5f;
    [SerializeField] private Vector2 minMaxhorizontalMovementDuration = new Vector2(1,1);
    [SerializeField] private Vector2 minMaxhorizontalMovementMagnitude = new Vector2(2,2);
    [SerializeField] private GameObject deathEffect;
    private float horizontalMovementDuration = 1;
    private float horizontalMovementMagnitude = 2;


    private void OnEnable()
    {
        shooting.SetActive(true);
        Vector2 minMaxSpeed = avatar.GetMinMaxSpeed();
        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        horizontalMovementDuration = Random.Range(minMaxhorizontalMovementDuration.x, minMaxhorizontalMovementDuration.y);
        horizontalMovementMagnitude = Random.Range(minMaxhorizontalMovementMagnitude.x, minMaxhorizontalMovementMagnitude.y);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime/horizontalMovementDuration;
        if (time > 1)
            time = 0;
        controller.Move(speed * transform.right + horizontalMovement.Evaluate(time) * horizontalMovementMagnitude * transform.up);   
       
        if (transform.position.x < minX)
        {
            avatar.Death(false);
        }
    }
}
