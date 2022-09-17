using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : Avatar
{

    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private Vector2 minMaxShootRate;
    [SerializeField] private int score = 10;



    public Vector2 GetMinMaxShootRate()
    {
        return minMaxShootRate;
    }

    public Vector2 GetMinMaxSpeed()
    {
        return minMaxSpeed;
    }

    public void Death(bool killedByPlayer)
    {
        if (killedByPlayer)
            ScoreManager.Instance.AddScore(score);
        ObjectPooler.Instance.ReturnObject("enemy", gameObject);
        GameManager.Instance.EnemyDead();
    }
}
