using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{

    [SerializeField]
    float starSpeed_x = 4f;
    [SerializeField]
    float starSpeed_y = 4f;
    [SerializeField]
    bool isCircle = true;

    Rigidbody2D myRigidBody2D;
    BoxCollider2D myBoxCollider2D;
    Coroutine deleteObj;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myBoxCollider2D.enabled = false;
        StartCoroutine(littlePause());
        StartCoroutine(colliderEnable());
        deleteObj = StartCoroutine(DeleteObjIn3());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D()
    {
        //float littleSpd = 0.99f*Time.deltaTime;
      //  transform.localScale = new Vector3(transform.localScale.x *littleSpd, transform.localScale.y * littleSpd, transform.localScale.z);
        if (myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Ground"))
            ||myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("untouchable"))
            || myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("unseen")))
        {
            StopCoroutine(deleteObj);
            StartCoroutine(DeleteObjIn2());
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x / 40, myRigidBody2D.velocity.y / 40);
            myRigidBody2D.gravityScale = 0f;
        }

    }

    IEnumerator DeleteObjIn3()
    {

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    IEnumerator DeleteObjIn2()
    {

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator littlePause()
    {
        yield return new WaitForSeconds(0.3f);
        if(!isCircle)
            myRigidBody2D.velocity *= UnityEngine.Random.Range(0,starSpeed_x);
        else  
            myRigidBody2D.velocity *=starSpeed_x;
    }

    IEnumerator colliderEnable()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f,0.5f));
        myBoxCollider2D.enabled = true;
    }
}
