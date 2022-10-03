using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRelay : MonoBehaviour
{
    public GameEvent gameEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameEvent.TriggerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameEvent.TriggerExit();
        }    
    }
}
