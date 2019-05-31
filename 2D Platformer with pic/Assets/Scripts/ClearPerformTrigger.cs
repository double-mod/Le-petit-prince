using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPerformTrigger : MonoBehaviour
{
    [SerializeField] GameObject cObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            cObj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
