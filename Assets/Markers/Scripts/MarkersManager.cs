using System.Collections.Generic;
using UnityEngine;

public class MarkersManager : Singleton<MarkersManager>
{
    public Camera camera;
    public Canvas canvas;

    private bool _initialised;
    private List<Marker> _markers = new List<Marker>();
    private float _showTimer;
    private bool _shown;

    #region Public Methods

    public void Init()
    {
        
        _initialised = true;
    }

    public void AddMarker(Marker marker)
    {
        _markers.Add(marker);
    }

    public void ShowAllMarkers(float duration)
    {
        _markers.Sort();
        for (int i = 0; i < _markers.Count; i++)
        {
            _markers[i].transform.SetSiblingIndex(i);
        }
        if (_initialised)
        {
            _shown = true;
            _showTimer = duration;
            foreach (var marker in _markers)
            {
                marker.anim.SetBool("Shown", true);
            }
        }
    }

    public void HideAllMarkers()
    {
        foreach (var marker in _markers)
        {
            marker.anim.SetBool("Shown", false);
        }
    }

    #endregion
}