using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    static Mesh _sphere;
    static Material _material;

    Vector3[] _vertices;

    void Awake()
    {
        if (_sphere == null)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _sphere = go.GetComponent<MeshFilter>().mesh;
            Destroy(go);

            _material = new Material(Shader.Find("Standard"))
            {
                color = Color.red,
                enableInstancing = true
            };
        }
    }

    void Start()
    {
        _vertices = GetComponent<MeshFilter>().mesh.vertices;
    }

    void Update()
    {
        if (MainController.DrawVertices)
            DrawVertices();
    }

    void DrawVertices()
    {
        var matrices = _vertices
            .Select(v=> Matrix4x4.TRS(transform.TransformPoint(v), Quaternion.identity, Vector3.one * 0.1f))
            .ToArray();

        Graphics.DrawMeshInstanced(_sphere, 0, _material, matrices);
    }
}
