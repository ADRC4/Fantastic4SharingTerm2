using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualiseVoxels : MonoBehaviour
{
    int voxx = 13;  //12  //18 //13
    int voxy = 15;  //15  //18 //15
    int voxz = 15;  //10  //19 //15
    List<int> active = new List<int>();

    void Start()
    {
    /* data */
    TextAsset questdata0 = Resources.Load<TextAsset>("PerpendicularRandomStressOdd"); //PerpendicularRandomStress //VectorCoordinatesInEveryVoxel                                                                          
    var path = @"C:\Users\Marina\Documents\London\MArch Architectural Design 2017-2018\Semester 2\Unity Files\Christos Files\perpendicular random stress odd.csv"; // vector coordinates in every voxel  //perpendicular random stress
    var lines = File.ReadAllLines(path);

    //Debug.Log(data.Length);
    for (int ii = 1; ii<lines.Length; ii++)
    {
       string[] row = lines[ii].Split(',');                    
       int i = ii - 1;                                                                   

       Vector3 v;

       //int.TryParse(row[0], out i);
       v.x = float.Parse(row[1]);
       v.y = float.Parse(row[3]);
       v.z = float.Parse(row[2]);
  
       if (v.sqrMagnitude< 0.01f) continue;
  
            if (v.sqrMagnitude != 0)
            {
                active.Add(i);          // List the indexes of active voxels
            }
            
    }
    }

    void OnDrawGizmos()
    {

        for (int i = 0; i < active.Count; i++)
        {
            var iLayer = active[i] % (voxx * voxz);
            var x = iLayer / voxz;
            var z = iLayer % voxz;
            var y = active[i] / (voxx * voxz);
            Vector3 pos = new Vector3(x, y, z);
            Gizmos.color = new Color(1, 1, 1, 0.2f);
            Gizmos.DrawCube(pos, new Vector3(1, 1, 1));
        }
    }

}
