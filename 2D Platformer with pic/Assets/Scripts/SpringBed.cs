﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBed : MonoBehaviour
{
    public float boundRate=2f;
    public float maxVelocity_x=20;
    public float maxVelocity_y=20;

    private Rigidbody2D myRigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myRigidbody2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Vector2 velocity = new Vector2(-collision.gameObject.GetComponent<Player>().GetPrevVelocity().x, -collision.gameObject.GetComponent<Player>().GetPrevVelocity().y);
            // var velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (velocity.x * boundRate < maxVelocity_x)
                velocity.x *= boundRate;
            else
                velocity.x = maxVelocity_x;

            if (velocity.y * boundRate < maxVelocity_y)
                velocity.y *= boundRate;
            else
                velocity.y = maxVelocity_y;

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * boundRate, velocity.y * boundRate);
            //StartCoroutine(Bound(collision));
        }
    }
    IEnumerator Bound(Collision2D collision)
    {
        Vector2 velocity= new Vector2(-collision.gameObject.GetComponent<Player>().GetPrevVelocity().x, -collision.gameObject.GetComponent<Player>().GetPrevVelocity().y);
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = velocity.normalized;
        yield return new WaitForSeconds(0.1f);
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (velocity.x*boundRate,velocity.y*boundRate);
    }
}
