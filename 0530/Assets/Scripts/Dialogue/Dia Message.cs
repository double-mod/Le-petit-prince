using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiaMessage
{
    [TextArea(3, 10)]
    public string sentences;
    [SerializeField] public float displaySpeed = 0.1f;

    [SerializeField] public float displayTime = 2.5f;
}
