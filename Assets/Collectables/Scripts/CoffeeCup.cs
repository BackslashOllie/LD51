using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : Collectable
{
    private void Update()
    {
        anchor.transform.position = _rend.bounds.center + (Vector3.up * 0.3f);
    }
}
