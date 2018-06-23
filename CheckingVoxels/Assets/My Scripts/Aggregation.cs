using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Aggregation : MonoBehaviour 
{
    public Mesh mesh;
    public Material material;
    public List<Pole> Buffer;
    public List<Pole> Structure; // Poles in Structure
    int nextPoleInternodeIndex; //AddNextPole
    int structureInternodeIndex; //AddNextPole
    Pole nextPole;
    float distanceA; //CalculateOverlappingPercentage
    float distanceB; //CalculateOverlappingPercentage
    int index; //index of the nextpole
    List<int> NodesInCurrent = new List<int>();
    List<float> OverlappingPercentage = new List<float>();

    // V O X E L S
    int voxx = 13;
    int voxy = 15;
    int voxz = 15;

    public Aggregation()
    {
        // Aggregation = CreateRandomPoles(3, 0.5f, 2.0f, 3, 8);
    }

    void AddNextPole() //Edit
    {
        int maxCount = 0;
        List<int> NextPoleIndexStore = new List<int>();
        List<int> StructurePoleIndexStore = new List<int>();
        List<List<int>>joints= new List<List<int>>();
        List<int> sum = new List<int>();

        foreach (var pole in Buffer)
        {
            bool firstJoint = false;
            List<int> jointsPerPole = new List<int>();
            for (int ii = 0; ii <= (pole.Nodes.Count); ii++)
            {
                int i = ii + 1;
                //nextPole = pole;
                
                var nextPoleNodeArea = pole.GetComponent<Pole>().GetInterNodeArea();
                for (int j = 0; j < Structure[Structure.Count - 1].Nodes.Count; j++)
                {
                    if ((InterNodeArea[i].GetValue[0].x >= Structure[Structure.Count - 1].GetInterNodeArea[j].GetValue[0].x) && (InterNodeArea[i].GetValue[1].x <= Structure[Structure.Count - 1].GetInterNodeArea[j].GetValue[1].x))
                    {
                        jointsPerPole.Add(i);
                    }
                    if (!firstJoint)
                    {
                        firstJoint = true;
                        nextPoleInternodeIndex = i;
                        structureInternodeIndex = j;
                        NextPoleIndexStore.Add(nextPoleInternodeIndex);
                        StructurePoleIndexStore.Add(structureInternodeIndex);
                        //DrawMesh
                    }
                }
            }
            joints.Add(jointsPerPole);
            for (int i=0; i<joints.Count; i++)
            {
                if (joints[i].Count > maxCount)
                {
                    maxCount = joints[i].Count;
                }
            }
        }
        for (int i =0; i<Buffer.Count; i++)
        {
            int minIndexA = NextPoleIndexStore[i];
            int minIndexB = StructurePoleIndexStore[i];
            sum.Add(minIndexA + minIndexB);
        }
        int min = 100;
        for (int i = 0; i < sum.Count; i++)
        {
            if (sum[i] < min)
            {
                min = sum[i];
                index = i;
            }
        }
        nextPole = Buffer[index];
        Buffer.Remove(nextPole);
        ScanPole(); //
        //nextPole.transform
        nextPole.transform.position = new Vector3 (x,z,y); // is the position based on the pattern condition inside MainController script
        //logic to move the pole to position;

        Structure.Add(nextPole);
        
    }
    
    void CalculateOverlappingPercentage()
    {
        float overlappingPercentage = new float();
        for (int i = nextPoleInternodeIndex; i < nextPole.Nodes.Count; i++) //inherits from AddNextPole
        {
            distanceA = Mathf.Abs(InterNodeArea[i].GetValue[1].magnitude - InternodeArea[i].GetValue[0].magnitude);
            
            int j = structureInternodeIndex;
            if(j < Structure[Structure.Count - 1].Nodes.Count)
            {
                distanceB = Mathf.Abs(Structure[Structure.Count-1].InterNodeArea[j].GetValue[1].magnitude - Structure[Structure.Count - 1].InternodeArea[j].GetValue[0].magnitude);
                j++;
            }
            overlappingPercentage = 100 * ( distanceA / distanceB);

        }
        OverlappingPercentage.Add(overlappingPercentage);
    }

    public void ScanPole()
    {
        var pole = CreateRandomPole(0.5f, 2.0f, 3, 8);
        Buffer.Add(pole);
    }

    Pole CreateRandomPole(float minLength, float maxLength, int minNodes, int maxNodes)
    {
        var nodeCount = Random.Range(minNodes, maxNodes);
        var nodes = new List<Vector3>();

        for (int i = 0; i < nodeCount; i++)
        {
            nodes.Add(new Vector3(0, Random.Range(minLength, maxLength), 0));
        }

        //  return new Pole(nodes);
        var pole = GameObject.Instantiate(polePrefab);
        var poleScript = pole.GetComponent<Pole>();
        poleScript.SetNodes(nodes);
    }

    List<Pole> CreateRandomPoles(int poleCount, float minLength, float maxLength, int minNodes, int maxNodes)
    {
        var poles = new List<Pole>();
        for (int i = 0; i < poleCount; i++)
        {
            poles.Add(CreateRandomPole(minLength, maxLength, minNodes, maxNodes));
        }
        return poles;
    }

    void CheckIfInCurrentVoxel()
    {
        Pole current = nextPole;
        int numberOfNodes = 0;
        for (int i = 0; i<nextPole.Nodes.Count; i++)
        {
            //var iLayer = active[i] % (voxx * voxz);
            //var x = iLayer / voxz;
            //var z = iLayer % voxz;
            //var y = active[i] / (voxx * voxz);
            //BOUNDS
            float half = 0.5f;
            if ((current.Nodes[i].x > x- half) && (current.Nodes[i].x < x + half) && (current.Nodes[i].z > z - half) && (current.Nodes[i].z < z + half) && (current.Nodes[i].y > y - half) && (current.Nodes[i].y < y + half))
            {
                numberOfNodes++;
            }
        }
        NodesInCurrent.Add(numberOfNodes);
    }

    void DrawMesh()
    {
        Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
    } 

     void Start()
    {
        for (int i =0; i<3; i++)
        {
            ScanPole();
        }
    }

    void Update()
    {
        DrawMesh();
    }

}
