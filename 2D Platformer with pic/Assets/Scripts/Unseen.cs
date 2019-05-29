using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unseen : MonoBehaviour
{
    private BoxCollider2D myBoxCollider2D;
    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mySpriteRenderer.color.a >= 0f)
            mySpriteRenderer.color = new Vector4(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, mySpriteRenderer.color.a - 1f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            StartCoroutine(visibleForWhile());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player"))&&collision.gameObject.GetComponent<Player>().getState()=="Walk")
        {
            StartCoroutine(visibleForWhile());
        }
    }

    private void OnTriggerEnter2D()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("star")))
        {
            StartCoroutine(visibleForWhile());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("star")))
        {
            StartCoroutine(visibleForWhile());
        }
    }

    IEnumerator visibleForWhile()
    {
        for(int cnt=0;cnt<3;cnt++)
        {
            if(mySpriteRenderer.color.a<0.15f)
                 mySpriteRenderer.color = new Vector4(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, mySpriteRenderer.color.a + 4f * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
