using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip : MonoBehaviour
{
    public enum scale
    {
        STAY,
        ENLARGE,
        NARROW,
    }

    [Range(-1,1)]
    public float direction_x;
    [Range(-1, 1)]
    public float direction_y;

    public float maxScale = 1f;

    public float speed;

    public scale Scale;
    [Range(2, 0)]
    public float ScalePerFrame=1f;

    private Rigidbody2D myRigidbody2D;

    private Vector3 originScale;

    private Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        originScale = transform.localScale;
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(direction_x, direction_y);
        transform.localScale *= ScalePerFrame;
        if (Scale == scale.ENLARGE)
            if (transform.localScale.x > maxScale)
                transform.localScale = new Vector3(maxScale, maxScale, maxScale);
    }
}
