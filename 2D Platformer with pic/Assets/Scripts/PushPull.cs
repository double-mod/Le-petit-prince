using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PushPull : MonoBehaviour
{
    public static bool withBox = false;

    [SerializeField]
    private GameObject player;

    private Rigidbody2D myRigidbody2D;

    private bool pull=false;

    private bool playerIn = false;

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
        if ( Mathf.Abs( player.transform.position.x-transform.position.x) < 1.5f && Mathf.Abs(player.transform.position.y - transform.position.y) < 1.5f)
        {
            if (Input.GetKeyDown("e") && pull == false&&withBox==false)
            {
                transform.parent= player.transform;
                //Debug.Log("按下去了");
                player.GetComponent<Player>().SetFlipStat(false);
                myRigidbody2D.isKinematic = false;
                myRigidbody2D.gravityScale = 1.0f;
                pull = true;
                withBox = true;
                playerIn = true;
            }
            else if (Input.GetKeyDown("e") && pull == true)
            {
                transform.parent = null;
                //Debug.Log("离开了");
                myRigidbody2D.isKinematic = true;
                player.GetComponent<Player>().SetFlipStat(true);
                myRigidbody2D.velocity = new Vector2(0, 0);
                pull = false;
                withBox = false;
                playerIn = false;
            }
            if (player.GetComponent<Player>().getState() =="Air")
            {
                transform.parent = null;
                player.GetComponent<Player>().SetFlipStat(true);
                pull = false;
                myRigidbody2D.velocity = new Vector2(0, 0);
                withBox = false;
                playerIn = false;
            }
        }
        else
        {
            if(withBox==false||playerIn==true)
            {
                transform.parent = null;
                player.GetComponent<Player>().SetFlipStat(true);
                playerIn = false;
            }
            pull = false;
            myRigidbody2D.velocity = new Vector2(0, 0);
        }
    }

}
