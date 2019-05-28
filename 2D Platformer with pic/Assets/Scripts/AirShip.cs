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

    public Sprite[] sprites;

    [Range(-1,1)]
    public float direction_x;
    [Range(-1, 1)]
    public float direction_y;

    public float maxScale = 1f;

    //public float speed;

    public scale Scale;
    [Range(0.97f, 1.03f)]
    public float ScalePerFrame=1f;

    private Rigidbody2D myRigidbody2D;

    private SpriteRenderer mySpriteRenderer;

    private Vector3 originScale;

    private Vector3 originPos;

    private bool prev;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        originScale = transform.localScale;
        originPos = transform.position;
        prev = TimeWatch.isNight;
        if (TimeWatch.isNight)
        {
            mySpriteRenderer.sprite = sprites[1];
        }
        else
            mySpriteRenderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(direction_x, direction_y);
        transform.localScale *= ScalePerFrame;
        if (Scale == scale.ENLARGE)
            if (transform.localScale.x > maxScale)
                transform.localScale = new Vector3(maxScale, maxScale, maxScale);
        Vector3 pos = transform.localPosition;

        //check dayTime
        if(prev!=TimeWatch.isNight)
        {
            if (TimeWatch.isNight)
            {
                mySpriteRenderer.sprite = sprites[1];
            }
            else
                mySpriteRenderer.sprite = sprites[0];
            prev = TimeWatch.isNight;
        }

        if(pos.x<-21||pos.x>21||pos.y>12||pos.y<-12)
        {
            Destroy(this.gameObject);
            CreatePlane.planeCnt--;
        }
    }
}
