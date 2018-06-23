using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Nodes : MonoBehaviour
{
    public List<Transform> scannedPoles = new List<Transform>();
    
    int voxx = 13;
    int voxy = 15;
    int voxz = 15;
    float maxValue = 0f;
    List<int> active = new List<int>();
    List<Vector3> vectors = new List<Vector3>();
    List<Vector3> angles = new List<Vector3>();
    List<Vector3> positions = new List<Vector3>();
    List<Vector3> positionsType2 = new List<Vector3>(); //aligned to x and 3rd direction of connection
    List<float> stress = new List<float>();
    int R;
    int r;
    int rr;
    List<Vector3> type;
   // List<int> nodes = new List<int>();

    List<Vector3> voxelPosIE = new List<Vector3>();
    List<Vector3> voxelPosIIE = new List<Vector3>();
    List<Vector3> voxelPosIIIE = new List<Vector3>();
    List<Vector3> voxelPosIO = new List<Vector3>();
    List<Vector3> voxelPosIIO = new List<Vector3>();
    List<Vector3> voxelPosIIIO = new List<Vector3>();

    void Start()
    {
       
        TextAsset questdata0 = Resources.Load<TextAsset>("PerpendicularRandomStressOdd"); 
                                                                                          
        var path = @"C:\Users\Marina\Documents\London\MArch Architectural Design 2017-2018\Semester 2\Unity Files\Christos Files\perpendicular random stress odd.csv"; 
        var lines = File.ReadAllLines(path);

        for (int ii = 1; ii < lines.Length; ii++)
        {
            string[] row = lines[ii].Split(',');                    
            int i = ii - 1;                                                                 

            Vector3 v;

            //int.TryParse(row[0], out i);
            v.x = float.Parse(row[1]);
            v.y = float.Parse(row[3]);
            v.z = float.Parse(row[2]);


            if (v.sqrMagnitude < 0.01f) continue;

            var iLayer = i % (voxx * voxz);
            var x = iLayer / voxz;
            var z = iLayer % voxz;
            var y = i / (voxx * voxz);


            Vector3 pos = new Vector3(x, y, z);
            var q = Quaternion.LookRotation(v) * Quaternion.Euler(-90, 0, 0);
            var e = q.eulerAngles;
            var distance = v.magnitude;
            var direction = v / distance;

            if (v.sqrMagnitude != 0)
            {
                active.Add(i);                           
                stress.Add(v.sqrMagnitude);
                vectors.Add(v);
                angles.Add(e);
                positions.Add(pos);
            }
            





            for (int j = 0; j < stress.Count; j++)
            {
                if (stress[j] > maxValue)
                {
                    maxValue = stress[j];
                }
            }
        }

        Check();
    }

    



    void Check()
    {
        for (int i =0; i<stress.Count; i++)
        {
            int nodes = 0;
            int R = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count));
            Instantiate(scannedPoles[R], positions[i], Quaternion.identity);
            bool exists = Physics.CheckBox(positions[i], new Vector3 (0.5f, 0.5f, 0.5f), Quaternion.identity, 8, QueryTriggerInteraction.UseGlobal);  //8=Layer8=Nodes
            var box = new Bounds();
            box.Contains(point);
            int children = scannedPoles[R].childCount;

            foreach (GameObject child in scannedPoles[R])
            {
                child.layer = 8;
                if (exists &&  children <= stress[i])
                {
                    nodes++;
                }
                if (nodes < stress[i])
                {
                    int r = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count));
                    Instantiate(scannedPoles[r], positions[i], Quaternion.identity);
                }
            }

        }
    }


	void Update ()
    {
		
	}
}
