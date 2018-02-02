using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
    public Transform bottle;
    public Transform boomerang;
    public Transform legoBrick;
    public Transform branch;
    public List<Transform> targets;

    

    void Start ()
    {
        targets.Add(bottle);
        targets.Add(boomerang);
        targets.Add(legoBrick);
        targets.Add(branch);
    }
	
	
	void Update ()
    {



        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < 4; i++)
            {

                //float angleX = -targets[i].transform.eulerAngles.x;
                //float angleZ = -targets[i].transform.eulerAngles.z;
                //float angleY = -targets[i].transform.eulerAngles.y;
                targets[i].position = new Vector3(0, 0, 0 + (i * 1.5f));
                targets[i].rotation = Quaternion.identity;
                //targets[i].Rotate(Vector3.right, angleX, Space.Self);
                //targets[i].Rotate(Vector3.forward, angleZ, Space.Self);
                //targets[i].Rotate(Vector3.up, angleY, Space.Self);
            }
        }

    }

    

}
