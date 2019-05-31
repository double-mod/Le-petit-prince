using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBlock : MonoBehaviour
{
    public GameObject gameObject;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            collision.gameObject.transform.parent = transform;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject != null)
            collision.gameObject.transform.parent = gameObject.transform;
        else
            collision.gameObject.transform.parent = null;
    }
}
