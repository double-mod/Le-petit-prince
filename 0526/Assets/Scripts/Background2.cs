using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2 : MonoBehaviour
{
    public bool test=false;
    public float alphaPlus;
    private SpriteRenderer spriteRenderer;
    private int type;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        type = 0 ;
    }

    // Update is called once per frame
    void Update()
    {
        if (test== true)
        {
            TimeWatch.isNight = !TimeWatch.isNight;
            test= false;
        }

        if (GetComponent<TimeWatch>().statIsChanged())
        {

            if (type != 0) type = 0;
            else type = 1;
            if(type==1)
                StartCoroutine(fadein());
            else if (type == 0)
                StartCoroutine(fadeout());
        }
    }

    IEnumerator fadein()
    {
        while (spriteRenderer.color.a < 1)
        {
            spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + alphaPlus);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator fadeout()
    {
        while (spriteRenderer.color.a >0)
        {
            spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - alphaPlus);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
