using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{
    public GameObject[] gridElements;
    Animator anim;
    float rotSpeed = 0.8f;
    float speed = 3f;
    public GameObject pole;
    int currentGE = 0;
    float accuracyGE = 0.1f;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        currentGE= FindClosestGridElement();
        anim.SetBool("isWalking", true);
    }

    int FindClosestGridElement()
    {
        if (gridElements.Length == 0) return -1;
        int closest = 0;
        float lastDist = Vector3.Distance(pole.transform.position, gridElements[0].transform.position);
        for (int i =1; i < gridElements.Length; i++)
        {
            float  thisDist = Vector3.Distance(pole.transform.position, gridElements[i].transform.position);
            if ( lastDist > thisDist && i != currentGE)
            {
                currentGE = i;

            }
        }
        return closest;
    }

    void Update ()
    {
        
        Vector3 direction = gridElements[currentGE].transform.position - pole.transform.position;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, Time.deltaTime * speed);

        if (direction.magnitude < accuracyGE)
        {
            currentGE = FindClosestGridElement();
        }

    }
}
