using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public struct energyStat
    {
        public int currentCage;
        public int currentEnergy;
    }


    [SerializeField]
    private int cageCnt = 3;

    [SerializeField]
    private int chargePerFrame=10;

    [SerializeField]
    private int decreasePerFrame = 5;

    [SerializeField]
    private int energyPerCage=100;

    private energyStat EnergyStat;

    // Start is called before the first frame update
    void Start()
    {
        EnergyStat.currentCage = 0;
        EnergyStat.currentEnergy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        energyCheck();
    }

    public energyStat getThisEnergy()
    {
        return EnergyStat;
    }

    public void energyIncrease(int rate)
    {
        if (EnergyStat.currentCage < 1)
            if ((EnergyStat.currentEnergy += rate) >= 100)
                EnergyStat.currentCage += 1;     
    }
    public bool energyUse ()
    {
        if (EnergyStat.currentCage < 1)
        {
            return false;
        }
        else
        {
            EnergyStat.currentCage--;
            return true;
        }
    }

    private void energyCheck()
    {
        switch (this.GetComponent<EventSystem>().getEventTYpe())
        {
            case EventSystem.eventType.LIGHT:
                if(EnergyStat.currentCage<cageCnt)
                    EnergyStat.currentEnergy += chargePerFrame;
                if (EnergyStat.currentEnergy >= energyPerCage)
                {
                    EnergyStat.currentEnergy = 0;
                    if (EnergyStat.currentCage < cageCnt) EnergyStat.currentCage++;
                }
                break;
            case EventSystem.eventType.FOG:
                if(EnergyStat.currentEnergy>0)
                    EnergyStat.currentEnergy -= decreasePerFrame;
                if (EnergyStat.currentEnergy <= 0)
                {
                    if (EnergyStat.currentCage > 0)
                    {
                        EnergyStat.currentCage--;
                        EnergyStat.currentEnergy = 100;
                    }
                }
                break;
        }
        //Debug.Log(EnergyStat.currentCage);
        //Debug.Log(EnergyStat.currentEnergy);
    }

}
