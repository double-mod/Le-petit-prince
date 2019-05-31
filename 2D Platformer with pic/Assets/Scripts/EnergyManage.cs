using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManage : MonoBehaviour
{
    [SerializeField] Energy energy;
    [SerializeField] GameObject[] energyStars;
    int preCage = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if( preCage < energy.getThisEnergy().currentCage)
        {
            preCage++;
            energyStars[preCage - 1].SetActive(true);
        }
        else if (preCage > energy.getThisEnergy().currentCage)
        {
            preCage--;
            energyStars[preCage].SetActive(false);
        }
    }
}
