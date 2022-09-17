using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : Avatar
{
    public void Death()
    {
        Destroy(gameObject);
        GameManager.Instance.PlayerDead();
    }

    public void GetHit()
    {
        ScoreManager.Instance.ResetCombo();
    }
}
