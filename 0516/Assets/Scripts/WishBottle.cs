using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StarDust))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class WishBottle : MonoBehaviour
{
    public float wishBottleRecoverTime = 0.5f;

    private bool on = false;

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
