using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private float length;
    private float startPos;
    private bool doubleSpeed = false;
    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Update()
    {
        float x = transform.position.x - (speed * (doubleSpeed ? 2:1)) * Time.deltaTime;
        if (startPos - x >= length)
            x += length;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void ToggleDoubleSpeed()
    {
        doubleSpeed = !doubleSpeed;
    }
}
