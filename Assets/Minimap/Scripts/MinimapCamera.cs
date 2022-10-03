using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform cameraRoot;
    public Camera minimapCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minimapCamera.transform.LookAt(transform, cameraRoot.forward);
    }
}
