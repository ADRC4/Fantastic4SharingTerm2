using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LoadQuests : MonoBehaviour
{
    public List<GameObject> scannedPoles = new List<GameObject>();
    public List<Rigidbody> rigidbodyPoles = new List<Rigidbody>();
    //public Transform pole;

    [SerializeField]
    Material _material;
    // int voxx;  //= 13;  //12  //18 //13
    //int voxy;  // = 15;  //15  //18 //15
    // int voxz;  //= 15;  //10  //19 //15
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

    List<Quest1> quests1 = new List<Quest1>();
    List<Quest0> quests0 = new List<Quest0>();

    List<Vector3> voxelPosIE = new List<Vector3>();
    List<Vector3> voxelPosIIE = new List<Vector3>();
    List<Vector3> voxelPosIIIE = new List<Vector3>();
    List<Vector3> voxelPosIO = new List<Vector3>();
    List<Vector3> voxelPosIIO = new List<Vector3>();
    List<Vector3> voxelPosIIIO = new List<Vector3>();

    Mesh _box;

    IEnumerator coroutine;




    void Start()
    {
        _box = Drawing.MakeWireFrameBox();
        
        /* data */

        TextAsset questdata0 = Resources.Load<TextAsset>("PerpendicularRandomStressOdd"); //PerpendicularRandomStress //VectorCoordinatesInEveryVoxel
                                                                                          // string[] data = questdata0.text.Split(new char[] { '\n' });                //Create an Array for the entire data set, divided by each line
        var path = @"C:\Users\Marina\Documents\London\MArch Architectural Design 2017-2018\Semester 2\Unity Files\Christos Files\perpendicular random stress odd.csv"; // vector coordinates in every voxel  //perpendicular random stress

        var lines = File.ReadAllLines(path);

        
        //var height = scannedPoles.Select(t => t.GetComponent<Renderer>().bounds.size.y).ToArray(); //Array of heights

        //Debug.Log(data.Length);
        for (int ii = 1; ii < lines.Length; ii++)
        {
            string[] row = lines[ii].Split(',');                    // Create an Array for every row, that includes the elements that exist in a line
            int i = ii - 1;                                                                   // Quest0 q0 = new Quest0();

            Vector3 v;

            //int.TryParse(row[0], out i);
            v.x = float.Parse(row[1]);
            v.y = float.Parse(row[3]);
            v.z = float.Parse(row[2]);


            if (v.sqrMagnitude < 0.01f) continue;

            //quests0.Add(q0);

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
                active.Add(i);                           // List the indexes of active voxels
                stress.Add(v.sqrMagnitude);
                vectors.Add(v);
                angles.Add(e);
                positions.Add(pos);
            }
            //  Transform poleNew = Instantiate(pole, pos, q);


            for (int j = 0; j < stress.Count; j++)
            {
                if (stress[j] > maxValue)
                {
                    maxValue = stress[j];
                }
            }
         //   Debug.Log("maxValue: " + maxValue);  //maxValue: 196 (minValue: 0)
                                                 // OFFSET POSITIONS
                                                 /* if is even */
                                                 /* vertical poles */
            Vector3 posAE = Vector3.right * 0.1f;
            Vector3 posBE = Vector3.left * 0.1f;
            Vector3 posCE = Vector3.forward * 0.1f;
            Vector3 posDE = Vector3.back * 0.1f;
           
            voxelPosIE.Add(posAE);
            voxelPosIE.Add(posBE);
            voxelPosIE.Add(posCE);
            voxelPosIE.Add(posDE);
            /* horizontal poles */ //ALIGNED TO Z
            Vector3 pos1E = Vector3.right * 0.1f;
            Vector3 pos2E = Vector3.left * 0.1f;
            Vector3 pos3E = Vector3.up * 0.1f;
            Vector3 pos4E = Vector3.down * 0.1f;
            
            voxelPosIIE.Add(pos1E);
            voxelPosIIE.Add(pos2E);
            voxelPosIIE.Add(pos3E);
            voxelPosIIE.Add(pos4E);
            /* horizontal poles */  //ALIGNED TO X
            Vector3 posIE = Vector3.forward * 0.1f;
            Vector3 posIIE = Vector3.back * 0.1f;
            Vector3 posIIIE = Vector3.up * 0.1f;
            Vector3 posIVE = Vector3.down * 0.1f;
            
            voxelPosIIIE.Add(posIE);
            voxelPosIIIE.Add(posIIE);
            voxelPosIIIE.Add(posIIIE);
            voxelPosIIIE.Add(posIVE);
            /* Type 2 poles */  //ALIGNED TO X AND 3RD CONNECTION
            Vector3 posT2E1 = Vector3.forward * 0.1f + Vector3.up * 0.05f;
            Vector3 posT2E2 = Vector3.back * 0.1f + Vector3.down * 0.05f;
            Vector3 posT2E3 = Vector3.up * 0.05f + Vector3.back * 0.1f;
            Vector3 posT2E4 = Vector3.forward * 0.1f + Vector3.down * 0.05f;
            List<Vector3> voxelPosT2 = new List<Vector3>();
            voxelPosT2.Add(posT2E1);
            voxelPosT2.Add(posT2E2);
            voxelPosT2.Add(posT2E3);
            voxelPosT2.Add(posT2E4);

            /* if is odd */
            /* vertical poles */
            Vector3 posAO = Vector3.right * 0.05f + Vector3.forward * 0.05f;
            Vector3 posBO = Vector3.left * 0.05f + Vector3.back * 0.05f;
            Vector3 posCO = Vector3.right * 0.05f + Vector3.back * 0.05f;
            Vector3 posDO = Vector3.forward * 0.05f + Vector3.left * 0.05f;
            
            voxelPosIO.Add(posAO);
            voxelPosIO.Add(posBO);
            voxelPosIO.Add(posCO);
            voxelPosIO.Add(posDO);
            /* horizontal poles */ //ALIGNED TO Z (Y axis x(-0.05) and x0.15)
            Vector3 pos1O = Vector3.right * 0.05f + Vector3.up * 0.15f;
            Vector3 pos2O = Vector3.left * 0.05f + Vector3.down * 0.05f;
            Vector3 pos3O = Vector3.right * 0.05f + Vector3.down * 0.05f;
            Vector3 pos4O = Vector3.left * 0.05f + Vector3.up * 0.15f;
            
            voxelPosIIO.Add(pos1O);
            voxelPosIIO.Add(pos2O);
            voxelPosIIO.Add(pos3O);
            voxelPosIIO.Add(pos4O);
            /* horizontal poles */ //ALIGNED TO X (Y axis x0.05 and x(-0.15))
            Vector3 posIO = Vector3.forward * 0.05f + Vector3.up * 0.05f;
            Vector3 posIIO = Vector3.back * 0.05f + Vector3.down * 0.15f;
            Vector3 posIIIO = Vector3.forward * 0.05f + Vector3.down * 0.15f;
            Vector3 posIVO = Vector3.back * 0.05f + Vector3.up * 0.05f;
            
            voxelPosIIIO.Add(posIO);
            voxelPosIIIO.Add(posIIO);
            voxelPosIIIO.Add(posIIIO);
            voxelPosIIIO.Add(posIVO);


           // GenerateAggregation();
            // coroutine = GenerateAggregation();
            //StartCoroutine(coroutine);

        }

        //   J  O  I  N  T  S
        //for (int i = 0; i<scannedPoles.Count; i++)
        //{
        //    rigidbodyPoles.Add(scannedPoles[i].AddComponent<Rigidbody>());
        //    rigidbodyPoles[i].mass = 2700;
        //}

    }

    //void OnDrawGizmos ()
    //{

    //    for (int i = 0; i < active.Count; i++)
    //    {
    //        var iLayer = active[i] % (voxx * voxz);
    //        var x = iLayer / voxz;
    //        var z = iLayer % voxz;
    //        var y = active[i] / (voxx * voxz);
    //        Vector3 pos = new Vector3(x, y, z);
    //        Gizmos.color = new Color(1, 1, 1, 0.2f);
    //        Gizmos.DrawCube(pos, new Vector3(1, 1, 1));
    //    }
    //}




    IEnumerator GenerateAggregation()
    {

        

        for (int i = 0; i < active.Count; i++)
        {
            var iLayer = active[i] % (voxx * voxz);
            var x = iLayer / voxz;
            var z = iLayer % voxz;
            var y = active[i] / (voxx * voxz);
            

            Vector3 pos = new Vector3(x, y, z);
            var q = Quaternion.LookRotation(vectors[i]) * Quaternion.Euler(-90, 0, 0);
            var e = q.eulerAngles;
            var distance = vectors[i].magnitude;
            var direction = vectors[i] / distance;

            //CHECKING NEIGHBOURS
            Vector3 n1 = pos + Vector3.right;
            Vector3 n2 = pos + Vector3.left;
            Vector3 n3 = pos + Vector3.forward;
            Vector3 n4 = pos + Vector3.back;
            Vector3 n5 = pos + Vector3.up;
            Vector3 n6 = pos + Vector3.down;
            bool _toggleLong = false;

            if (z == 0 && ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180)))
            {

            }

                for (int l = 0; l < positions.Count; l++)
            {

                if ((n1 == positions[l] || n2 == positions[l] || n3 == positions[l] || n4 == positions[l] || n5 == positions[l] || n6 == positions[l]) && (!_toggleLong))       //checking if the neighbouring position belongs to the list of active voxels
                {
                    if (e != angles[l])  // checking if the angle of the vector of the neighbouring voxel is different from the current one
                    {
                        _toggleLong = true;
                    }
                }

            }

            Vector3 t1 = pos + Vector3.right + Vector3.forward;
            Vector3 t2 = pos + Vector3.left + Vector3.forward;
            Vector3 t3 = pos + Vector3.back + Vector3.right;
            Vector3 t4 = pos + Vector3.back + Vector3.left;

            bool _toggleType2 = false;
            for (int l = 0; l < positions.Count; l++)
            {
                if (active[l] % 2 == 0)
                {
                    if (((e.x != 0 || e.x != 180 || e.x != -180) && (e.z != 0 || e.z != 180 || e.z != -180)) && ((e.x != 90 || e.x != 270 || e.x != -90 || e.x != -270 || e.x != 90.00001 || e.x != -90.00001) && (e.y != 0 || e.y != 180 || e.y != -180)))
                    {
                        if (t1 == positions[l])
                        {

                            if ((angles[l].x != 90 || angles[l].x != 270 || angles[l].x != -90 || angles[l].x != -270 || angles[l].x != 90.00001 || angles[l].x != -90.00001) && (angles[l].y != 0 || angles[l].y != 180 || angles[l].y != -180))
                            {
                                positionsType2.Add(t1);
                            }
                        }
                        if (t2 == positions[l])
                        {

                            if ((angles[l].x != 90 || angles[l].x != 270 || angles[l].x != -90 || angles[l].x != -270 || angles[l].x != 90.00001 || angles[l].x != -90.00001) && (angles[l].y != 0 || angles[l].y != 180 || angles[l].y != -180))
                            {
                                positionsType2.Add(t2);
                            }
                        }
                        if (t3 == positions[l])
                        {

                            if ((angles[l].x != 90 || angles[l].x != 270 || angles[l].x != -90 || angles[l].x != -270 || angles[l].x != 90.00001 || angles[l].x != -90.00001) && (angles[l].y != 0 || angles[l].y != 180 || angles[l].y != -180))
                            {
                                positionsType2.Add(t3);
                            }
                        }
                        if (t4 == positions[l])
                        {

                            if ((angles[l].x != 90 || angles[l].x != 270 || angles[l].x != -90 || angles[l].x != -270 || angles[l].x != 90.00001 || angles[l].x != -90.00001) && (angles[l].y != 0 || angles[l].y != 180 || angles[l].y != -180))
                            {
                                positionsType2.Add(t4);
                            }
                        }
                    }

                }
            }

            for (int m = 0; m < positionsType2.Count; m++)
            {
                if (pos == positionsType2[m])
                {

                    type = positionsType2;
                    _toggleType2 = true;
                }
                else
                {
                    type = voxelPosIIIE;
                }
            }
            //Transform poleNew = Instantiate(pole, pos, q);
            if (active[i] % 2 == 0) //is even
            {

                if (_toggleLong == false || _toggleType2 == true)
                {
                    R = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                }
                else
                {
                    R = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                }
                if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))
                {
                    //if (z == 0)
                    //{
                    //    Rigidbody poleee = scannedPoles[R].AddComponent<Rigidbody>();
                    //     = scannedPoles[R].AddComponent(FixedJoint) ;
                    //    Rigidbody firstPole = Instantiate(rigidbodyPoles[R], pos, q);
                    //}
                    GameObject pole = Instantiate(scannedPoles[R], pos, q);
                }

                if (stress[i] > maxValue / 5 && stress[i] <= 2 * (maxValue / 5))
                {
                    for (int k = 0; k < 2; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //if (_toggleLong == true)
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}

                        //Check directionality & instantiate
                        //if ( (e.x == 90.00001 || e.x == -90.00001) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001f || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //        Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIE[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIE[k], q);
                        }
                        else if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIE[k], q);
                        }

                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + type[k], q);
                        }
                    }
                }
                else if (stress[i] > 2 * (maxValue / 5) && stress[i] <= 3 * (maxValue / 5))
                {
                    for (int k = 0; k < 3; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //else
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}
                        //Check directionality & instantiate
                        //if ( (e.x == 90.00001 || e.x == -90.00001) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001 || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //    Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIE[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIE[k], q);
                        }
                        else if ((e.x == 0 || q.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIE[k], q);
                        }
                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + type[k], q);
                        }
                    }
                }
                else if (stress[i] > 3 * (maxValue / 5) && stress[i] <= 4 * (maxValue / 5))
                {
                    for (int k = 0; k < 4; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //else
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}
                        //Check directionality & instantiate
                        //if (  (e.x == 90.00001 || e.x == -90.00001) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001 || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //    Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIE[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIE[k], q);
                        }
                        else if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIE[k], q);
                        }
                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + type[k], q);
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < 5 - 1; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //else
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}
                        //Check directionality & instantiate
                        //if (  (e.x == 90.00001 || e.x == -90.00001 || e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001 || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //    Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIE[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIE[k], q);
                        }
                        else if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIE[k], q);
                        }
                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + type[k], q);
                        }
                    }
                }
            }
            else //if (i%2==1) //is odd
            {
                if (stress[i] > (maxValue / 4) && stress[i] <= (2 * (maxValue / 4)))
                {
                    for (int k = 0; k < 2; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //else
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}

                        //Check directionality & instantiate
                        //if ( (e.x == 90.00001 || e.x == -90.00001) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001 || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //    Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIO[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIO[k], q);
                        }
                        else if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIO[k], q);
                        }
                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIIO[k], q);
                        }
                    }
                }
                else if (stress[i] > (2 * (maxValue / 4)) && stress[i] <= (3 * (maxValue / 4)))
                {
                    for (int k = 0; k < 2; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //else
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}
                        //Check directionality & instantiate
                        //if ( (e.x == 90.00001 || e.x == -90.00001) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001 || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //    Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIE[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIO[k], q);
                        }
                        else if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIO[k], q);
                        }
                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIIO[k], q);
                        }
                    }
                }
                else if (stress[i] > (3 * (maxValue / 4)) && stress[i] <= (4 * (maxValue / 4)))
                {
                    for (int k = 0; k < 3; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //else
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}
                        //Check directionality & instantiate
                        //if (  (e.x == 90.00001 || e.x == -90.00001) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001 || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //    Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIE[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIO[k], q);
                        }
                        else if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIO[k], q);
                        }
                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIIO[k], q);
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < 4; k++)
                    {
                        //if (_toggleLong == false)
                        //{
                        rr = UnityEngine.Random.Range(0, Mathf.RoundToInt(scannedPoles.Count / 2));
                        //}
                        //else
                        //{
                        r = UnityEngine.Random.Range(Mathf.RoundToInt(scannedPoles.Count / 2), scannedPoles.Count);
                        //}
                        //Check directionality & instantiate
                        //if ( (e.x == 90.00001 || e.x == -90.00001) && (e.z == 90 || e.z == 270 || e.z == -90 || e.z == -270 || e.z == 90.00001 || e.z == -90.00001)) // HORIZONTAL POLE, ALIGNED TO X
                        //{
                        //    Transform pole = Instantiate(scannedPoles[r], pos + voxelPosIIIE[k], q);
                        //}
                        if ((e.x == 90 || e.x == 270 || e.x == -90 || e.x == -270 || e.x == 90.00001 || e.x == -90.00001) && (e.y == 0 || e.y == 180 || e.y == -180) /* && (q.z == 0 || q.z == 180 || q.z == -180) */) // HORIZONTAL POLE, ALIGNED TO Z
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIO[k], q);
                        }
                        else if ((e.x == 0 || e.x == 180 || e.x == -180) && (e.z == 0 || e.z == 180 || e.z == -180))  //VERTICAL POLE
                        {
                            GameObject pole = Instantiate(scannedPoles[r], pos + voxelPosIO[k], q);
                        }
                        else
                        {
                            GameObject pole = Instantiate(scannedPoles[rr], pos + voxelPosIIIO[k], q);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.001f);
        }
}
    
   


    void Update()
    {
        foreach(var pos in positions)
        {
            var matrix = Matrix4x4.Translate(pos);
            Graphics.DrawMesh(_box, matrix, _material, 0);
        }

        if (Input.GetKeyDown("space"))
        {
           // GenerateAggregation();
        }
    }

    void OnGUI()
    {
        coroutine = GenerateAggregation();
        if (GUILayout.Button("Generate Aggregation"))
        {
            StartCoroutine(coroutine);
        }
    }
}
