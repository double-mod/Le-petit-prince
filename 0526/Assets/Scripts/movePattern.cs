using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]

//[RequireComponent(typeof(BoxCollider2D))]

public class movePattern : MonoBehaviour
{
    public enum Pattern
    {
        upDown,
        leftRight,
        up,
        down
    }

    public Pattern pattern;

    public float spd;

    public float distance;

    public GameObject followblock=null;

    private Rigidbody2D myrigidbody2D;

    private bool shiftFlg = false;

    private Vector2 center;

    // Start is called before the first frame update
    void Start()
    {
        center = new Vector2(transform.position.x,transform.position.y);
        if (followblock!=null)prev = followblock.transform.position;
        //myrigidbody2D = GetComponent<Rigidbody2D>();
        //myrigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        distanceCheck();
        switch (pattern)
        {
            case Pattern.upDown:
                verticalMov();
                break;
            case Pattern.leftRight:
                horizontalMov();
                break;
            case Pattern.down:
                downMov();
                break;
            case Pattern.up:
                upMov();
                break;
        }
        FollowBlock();
    }

    private void durationTest()
    {
        
    }

    private void horizontalMov()
    {
        Vector2 moveDistance = new Vector2(Time.deltaTime * spd, 0f);
        transform.position = new Vector2(transform.position.x + moveDistance.x, transform.position.y + moveDistance.y);

        if (Mathf.Abs(transform.position.x - center.x) >= distance && distance != 0)
            shiftFlg = !shiftFlg;

    }

    private void verticalMov()
    {
        Vector2 moveDistance = new Vector2(0f, Time.deltaTime * spd);
        transform.position = new Vector2(transform.position.x + moveDistance.x, transform.position.y + moveDistance.y);

        if (Mathf.Abs(transform.position.y - center.y) >= distance&&distance!=0)
            shiftFlg = !shiftFlg;
    }

    private void upMov()
    {
        if (Mathf.Abs(transform.position.y - center.y) < distance )
        {
            Vector2 moveDistance = new Vector2(0f, Time.deltaTime * spd);
            transform.position = new Vector2(transform.position.x + moveDistance.x, transform.position.y + moveDistance.y);
        }
    }

    private void downMov()
    {
        if (Mathf.Abs(transform.position.y - center.y) < distance)
        {
            Vector2 moveDistance = new Vector2(0f, Time.deltaTime * spd);
            transform.position = new Vector2(transform.position.x - moveDistance.x, transform.position.y - moveDistance.y);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("foreground")))
    //    {
    //        spd = -spd;
    //    }

    //}

    private Vector3 prev=new Vector3(0,0,0);
    private void FollowBlock()
    {
        if(followblock!=null)
        {
            this.transform.position = new Vector2(this.transform.position.x-(followblock.transform.position.x-prev.x), this.transform.position.y - (followblock.transform.position.y - prev.y));
            prev = followblock.transform.position;
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
