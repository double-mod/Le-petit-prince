using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarDust : MonoBehaviour
{
    [SerializeField]
    GameObject starDustPrefab;
    [SerializeField]
    int starAmount = 10;
    [SerializeField]
    float startspd = 0.4f;
    [SerializeField]
    int boostCnt = 2;

    const float PI = 3.1415926f;

    Vector2 starStartingSpd;

    // Start is called before the first frame update
    void Start()
    {
        starStartingSpd = new Vector2(startspd, startspd);
    }

    // Update is called once per frame
    void Update()
    {

        //if(Input.GetKeyDown("j"))
        //{
        //    //StarDusting();
        //    boost();
        //    //StartCoroutine(Boost());
        //}
        
    }

    //private void StarDusting()
    //{
    //    StartCoroutine(PrintAndWait());
    //}
    //IEnumerator PrintAndWait()
    //{
    //    for (int i = 0; i < starAmount; i++)
    //    {
    //        GameObject starDust = Instantiate(starDustPrefab, transform.position, Quaternion.identity) as GameObject;
    //        Vector2 direction = new Vector2(starSpeed_x, starSpeed_y);
    //        direction.x = starSpeed_x * Mathf.Cos(theta * PI / 180) - starSpeed_y * Mathf.Sin(theta * PI / 180);
    //        direction.y = starSpeed_x * Mathf.Sin(theta * PI / 180) + starSpeed_y * Mathf.Cos(theta * PI / 180);
    //        starDust.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x,direction.y);
    //    }
    //    yield return new WaitForSeconds(2f);
    //}


    public void boost()
    {
        for(float theta=0;theta<360;theta+=10)
        {
            GameObject starDust = Instantiate(starDustPrefab, transform.position, Quaternion.identity) as GameObject;
            Vector2 direction = new Vector2(starStartingSpd.x, starStartingSpd.y);
            direction.x = starStartingSpd.x * Mathf.Cos(theta * PI / 180) - starStartingSpd.y * Mathf.Sin(theta * PI / 180);
            direction.y = starStartingSpd.x * Mathf.Sin(theta * PI / 180) + starStartingSpd.y * Mathf.Cos(theta * PI / 180);
            starDust.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);
        }
    }

    //IEnumerator Boost()
    //{
    //    boost();
    //    yield return new WaitForSeconds(0.1f);
    //}
}
