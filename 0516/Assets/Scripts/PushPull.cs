using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PushPull : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Rigidbody2D myRigidbody2D;

    private bool pull=false;    

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        toucchBlock();
        if (pull == true)
            myRigidbody2D.velocity=new Vector2( player.GetComponent<Rigidbody2D>().velocity.x,myRigidbody2D.velocity.y);
    }

    private void toucchBlock()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            if (Input.GetKeyDown("e") && pull == false)
            {
                transform.parent= player.transform;
                //Debug.Log("按下去了");
                player.GetComponent<Player>().SetFlipStat(false);
                myRigidbody2D.isKinematic = false;
                pull = true;
            }
            else if (Input.GetKeyDown("e") && pull == true)
            {
                transform.parent = null;
                //Debug.Log("离开了");
                myRigidbody2D.isKinematic = true;
                player.GetComponent<Player>().SetFlipStat(true);
                myRigidbody2D.velocity = new Vector2(0, 0);
                pull = false;
            }
            if (player.GetComponent<Player>().getState() =="Air")
            {
                transform.parent = null;
                player.GetComponent<Player>().SetFlipStat(true);
                pull = false;
                myRigidbody2D.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            transform.parent = null;
            player.GetComponent<Player>().SetFlipStat(true);
            pull = false;
            myRigidbody2D.velocity = new Vector2(0, 0);
        }
    }

}
