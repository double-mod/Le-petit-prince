using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMove : MonoBehaviour
{
    public float distance;

    public float speed = 1;

    private float theta = 0;

    private void move()
    {
        var radius = theta * speed / 180 * Mathf.PI;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + distance * Mathf.Sin(radius) * Time.deltaTime);
        if ((++theta) * speed > 360)
            theta = 0;
        transform.position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
