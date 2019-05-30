using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            TimeWatch.isNight = !TimeWatch.isNight;
            Destroy(this.gameObject);
        }

    }
}
