using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceScript : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject sphere;
    List<GameObject> elements = new List<GameObject>();

    //Mesh cube1mesh;
    //Mesh cube2mesh;
    //Mesh cube3mesh;
    //Mesh cube4mesh;
    List<Mesh> meshes = new List<Mesh>();

    List<Vector3[]> vertices = new List<Vector3[]>();
    List<int[]> triangles = new List<int[]>();
    List<Vector3[]> normals = new List<Vector3[]>();

    //Vector3[] vertices; //= new Vector3[meshes.Length];

    List<Vector3> startPos = new List<Vector3>();
    List<Vector3> endPos = new List<Vector3>();

    List<Vector3> sPoints = new List<Vector3>();
    List<Vector3> ePoints = new List<Vector3>();




    void Start()
    {
        elements.Add(cube1);
        elements.Add(cube2);
        elements.Add(cube3);
        elements.Add(cube4);
        meshes.Add(cube1.GetComponent<MeshFilter>().mesh);
        meshes.Add(cube2.GetComponent<MeshFilter>().mesh);
        meshes.Add(cube3.GetComponent<MeshFilter>().mesh);
        meshes.Add(cube4.GetComponent<MeshFilter>().mesh);
        for (int i = 0; i < 4; i++)
        {
            sPoints.Add(new Vector3(0, 0, 0));
            ePoints.Add(new Vector3(0, 0, 0));
        }
        MakeMeshData();

    }


    void Update()
    {

    }

    void MakeMeshData()
    {
        for (int i = 0; i < meshes.Count; i++)
        {
            // meshes[i] = elements[i].GetComponent<MeshFilter>().mesh;
            vertices.Add(meshes[i].vertices);
            normals.Add(meshes[i].normals);
            triangles.Add(meshes[i].triangles);
            meshes[i].RecalculateNormals();
            meshes[i].RecalculateBounds();
        }
        //cube1mesh = cube1.GetComponent<MeshFilter>().mesh;
        //cube2mesh = cube2.GetComponent<MeshFilter>().mesh;
        //cube3mesh = cube3.GetComponent<MeshFilter>().mesh;
        //cube4mesh = cube4.GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices1 = cube1mesh.vertices;
        //Vector3[] vertices2 = cube2mesh.vertices;
        //Vector3[] vertices3 = cube3mesh.vertices;
        //Vector3[] vertices4 = cube4mesh.vertices;
        //int[] triangles1 = cube1mesh.triangles;
        //int[] triangles2 = cube2mesh.triangles;
        //int[] triangles3 = cube3mesh.triangles;
        //int[] triangles4 = cube4mesh.triangles;

    }

    void OnGUI()
    {
        if (GUILayout.Button("Show Vertices"))
        {
            ShowVertices();
        }
    }

    void ShowVertices()
    {
        for (int j = 0; j < meshes.Count; j++)
        {
            Matrix4x4 localToWorld = transform.localToWorldMatrix;
            for (int i = 0; i < vertices[j].Length; i++)
            {
                // Vector3 pos = new Vector3(meshes[j].vertices[i].x, meshes[j].vertices[i].y, meshes[j].vertices[i].z);
                Vector3 world_v = localToWorld.MultiplyPoint3x4(meshes[j].vertices[i]);
                Instantiate(sphere, world_v, Quaternion.identity);
            }
            for (int i = 0; i < 4; i++)
            {
                sPoints[j] = sPoints[j] + localToWorld.MultiplyPoint3x4(meshes[j].vertices[i]);
            }
            startPos.Add(sPoints[j] / 4);
            Instantiate(sphere, startPos[j], Quaternion.identity);

            for (int i = 4; i < 8; i++)
            {
                ePoints[j] = ePoints[j] + localToWorld.MultiplyPoint3x4(meshes[j].vertices[i]);
            }
            endPos.Add(ePoints[j] / 4);
            Instantiate(sphere, endPos[j], Quaternion.identity);
        }
    }
}
