using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Drawing : MonoBehaviour
{
    [SerializeField]
    Mesh _box;
    [SerializeField]
    Material _opaque;
    [SerializeField]
    Material _transparent;
    static Drawing _instance;

    void Awake()
    {
        _instance = this;

    }

    public static void DrawCube(Vector3 center, float size)
    {
        var matrix = Matrix4x4.TRS(
                center,
                Quaternion.identity,
                Vector3.one * (size * 0.999f)
                );

        Graphics.DrawMesh(_instance._box, matrix, _instance._transparent, 0);
    }

    public static Mesh MakeTwistedBox(Vector3[] corners, Mesh mesh = null)
    {
        Vector3 center = Vector3.zero;
        foreach (var corner in corners)
            center += corner;

        center /= corners.Length;

        corners = corners
            .Select(c => c + (center - c).normalized * 0.05f)
            .ToArray();

        var f = new[]
        {
            0,1,3,2,
            4,6,7,5,
            6,4,0,2,
            4,5,1,0,
            5,7,3,1,
            7,6,2,3
        };

        var v = f.Select(i => corners[i]).ToArray();

        if (mesh == null)
        {
            var faces = Enumerable.Range(0, 24).ToArray();

            mesh = new Mesh()
            {
                vertices = v,
            };

            mesh.SetIndices(faces, MeshTopology.Quads, 0); // first submesh
        }
        else
        {
            mesh.vertices = v;
        }

        mesh.RecalculateNormals();
        return mesh;
    }

    public static Mesh MakeWireFrameBox()
    {
        float s = 0.5f;
        var corners = new[]
        {
         new Vector3(-s,-s,-s),
         new Vector3(s,-s,-s),
         new Vector3(-s,-s,s),
         new Vector3(s,-s,s),
         new Vector3(-s,s,-s),
         new Vector3(s,s,-s),
         new Vector3(-s,s,s),
         new Vector3(s,s,s),
        };

        var f = new[]
        {
            0,1,3,2,
            4,6,7,5,
            6,4,0,2,
            4,5,1,0,
            5,7,3,1,
            7,6,2,3
        };



        var edges = new[]
        {
            0,1,1,3,3,2,2,0,
            4,5,5,7,7,6,6,4,
            0,4,1,5,3,7,2,6
        };

        var mesh = new Mesh()
        {
            vertices = corners,
        };

        mesh.SetIndices(edges, MeshTopology.Lines, 0);
        mesh.RecalculateNormals();
        return mesh;
        //var v = f.Select(i => corners[i]).ToArray();

        //if (mesh == null)
        //{
        //    var faces = Enumerable.Range(0, 24).ToArray();


        //    mesh.SetIndices(faces, MeshTopology.Quads, 0); // first submesh
        //}
        //else
        //{
        //    mesh.vertices = v;
        //}

    }

}
