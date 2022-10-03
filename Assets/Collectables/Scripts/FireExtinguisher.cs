using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Collectable
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    protected void Start()
    {
        base.Start();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }
    
    private void Update()
    {
        anchor.transform.position = _rend.bounds.center + (Vector3.up * 0.3f);
    }

    private void OnDestroy()
    {
        if (RespawnManager.Instance) RespawnManager.Instance.SpawnNewExtinguisher(_startPosition, _startRotation);
    }
}
