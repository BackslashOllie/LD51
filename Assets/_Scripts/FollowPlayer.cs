using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform followTarget;

    public void Update()
    {
        transform.position = followTarget.transform.position;
        
    }
}
