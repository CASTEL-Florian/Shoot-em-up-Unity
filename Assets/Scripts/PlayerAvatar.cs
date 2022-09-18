using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : Avatar
{
    private bool alive = true;
    [SerializeField] private SpriteRenderer rend;
    public void Death()
    {
        GameManager.Instance.PlayerDead();
        rend.enabled = false;
    }

    public void GetHit()
    {
        ScoreManager.Instance.ResetCombo();
    }

    public bool isAlive()
    {
        return alive;
    }
}
