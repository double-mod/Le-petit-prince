using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDaytime : MonoBehaviour
{
    public bool isDaytime;
    // Start is called before the first frame update
    void Start()
    {
        TimeWatch.isNight = !isDaytime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
