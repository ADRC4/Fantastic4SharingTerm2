using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public GameObject area;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float angle = 8 * Time.deltaTime;
        this.transform.RotateAround(Vector3.zero, Vector3.up, angle);
    }
}