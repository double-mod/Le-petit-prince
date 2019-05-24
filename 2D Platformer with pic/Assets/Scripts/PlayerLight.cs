using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public float intensityChange;
    public float maxIntensity;
    public float minIntensity;

    private TimeWatch timeWatch;
    private Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        timeWatch = GetComponent<TimeWatch>();
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeWatch.statIsChanged())
        {
            if (TimeWatch.isNight)
            {
                StartCoroutine(plusLight(intensityChange));
            }
            else
            {
                StartCoroutine(minusLight(intensityChange));
            }
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
