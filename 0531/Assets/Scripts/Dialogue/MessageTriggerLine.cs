using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTriggerLine : MonoBehaviour
{
    [SerializeField] MassageTrigger msg;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        msg.TriggerMessage();
    }
}
