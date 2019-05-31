using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShip : MonoBehaviour
{
    public float theta=2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float rotation_z = 0f;
    // Update is called once per frame
    void Update()
    {
       // this.transform.RotateAround(this.transform.parent.transform.position, Vector3.left, Time.deltaTime * 20);
        Vector2 direction;
        //rotation_z += theta * Time.deltaTime*0.001f;
        direction.x = (transform.position.x - this.transform.parent.transform.position.x) * Mathf.Cos(theta * Mathf.PI / 180) - (transform.position.y - this.transform.parent.transform.position.y) * Mathf.Sin(theta * Mathf.PI / 180) + this.transform.parent.transform.position.x;
        direction.y = (transform.position.x - this.transform.parent.transform.position.x) * Mathf.Sin(theta * Mathf.PI / 180) + (transform.position.y - this.transform.parent.transform.position.y) * Mathf.Cos(theta * Mathf.PI / 180) + this.transform.parent.transform.position.y;
        transform.position = direction;
        transform.Rotate(Vector3.back, -1f);
    }
}
