using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLight : MonoBehaviour
{
    public float intensityChange;
    public float maxIntensity;
    public float minIntensity;

    private TimeWatch timeWatch;
    private Light myLight;
    private bool prev;

    // Start is called before the first frame update
    void Start()
    {
        timeWatch = GetComponent<TimeWatch>();
        myLight = GetComponent<Light>();
        prev = TimeWatch.isNight;
    }

    // Update is called once per frame
    void Update()
    {
        if (prev!=TimeWatch.isNight)
        {
            if (TimeWatch.isNight)
            {
                StartCoroutine(minusLight(intensityChange));
            }
            else
            {
                StartCoroutine(plusLight(intensityChange));
            }
            prev = TimeWatch.isNight;
        }
    }

    IEnumerator minusLight(float intensityChange)
    {
        while (myLight.intensity > minIntensity)
        {
            myLight.intensity -= intensityChange;
            yield return new WaitForSeconds(0.05f);
        }

    }

    IEnumerator plusLight(float intensityChange)
    {
        while (myLight.intensity < maxIntensity)
        {
            myLight.intensity += intensityChange;
            yield return new WaitForSeconds(0.05f);
        }

    }

}
