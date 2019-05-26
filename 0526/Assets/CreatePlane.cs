using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    [SerializeField]
    private int maxPlaneCnt = 5;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    [Range(0f, 3f)]
    private float nextPlaneCreateTime = 2f;

    [SerializeField]
    GameObject planePrefab;

    static public int planeCnt = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            StartCoroutine(create());
    }

    private bool RandomBool()
    {
        if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
            return true;
        else
            return false;
    }

    IEnumerator create()
    {
        while (planeCnt < maxPlaneCnt)
        {
            Vector3 pos = transform.position;
            bool boolcheck = RandomBool();
            if (boolcheck)
            {
                pos.x = 20f;
            }
            else
            {
                pos.x = -20f;
            }
            pos.y = UnityEngine.Random.Range(-5f, 5f);
            //create plane
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity) as GameObject;
            //specify variables

            plane.transform.parent = transform;
            plane.transform.localPosition = pos;


            //the sprite
            plane.GetComponent<SpriteRenderer>().sprite = RandomBool() ? sprites[0] : sprites[1];

            //the Airship variables
            var airship = plane.GetComponent<AirShip>();
            airship.direction_x = pos.x > 0 ? UnityEngine.Random.Range(-1f, 0f) : UnityEngine.Random.Range(0f, 1f);
            airship.direction_y = RandomBool() ? 0 : UnityEngine.Random.Range(-0.5f, 0.5f);
            plane.transform.localScale = airship.transform.localScale * UnityEngine.Random.Range(0f, 0.8f);

            //flip when x is over 0
            if(airship.direction_x>0)
            {
                plane.GetComponent<SpriteRenderer>().flipX = true;
            }

            planeCnt++;
            Debug.Log(planeCnt);

            yield return new WaitForSeconds(nextPlaneCreateTime);
        }
    }
        
}
