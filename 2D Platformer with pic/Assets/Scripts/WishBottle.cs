using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StarDust))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class WishBottle : MonoBehaviour
{
    public float distance;

    public float wishBottleRecoverTime = 0.5f;

    public float speed = 1;

    private float theta = 0;

    private bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        var radius = theta * speed / 180 * Mathf.PI;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + distance * Mathf.Sin(radius)*Time.deltaTime);
        if ((++theta)*speed <= 360)
            ;
        else
            theta = 0;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Player"))&&on==false)
        {
            this.GetComponent<StarDust>().boost();
            Debug.Log("boost");
            StartCoroutine(pause());
        }
    }

    IEnumerator pause()
    {
        on = true;
        yield return new WaitForSeconds(wishBottleRecoverTime);
        on = false;
    }
}
