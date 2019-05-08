using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(BoxCollider2D))]

public class movePattern : MonoBehaviour
{
    public enum Pattern
    {
        upDown,
        leftRight,
        breakable
    }

    public Pattern pattern;

    public float spd;

    public float distance;

    private Rigidbody2D myrigidbody2D;

    private bool shiftFlg = false;

    private Vector2 center;

    // Start is called before the first frame update
    void Start()
    {
        center = new Vector2(transform.position.x,transform.position.y);
        myrigidbody2D = GetComponent<Rigidbody2D>();
        myrigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        switch(pattern)
        {
            case Pattern.upDown:
                verticalMov();
                break;
            case Pattern.leftRight:
                horizontalMov();
                break;
            case Pattern.breakable:
                durationTest();
                break;
        }
        distanceCheck();
    }

    private void durationTest()
    {
        
    }

    private void horizontalMov()
    {
        Vector2 moveDistance = new Vector2(Time.deltaTime * spd, 0f);
        transform.position = new Vector2(transform.position.x + moveDistance.x, transform.position.y + moveDistance.y);

        if (Mathf.Abs(transform.position.x - center.x) > distance && distance != 0)
            shiftFlg = !shiftFlg;

    }

    private void verticalMov()
    {
        Vector2 moveDistance = new Vector2(0f, Time.deltaTime * spd);
        transform.position = new Vector2(transform.position.x + moveDistance.x, transform.position.y + moveDistance.y);

        if (Mathf.Abs(transform.position.y - center.y) > distance&&distance!=0)
            shiftFlg = !shiftFlg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("foreground")))
        {
            spd = -spd;
        }
           
    }

    private void distanceCheck()
    {
        if(shiftFlg)
        {
            spd = -spd;
            shiftFlg = !shiftFlg;
        }
    }
}
