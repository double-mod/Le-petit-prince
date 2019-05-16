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
            direction = eventItem[i].transform.position - transform.position;
            var direct = transform.position - eventItem[i].transform.position;
            float startAngle = eventItem[i].GetComponent<MeshTest>().startAngle;
            float endAngle= eventItem[i].GetComponent<MeshTest>().endAngle;
            float distance = Vector3.Distance(eventItem[i].transform.position, transform.position);
            Range = eventItem[i].GetComponent<MeshTest>().retRange();

            //check whether this is in the range of sector
            Vector2 start = new Vector2(Mathf.Cos(Mathf.Deg2Rad * startAngle), Mathf.Sin(Mathf.Deg2Rad * startAngle));
            start = start.normalized;
            Vector2 end = new Vector2(Mathf.Cos(Mathf.Deg2Rad * endAngle), Mathf.Sin(Mathf.Deg2Rad * endAngle));
            end = end.normalized;
            Vector2 forwardLocalVect = start + end;
            if (endAngle - startAngle > 180)
                direct = -direct;
            float angle = Vector2.Angle(direct, forwardLocalVect);

            if (Range>=distance && angle<((endAngle-startAngle)/2) && (!Physics2D.Raycast(transform.position, direction, Range, layerMaskValue)))
            {
                //&& !Physics2D.Raycast(transform.position, direction, Range, layerMaskValue)
                if ((1<<eventItem[i].layer) == UnityEngine.LayerMask.GetMask("light")&&eventItem[i].activeInHierarchy==true)
                    EventType |= eventType.LIGHT;
                else if ((1<<eventItem[i].layer) == UnityEngine.LayerMask.GetMask("fog") && eventItem[i].activeInHierarchy == true)
                    EventType |= eventType.FOG;   
                Debug.Log("true");
            }
            bool boo = angle < ((endAngle - startAngle) / 2);
            //Debug.Log(angle);
            Debug.Log(boo);
            //Debug.Log(startAngle);
            //Debug.Log(endAngle);
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
