using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTrigger : MonoBehaviour
{
    
    public string objectName, objectDescription;
    public Marker markerPrefab;
    public Transform anchor;
    public Marker marker;
    protected Renderer _rend;

    // Start is called before the first frame update
    protected void Start()
    {
        _rend = GetComponentInChildren<Renderer>();
        
        marker = Instantiate(markerPrefab, MarkersManager.Instance.transform);
        marker.Set(objectName, objectDescription, this);
        MarkersManager.Instance.AddMarker(marker);
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_rend.isVisible)
            {
                marker.ShowMarker();
            }
            else
                marker.HideMarker();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            marker.HideMarker();
        }
    }
}
