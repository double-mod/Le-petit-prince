using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            this.GetComponent<Suck>().enabled = true;
            Debug.Log("我赢啦");
        }
    }
}
