using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshParticleSystem : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    private int MAX_QUAD_COUNT = 15000;
    private int quadIndex;
    void Awake()
    {
        mesh = new Mesh();
        vertices = new Vector3[4 * MAX_QUAD_COUNT];
        uv = new Vector2[4 * MAX_QUAD_COUNT];
        triangles = new int[6 * MAX_QUAD_COUNT];
        AddQuad(new Vector3(0, 0),0.0f,Vector3.one);

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddQuad(Vector3 position,float rotation,Vector3 quadSize)
    {
        if (quadIndex >= MAX_QUAD_COUNT)
        {
            return;
        }

        UpdateQuad(quadIndex, position, rotation, quadSize);

        quadIndex++;
    }

    private void UpdateQuad(int quadIndex,Vector3 position,float rotation,Vector3 quadSize)
    {

        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * quadSize;
        vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * quadSize;
        vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * quadSize;
        vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * quadSize;

        uv[vIndex0] = new Vector2(0, 0);
        uv[vIndex1] = new Vector2(0, 1);
        uv[vIndex2] = new Vector2(1, 1);
        uv[vIndex3] = new Vector2(1, 0);

        int tIndex = quadIndex * 6;
        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;


    }
}
