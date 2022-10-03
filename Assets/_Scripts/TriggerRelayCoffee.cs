using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRelayCoffee : MonoBehaviour
{
    public CoffeeSpwanerInput spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawner.TriggerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawner.TriggerExit();
        }    
    }
}
