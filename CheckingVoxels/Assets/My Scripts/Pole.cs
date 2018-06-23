using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pole : MonoBehaviour   
{
    public List<Vector3> Nodes;
    List<Vector3[]> NodePoints = new List<Vector3[]>();
    List<Vector3> startOfNode = new List<Vector3>();
    List<Vector3> endOfNode = new List<Vector3>();

    List<Vector3> startOfInternode = new List<Vector3>();
    List<Vector3> endOfInternode = new List<Vector3>();
    List<Vector3[]> InterNodeArea = new List<Vector3[]>();

    void Start()
    {
        SetNodes(Nodes);
    }

    public void SetNodes(List<Vector3> nodes)
    {
        Nodes = nodes;
    }

    public int GetJointCount()
    {
        int num = Nodes.Count -1;
        return num;
    }



    public List<Vector3[]> GetNodeArea()
    {
        
        Vector3 minusNode;
        Vector3 plusNode;
        for (int i = 0; i < Nodes.Count; i++)
        {
            minusNode = Nodes[i] - new Vector3(0, 0.025f, 0);
            startOfNode.Add(minusNode);
            
            plusNode = Nodes[i] + new Vector3(0, 0.025f, 0);
            endOfNode.Add(plusNode);
            Vector3 [] twopoints = new Vector3[] { minusNode, plusNode };
            NodePoints.Add(twopoints);
        }
        return NodePoints;
    }

    public List<Vector3[]> GetInterNodeArea() 
    {
        
        for (int ii = 0; ii <= (Nodes.Count); ii++)
        {
            int i = ii + 1;
            Vector3 starting = (Vector3) NodePoints[i - 1].GetValue(1);
            Vector3 ending = (Vector3)NodePoints[i].GetValue(0);
            if ((ending - starting).sqrMagnitude >= 0.07f) //0.07f is the width of the joint
            {
                Vector3[] twopoints = new Vector3[] { starting, ending };
                InterNodeArea.Add(twopoints);
            }
        }
        return InterNodeArea;
    }

}



//public class Poles : MonoBehaviour
//{
//    List<Vector3> poles = new List<Vector3>();
//    List<Vector3[]> nodes = new List<Vector3[]>();

//    Vector3[,] poles;


//    void Start()
//    {
//        for (int z = 0; z < 10; z++)
//        {

//            int numberOfNodes = Random.Range(2, 7);
//            Vector3 startPos = new Vector3(0, 0, z);
//            poles.Add(startPos);
//            Vector3 nodePos = startPos;
//            for (int i = 0; i < numberOfNodes; i++)
//            {

//                /*poles[z].*/
//                nodes[i].Add(startPos + Vector3(0, nodePos, 0));
//                float distanceOfNodes = Random.Range(0.1f, 1f);
//                nodePos += Vector3(0, distanceOfNodes, 0);
//            }
//        }

//    }


//    void Update()
//    {

//    }
//}
