using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertices : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject sphere;
    List<GameObject> elements = new List<GameObject>();

    List<Mesh> meshes = new List<Mesh>();

    List<Vector3[]> vertices = new List<Vector3[]>();
    List<int[]> triangles = new List<int[]>();
    List<Vector3[]> normals = new List<Vector3[]>();
 
    List<Vector3> startPos = new List<Vector3>();
    List<Vector3> endPos = new List<Vector3>();

    List<Vector3> sPoints = new List<Vector3>();
    List<Vector3> ePoints = new List<Vector3>();

   


    void Start ()
    {
        elements.Add(cube1);
        elements.Add(cube2);
        elements.Add(cube3);
        elements.Add(cube4);
        meshes.Add(cube1.GetComponent<MeshFilter>().mesh);
        meshes.Add(cube2.GetComponent<MeshFilter>().mesh);
        meshes.Add(cube3.GetComponent<MeshFilter>().mesh);
        meshes.Add(cube4.GetComponent<MeshFilter>().mesh);
        for (int i=0; i<4; i++)
        {
            sPoints.Add(new Vector3(0, 0, 0));
            ePoints.Add(new Vector3(0, 0, 0));
        }
        
       
    }
	
	
	void Update ()
    {
        MakeMeshData();
        for (int j = 0; j < meshes.Count; j++)
        {
            for (int i = 0; i < vertices[j].Length; i++)
            {
                meshes[j].vertices[i] += meshes[j].normals[i] * Mathf.Sin(Time.time);
                
            }
            meshes[j].vertices = vertices[j];
        }
    }

    void MakeMeshData()
    {
        for (int j =0; j<meshes.Count; j++)
        {
            vertices.Add (meshes[j].vertices); 
            normals.Add(meshes[j].normals);
            triangles.Add(meshes[j].triangles);
            meshes[j].RecalculateNormals();
            meshes[j].RecalculateBounds();
        }  
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
        for (int j=0 ; j < meshes.Count; j++)
        {
            Matrix4x4 localToWorld = transform.localToWorldMatrix;
            for (int i = 0; i < vertices[j].Length ; i++)
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

