using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextFlow : MonoBehaviour
{
    [SerializeField] GameFlowManager gamef;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gamef.ActiveNextFlow();
            gameObject.SetActive(false);
        }
    }
}
