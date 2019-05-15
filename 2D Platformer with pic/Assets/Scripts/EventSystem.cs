using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public enum eventType
    {
        NONE=0,
        LIGHT=1<<0,
        FOG=1<<1,
        BOTH=LIGHT|FOG
    }

    public GameObject[] eventItem;

    public LayerMask[] LayerMask;

    private int layerMaskValue;

    private Vector3 direction;

    private float Range;

    private eventType EventType=eventType.NONE;

    // Start is called before the first frame update
    void Start()
    {
        SetLayerMaskValue(LayerMask);
    }

    private void Update()
    {
        checkEvent();
    }

    private void SetLayerMaskValue(LayerMask[] layerMask)
    {
        for (int i = 0; i < layerMask.Length; i++)
        {
            layerMaskValue = layerMask[i].value | layerMaskValue;
        }

    }

    public void checkEvent()
    {
        //for(int i=0;i<eventItem.Length;i++)
        //{
        //    direction = eventItem[i].transform.position - transform.position;
        //    float distance = Vector3.Distance(eventItem[i].transform.position, transform.position);
        //    Range = eventItem[i].GetComponent<meshTest>().retRange();
        //    //when hit before hit happen
        //    if (Physics2D.Raycast(transform.position, direction, Range, layerMaskValue)) 
        //    {
        //        Debug.Log("false");
        //    }
        //}
        
        //refresh the data every frame
        EventType = eventType.NONE;
        for(int i = 0; i < eventItem.Length; i++)
        {
            //when hit happen
            float distance = Vector3.Distance(eventItem[i].transform.position, transform.position);
            Range = eventItem[i].GetComponent<MeshTest>().retRange();
            if (Range>=distance)
            {
                if ((1<<eventItem[i].layer) == UnityEngine.LayerMask.GetMask("light"))
                    EventType |= eventType.LIGHT;
                else if ((1<<eventItem[i].layer) == UnityEngine.LayerMask.GetMask("fog"))
                    EventType |= eventType.FOG;
                Debug.Log("true");
            }
        }
        Debug.Log(EventType);
        //when hit do not happen
       // Debug.Log("false");
    }

    public eventType getEventTYpe()
    {
        return EventType;
    }
}
