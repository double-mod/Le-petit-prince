using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float distance;

    public enum type
    {
        cloud,
        sky
    }

    public bool isFollow;

    public type Type;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(Type)
        {
            case type.cloud:
                if(!isFollow)
                    transform.position = new Vector3(player.transform.position.x, transform.position.y,10f);
                else
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y-distance,10f);
                break;
            case type.sky:
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y,10f);
                break;
        }

    }
}
