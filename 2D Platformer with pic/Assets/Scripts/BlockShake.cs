using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShake : MonoBehaviour
{
    public float shakeAmount=0.05f;
    public bool isShake = false;
    private Vector3 first_pos;

    private void Start()
    {
        first_pos = this.transform.localPosition;
    }

    public void StartShake()
    {
        isShake= true;
    }

    public void Update()
    {
        if (!isShake) return;
        Vector3 pos = first_pos + Random.insideUnitSphere * shakeAmount;
        pos.y = transform.localPosition.y;
        transform.localPosition = pos;
    }

    public void EndShake()
    {
        isShake = false;
        first_pos.y = transform.localPosition.y;
        transform.localPosition = first_pos;
    }
}
