using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public GameObject[] eventItem;

    public LayerMask[] LayerMask;

    private int layerMaskValue;

    private Vector3 direction;

    private float Range;

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

    public bool checkEvent()
    {
        for(int i=0;i<eventItem.Length;i++)
        {
            direction = eventItem[i].transform.position - transform.position;
            float distance = Vector3.Distance(eventItem[i].transform.position, transform.position);
            Range = eventItem[i].GetComponent<MeshTest>().retRange();
            if (Physics2D.Raycast(transform.position, direction, Range, layerMaskValue))
            {
                Debug.Log("false");
                return false;
            }
        }
        for(int i = 0; i < eventItem.Length; i++)
        {
            float distance = Vector3.Distance(eventItem[i].transform.position, transform.position);
            Range = eventItem[i].GetComponent<MeshTest>().retRange();
            if (Range>=distance)
            {
                Debug.Log("true");
                return true;
            }
        }
        return true;
    }
}
