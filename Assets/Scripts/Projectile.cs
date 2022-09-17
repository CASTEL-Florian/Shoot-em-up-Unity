using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;
    private bool active = true;
    private float time = 0;
    [SerializeField] private float maxX = Mathf.Infinity;
    [SerializeField] private float maxY = Mathf.Infinity;
    private string sourceTag = "";

    private void OnEnable()
    {
        time = 0;
        active = true;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > lifeTime || Mathf.Abs(transform.position.x) > maxX ||Mathf.Abs(transform.position.y) > maxY)
        {
            active = false;
            ObjectPooler.Instance.ReturnObject("bullet", gameObject);
        }
    }

    public void SetTag(string str)
    {
        sourceTag = str;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active || collision.tag == sourceTag)
            return;
        active = false;
        HitManager hitManager = collision.GetComponent<HitManager>();
        if (hitManager)
            hitManager.Hit(1);
        ObjectPooler.Instance.ReturnObject("bullet", gameObject);
    }
}
