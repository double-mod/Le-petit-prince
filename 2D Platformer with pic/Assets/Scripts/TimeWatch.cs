using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWatch : MonoBehaviour
{
    static public bool isNight = false;
    bool prev;

    public void timeChange()
    {
        isNight = !isNight;
    }

    public bool statIsChanged()
    {
        if (prev == isNight)
        {
            prev = isNight;
            return false;
        }
        else
        {
            prev = isNight;
            return true;
        }
    }

    private void Start()
    {
        prev = isNight;
    }
}
