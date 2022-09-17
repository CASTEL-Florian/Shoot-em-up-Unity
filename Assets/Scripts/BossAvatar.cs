using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAvatar : Avatar
{
    [SerializeField] private int score = 500;
    public void Death()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(score);
        GameManager.Instance.EnemyDead();
    }
}
