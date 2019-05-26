using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLight : MonoBehaviour
{
    public float intensityChange;
    public float maxIntensity;
    public float minIntensity;

    private TimeWatch timeWatch;
    private Light light;

    // Start is called before the first frame update
    void Start()
    {
        timeWatch = GetComponent<TimeWatch>();
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeWatch.statIsChanged())
        {
            if (TimeWatch.isNight)
            {
                StartCoroutine(minusLight(intensityChange));
            }
            else
            {
                StartCoroutine(plusLight(intensityChange));
            }
        }
    }

    IEnumerator minusLight(float intensityChange)
    {
        while (light.intensity > minIntensity)
        {
            light.intensity -= intensityChange;
            yield return new WaitForSeconds(0.05f);
        }

    }

    IEnumerator plusLight(float intensityChange)
    {
        while (light.intensity < maxIntensity)
        {
            light.intensity += intensityChange;
            yield return new WaitForSeconds(0.05f);
        }

    }

}
