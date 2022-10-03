using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSmoothMover : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    private float speed;
    public float timeToArrive = 3f;
    private bool moving;

    // Start is called before the first frame update
    void OnEnable()
    {
        speed = Vector3.Distance(startPos, endPos) / timeToArrive;
    }

    public void StartMoving()
    {
        moving = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        }
    }
}
