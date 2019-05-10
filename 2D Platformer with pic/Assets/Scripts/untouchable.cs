using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class untouchable : MonoBehaviour
{
    Rigidbody2D myRigidBody2D;
    BoxCollider2D myCollider2D;
    SpriteRenderer mySpriteRenderer;

    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt <= 0)
            myCollider2D.isTrigger = true;
        Debug.Log(cnt);
        if (mySpriteRenderer.color.a >= 0)
            mySpriteRenderer.color = new Vector4(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, mySpriteRenderer.color.a - 1f * Time.deltaTime);
        //move();
    }

    private void OnTriggerEnter2D()
    {
        if (myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("star")))
        {
            myCollider2D.isTrigger = false;
            cnt++;
            StartCoroutine(go());
        }
    }

    private void OnTriggerStay2D()
    {
        if (myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("star"))　&& mySpriteRenderer.color.a<1.0f)
            mySpriteRenderer.color = new Vector4(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, mySpriteRenderer.color.a + 0.5f * Time.deltaTime);
    }

    private void move()
    {
        if(cnt>0)
             transform.position = new Vector3(transform.position.x - 0.5f*Time.deltaTime, transform.position.y, transform.position.y);
    }

    IEnumerator go()
    {
        yield return new WaitForSeconds(2f);
        cnt--;
    }
}
