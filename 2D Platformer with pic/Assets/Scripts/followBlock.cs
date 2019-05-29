using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("怎么回事");
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
