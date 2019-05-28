using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLight : MonoBehaviour
{
    [Range(0f, 1f)]
    public float decresePerFrame = 0.1f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (TimeWatch.isNight)
        {
            if (spriteRenderer.color.a > 0)
            {
                spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - decresePerFrame * Time.deltaTime);
            }
            else
                spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
        else
            spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);


    }
}
