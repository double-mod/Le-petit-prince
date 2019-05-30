using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour
{
    GameFlowManager flowManager;
    [SerializeField] string nextSceneName;

    private void Start()
    {
        flowManager = FindObjectOfType<GameFlowManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            flowManager.LoadTargetScene(nextSceneName);
    }
}
