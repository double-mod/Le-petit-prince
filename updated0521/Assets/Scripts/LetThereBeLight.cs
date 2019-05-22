using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetThereBeLight : MonoBehaviour
{
    public bool testControl = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LightOnOf();
        if(testControl==true)
        {
            TimeWatch.isNight = !TimeWatch.isNight;
            testControl = false;
        }
    }

    private void LightOnOf()
    {
        if (TimeWatch.isNight == true)
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        else
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

    }
}
