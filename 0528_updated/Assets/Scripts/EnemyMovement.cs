using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float front = 1f;
    Rigidbody2D myRigdbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigdbody = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(front, 1);
    }

    // Update is called once per frame
    void Update()
    {
        myRigdbody.velocity = new Vector2(front * moveSpeed, myRigdbody.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        front *= -1;
        transform.localScale = new Vector2(front, 1);
    }
}
