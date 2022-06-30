using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akane_CameraScript : MonoBehaviour
{
    public Transform Playerposition;
    private Vector3 cameraoffset;
    void Start()
    {
        cameraoffset = Playerposition.position - transform.position;
    }
    void Update()
    {
        Vector3 newpos = Playerposition.position + cameraoffset;
        transform.position = newpos;
        
    }
}
