using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{

    public enum EnergyType
    {
        Luminous,
        Fog
    }

    public EnergyType energyType;

    [SerializeField]
    private int cageCnt = 3;

    [SerializeField]
    private int chargePerFrame=10;

    [SerializeField]
    private int energyPerCage=100;

    private int currentCage = 0;

    private int currentEnergy = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(energyType)
        {
            case EnergyType.Luminous:
                if (this.GetComponent<EventSystem>().checkEvent())
                {
                    currentEnergy += chargePerFrame;
                    if (currentEnergy >= energyPerCage)
                    {
                        currentEnergy = 0;
                        if (currentCage <= cageCnt) currentCage++;
                    }
                }
                break;
            case EnergyType.Fog:
                if (this.GetComponent<EventSystem>().checkEvent())
                {
                    currentEnergy -= chargePerFrame;
                    if (currentEnergy < 0)
                    {
                        currentEnergy = 100;
                        if (currentCage > 0) currentCage--;
                    }
                }
                break;
        }

        
    }
}
