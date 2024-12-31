using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField] Transform cameraPosision;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosision.position;    
    }
}
