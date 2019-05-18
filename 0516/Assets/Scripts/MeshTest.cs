using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]

[RequireComponent(typeof(MeshRenderer))]

public class MeshTest : MonoBehaviour
{
    //mask collor
    public Color color = Color.yellow;
    //mask range
    public float Range = 3;
    //color change offset
    public float color_offset = 0.2f;
    //should be >=3
    public int SmoothnessOfLighting = 15;
    //layer need to check
    public LayerMask[] LayerMask;
    //sector start
    [Range(0, 1)]
    public float sectorStart = 0f;
    //sector end
    [Range(0, 1)]
    public float sectorEnd = 1f;

    public float startAngle,endAngle;
    //layer mask value
    private int layerMaskValue;

    private Vector3[] vectorsOfLightMesh;

    private Mesh mesh;

    private Vector3 direction;

    private RaycastHit2D hit;

    private int triangleIndex;

    private int[] triangles;

    private Vector2[] uvArray;

    private bool colorTrigger = false;


    void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;

        vectorsOfLightMesh = new Vector3[SmoothnessOfLighting + 1];

        triangles = new int[SmoothnessOfLighting * 3];

        uvArray = new Vector2[SmoothnessOfLighting + 1];

        SetLayerMaskValue(LayerMask);

        Vector3 min = new Vector3(0f, 0f, 0f);

        Vector3 max = new Vector3(1000f,1000f, 1000f);

        mesh.bounds.SetMinMax(min, max);
    }


    void Update()
    {
        SetLayerMaskValue(LayerMask);

        UpdateColor();

        GenerateMesh();

        startAngle = sectorStart * 360 ;
        endAngle = sectorEnd * 360;
    }

    private void SetLayerMaskValue(LayerMask[] layerMask)

    {

        for (int i = 0; i < layerMask.Length; i++)
        {
            layerMaskValue = layerMask[i].value;
        }

    }

    protected void UpdateColor()
    {
        Color32[] colors32 = new Color32[mesh.vertices.Length];

        if (colorTrigger)
        {
            if ((color.a += color_offset * Time.deltaTime) > 0.6f)
            {
                colorTrigger = !colorTrigger;

            }
        }
        else
        {
            if ((color.a -= color_offset * Time.deltaTime) < 0.4f)
            {
                colorTrigger = !colorTrigger;
            }
        }

        for (int i = 0; i < colors32.Length; i++)
        {
            colors32[i] = color;
        }
        mesh.colors32 = colors32;
    }

    private void GenerateMesh()
    {

        vectorsOfLightMesh[0] = Vector3.zero;
        int cnt =0, limit=0;
        cnt = (int)(SmoothnessOfLighting * sectorEnd);
        limit = (int)(SmoothnessOfLighting * sectorStart);
        for (; cnt > limit; cnt--)

        {

            direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * cnt * 360 / SmoothnessOfLighting), Mathf.Sin(Mathf.Deg2Rad * cnt * 360 / SmoothnessOfLighting), 0);

            direction = direction * Range;

            if (Physics2D.Raycast(transform.position, direction, Range, layerMaskValue))

            {

                Debug.DrawLine(transform.position, transform.position + direction, Color.red);

                hit = Physics2D.Raycast(transform.position, direction, Range, layerMaskValue);

                vectorsOfLightMesh[SmoothnessOfLighting - cnt + 1] = (Vector3)hit.point + Vector3.forward * transform.position.z - transform.position;

            }

            else

            {
                Debug.DrawLine(transform.position, transform.position + direction, Color.white);

                vectorsOfLightMesh[SmoothnessOfLighting - cnt + 1] = direction;
            }

        }

        mesh.vertices = vectorsOfLightMesh;

        triangleIndex = 0;

        for (int i = 0, j = 1; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j;
            if ((j + 1) == vectorsOfLightMesh.Length)
                triangles[i + 2] = 1;
            else
                triangles[i + 2] = j + 1;
        }


        mesh.triangles = triangles;

        UpdateUVs(vectorsOfLightMesh);

    }

    private void UpdateUVs(Vector3[] vertices)
    {
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            float u = vertices[i].x / Range / 2 + 0.5f;
            float v = vertices[i].y / Range / 2 + 0.5f;
            
            u = SMath.Clamp(0, u, 1);
            v = SMath.Clamp(0, v, 1);

            uvs[i] = new Vector2(u, v);
        }

        mesh.uv = uvs;
    }

    public float retRange()
    {
        return Range;
    }
}
