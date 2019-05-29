using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suck : MonoBehaviour
{
    public GameObject[] SuckBlock;

    public Vector3 SuckPoint;

    public float suckFrequncy = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (SuckBlock != null)
        {
            for (int i = 0; i < SuckBlock.Length; i++)
            {
                StartCoroutine(suck(suckFrequncy*i, SuckBlock[i]));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator suck(float time,GameObject block)
    {
        yield return new WaitForSeconds(time);
        block.GetComponent<BlockShake>().StartShake();
        yield return new WaitForSeconds(2f);
        block.GetComponent<BlockShake>().EndShake();
        SuckMove(block);
    }

    private void SuckMove(GameObject block)
    {
        Debug.Log(SuckPoint);
        Debug.Log(block.transform.localPosition);
        Vector3 direction = SuckPoint - block.transform.position;
        Debug.Log(direction);
        block.GetComponent<Rigidbody2D>().velocity = direction;
    }
}
