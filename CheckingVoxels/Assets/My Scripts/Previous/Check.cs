using System.Collections;
//using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




public class Check : MonoBehaviour
{
    public List<Transform> positions = new List<Transform>();
    public Transform pole;
    List<float> distances = new List<float>();
    public List<Transform> poles = new List<Transform>();
    public List<Transform> referencePoles = new List<Transform>();
    public Transform referencePole;
    public List<int> values = new List<int>();
    List<Vector3> pos = new List<Vector3>();
    bool isEven = true;

    void Start()
    {
        List<float> heights = new List<float>();
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < positions.Count; i++)
        {
            float dist = Vector3.Distance(positions[i].position, pole.position);
            distances.Add(dist);
            
        }

        var height = poles.Select(t => t.GetComponent<Renderer>().bounds.size.y).ToArray(); //Array of heights

        float minValue = Mathf.Min(distances.ToArray());
        int index = distances.IndexOf(minValue);
        Debug.Log("index :" + index + ", minValue : " + minValue);

        //Transform pole1 = Instantiate(pole, positions[index].position, positions[index].rotation); // nearest // test with one pole
        //Transform pole2 = Instantiate(pole, pole.position, positions[index].rotation);  //
        int index2 = Random.Range(0, poles.Count);
        //Transform pole1 = Instantiate(poles[index2], positions[index].position, positions[index].rotation);
       // Transform pole2 = Instantiate(pole, pole.position, positions[index].rotation);
        heights.Add(height[index2]);
        for (int i = 1; i< 3; i++)
        {
            int index3 = Random.Range(0, poles.Count);
            //if (height[index3] > heights[i-1])
            //{
            //    //Instantiate in certain position
            //}
            //else if()
            //{

            //}
            //else
            //{

            //}

        }
        for (int i = 0; i < referencePoles.Count; i++)
        {
            pos.Add(referencePoles[i].position);

        }
        //Vector3 pos = referencePole.position;
       // Transform poleNew = Instantiate(pole, pos, referencePole.rotation);
        Vector3 posA =  (Vector3.right * 0.1f);    //Vector3 posA = new Vector3(pos.x + 0.1f, pos.y , pos.z);
        Vector3 posB = (Vector3.left * 0.1f);     // Vector3 posB = new Vector3(pos.x - 0.1f, pos.y, pos.z);
        Vector3 posC = (Vector3.forward * 0.1f);  //Vector3 posC = new Vector3(pos.x, pos.y, pos.z + 0.1f);
        Vector3 posD = (Vector3.back * 0.1f);     //Vector3 posD = new Vector3(pos.x, pos.y, pos.z - 0.1f);
        List<Vector3> voxelPosI = new List<Vector3>();
        voxelPosI.Add(posA);
        voxelPosI.Add(posB);
        voxelPosI.Add(posC);
        voxelPosI.Add(posD);

        Vector3 pos1 = (Vector3.right * 0.05f);    //Vector3 posA = new Vector3(pos.x + 0.1f, pos.y , pos.z);
        Vector3 pos2 = (Vector3.left * 0.05f);     // Vector3 posB = new Vector3(pos.x - 0.1f, pos.y, pos.z);
       // Vector3 pos3 = (Vector3.forward * 0.1f);  //Vector3 posC = new Vector3(pos.x, pos.y, pos.z + 0.1f);
       // Vector3 pos4 = (Vector3.back * 0.1f);    
        Vector3 pos3 = Vector3.right * 0.05f +Vector3.down * 0.1f;
        Vector3 pos4 = Vector3.left * 0.05f  + Vector3.down * 0.1f;   

        List<Vector3> voxelPosII = new List<Vector3>();
        voxelPosII.Add(pos1);
        voxelPosII.Add(pos2);
        voxelPosII.Add(pos3);
        voxelPosII.Add(pos4);

        Vector3 posI = Vector3.forward * 0.1f + Vector3.up * 0.05f;    //Vector3 posA = new Vector3(pos.x + 0.1f, pos.y , pos.z);
        Vector3 posII = Vector3.back * 0.1f + Vector3.up * 0.05f;    // Vector3 posB = new Vector3(pos.x - 0.1f, pos.y, pos.z);
        Vector3 posIII = Vector3.forward * 0.1f + Vector3.down * 0.05f;  //Vector3 posC = new Vector3(pos.x, pos.y, pos.z + 0.1f);
        Vector3 posIV = Vector3.back * 0.1f + Vector3.down * 0.05f;     //Vector3 posD = new Vector3(pos.x, pos.y, pos.z - 0.1f);
        List<Vector3> voxelPosIII = new List<Vector3>();
        voxelPosIII.Add(posI);
        voxelPosIII.Add(posII);
        voxelPosIII.Add(posIII);
        voxelPosIII.Add(posIV);



        //for (int i = 0; i < 4; i++)

        //{
        //    Transform poleNew2 = Instantiate(pole, voxelPos[i], referencePole.rotation);
        //}

        for (int i = 0; i < referencePoles.Count; i++)
        {
            if (referencePoles[i].rotation.z != 0)
            {
                for (int y = 0; y < values[i]; y++)
                {
                    int r = Random.Range(0, poles.Count);
                    Transform poleNew2 = Instantiate(poles[r], pos[i]+voxelPosIII[y], referencePoles[i].rotation);
                }
            }
            if (referencePoles[i].rotation.x != 0)
            {
                for (int y = 0; y < values[i]; y++)
                {
                    int r = Random.Range(0, poles.Count);
                    Transform poleNew2 = Instantiate(poles[r], pos[i]+ voxelPosII[y], referencePoles[i].rotation);
                }
            }
            if (referencePoles[i].rotation.x ==0 & referencePoles[i].rotation.y == 0 & referencePoles[i].rotation.z == 0)
            {
                
                if (referencePoles[i+1].rotation.x == 0 & referencePoles[i+1].rotation.y == 0 & referencePoles[i+1].rotation.z == 0 & isEven)
                for (int y = 0; y < values[i]; y++)
                {
                    if (y==0)
                    {
                            int r = Random.Range(0, poles.Count);
                            Instantiate(poles[r], pos[i], referencePoles[i].rotation);
                    }
                    Transform poleNew2 = Instantiate(poles[index], pos[i]+voxelPosI[y], referencePoles[i].rotation);
                    isEven = false;
                }
                else if (referencePoles[i + 1].rotation.x == 0 & referencePoles[i + 1].rotation.y == 0 & referencePoles[i + 1].rotation.z == 0 & !isEven)
                {
                    for (int y = 0; y < values[i]; y++)
                    {
                        int r = Random.Range(0, poles.Count);
                        Transform poleNew2 = Instantiate(poles[r], pos[i] + voxelPosI[y], referencePoles[i].rotation);
                    }
                    isEven = true;
                }
                else
                {
                    for (int y = 0; y < values[i]; y++)
                    {
                        int r = Random.Range(0, poles.Count);
                        Transform poleNew2 = Instantiate(poles[r], pos[i] + voxelPosI[y], referencePoles[i].rotation);
                    }
                }
            }
        }
        //float minVal = distances.Min();
        //int index = distances.IndexOf(minVal);
        //Debug.Log("index : " + index);
        //for (int i = 0; i < distances.Count; i++)
        //{
        //    Debug.Log(Mathf.Min(distances.Count));
        //}


    }



    void Update()
    {
       

    }


}