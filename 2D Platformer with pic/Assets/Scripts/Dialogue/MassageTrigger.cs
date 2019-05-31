using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassageTrigger : MonoBehaviour
{
    [SerializeField] DiaMessage message;
    [SerializeField] float resetTime = 4f;

    BoxCollider2D myCollider;
    MessageManager messageManager;

    private void Start()
    {
        messageManager = FindObjectOfType<MessageManager>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    public void TriggerMessage()
    {
        messageManager.StartMessage(message);
        myCollider.enabled = false;
        StartCoroutine(ResetMe());
    }

    IEnumerator ResetMe()
    {
        yield return new WaitForSeconds(resetTime);
        myCollider.enabled = true;
    }
}
