using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetThereBeLight : MonoBehaviour
{
    private bool prev;
    // Update is called once per frame
    void Update()
    {
        LightOnOf();
        prev = TimeWatch.isNight;
    }

    private void LightOnOf()
    {
        if (prev!=TimeWatch.isNight)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
            prev = TimeWatch.isNight;
        }
    }
}
