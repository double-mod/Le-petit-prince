using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWatch : MonoBehaviour
{
    static public bool isNight = false;

    public void timeChange()
    {
        isNight = !isNight;
    }
}
