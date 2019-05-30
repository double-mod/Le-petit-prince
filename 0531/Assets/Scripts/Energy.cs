using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public enum EventSoundEffect
    {
        CHARGE,
        ABSORB,
        NONE
    }


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

    [SerializeField]
    AudioClip[] EventSE;

    private energyStat EnergyStat;

    private bool DashInStarry = false;

    private Vector3 velocity;

    private EventSystem eventSystem;

    private AudioSource[] myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        EnergyStat.currentCage = 0;
        EnergyStat.currentEnergy = 0;
        eventSystem = GetComponent<EventSystem>();
        myAudioSource = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        energySound();
        energyCheck();
        if (DashInStarry) GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public void setDashInStarry(bool a)
    {
        DashInStarry = a;
    }

    public energyStat getThisEnergy()
    {
        return EnergyStat;
    }

    public void energyIncrease(int rate)
    {
        if (EnergyStat.currentCage < 1)
            if ((EnergyStat.currentEnergy += rate) >= 100)
            {
                EnergyStat.currentCage += 1;
                EnergyStat.currentEnergy = 0;
            }
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
        switch (eventSystem.getEventTYpe())
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
            case EventSystem.eventType.STARRYLIGHTA:
                EnergyStat.currentCage = 3;
                this.GetComponent<Player>().InfinitDash(true);
                break;
            case EventSystem.eventType.STARRYLIGHTB:
                EnergyStat.currentCage = 3;
                this.GetComponent<Player>().InfinitDash(true);
                if (GetComponent<Player>().getState() == "Dash" || GetComponent<Player>().getState() == "SuperJump")
                {
                    DashInStarry = true;
                    velocity=GetComponent<Rigidbody2D>().velocity;
                }
                break;
            case EventSystem.eventType.NONE:
                DashInStarry = false;
                this.GetComponent<Player>().InfinitDash(false);
                break;
        }
        //Debug.Log(EnergyStat.currentCage);
        //Debug.Log(EnergyStat.currentEnergy);
    }

    private void energySound()
    {
        if(eventSystem._eventChanged)
        {
            if(EnergyStat.currentCage < cageCnt&&(eventSystem.getEventTYpe()==EventSystem.eventType.LIGHT
                || eventSystem.getEventTYpe() == EventSystem.eventType.STARRYLIGHTA
                || eventSystem.getEventTYpe() == EventSystem.eventType.STARRYLIGHTB))
            {
                playSound(EventSoundEffect.CHARGE);
            }
            else if(EnergyStat.currentCage >0 &&
                eventSystem.getEventTYpe() == EventSystem.eventType.FOG)
            {
                playSound(EventSoundEffect.ABSORB);
            }
        }
    }

    private void playSound(EventSoundEffect sound)
    {
        //myAudioSource.Stop();
        if (sound != EventSoundEffect.NONE) myAudioSource[1].PlayOneShot(EventSE[(int)sound]);
    }

    public int energyHave()
    {
        return EnergyStat.currentEnergy;
    }

}
