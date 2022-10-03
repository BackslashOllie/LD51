using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour, IComparable<Marker>
{
    public RectTransform rectTransform;
    public TextMeshProUGUI text;
    public Image colour;
    public MarkerTrigger target;
    public Animator anim;

    #region Public Methods
    public void Set(string name, string description, MarkerTrigger trigger)
    {
        text.text = name;
        if (description.Length > 0) text.text += "\r\n " + description;
        target = trigger;
        trigger.marker = this;
    }

    public void ChangeText(string t)
    {
        text.text = t;
    }

    public void ShowMarker()
    {
        anim.SetBool("Shown", true);
    }

    public void HideMarker()
    {
        anim.SetBool("Shown", false);
    }

    #endregion

    #region Unity Callbacks

    private void FixedUpdate()
    {
        
        if (target)
        {
            // convert screen coords
            Vector2 adjustedPosition = MarkersManager.Instance.camera.WorldToScreenPoint(target.anchor.transform.position);
            
            float h = MarkersManager.Instance.canvas.pixelRect.height;
            float w = MarkersManager.Instance.canvas.pixelRect.width;
            float x = adjustedPosition.x - (w / 2) - Screen.width * MarkersManager.Instance.camera.rect.position.x;
            float y = adjustedPosition.y - (h / 2) - Screen.height * MarkersManager.Instance.camera.rect.position.y;
            float s = MarkersManager.Instance.canvas.scaleFactor;

            rectTransform.anchoredPosition = new Vector2(x, y) / s;
        }
    }

    #endregion

    #region IComparable members

    public int CompareTo(Marker other)
    {
        // A null value means that this object is greater.
        if (other == null)
            return 1;
        else
            return other.rectTransform.anchoredPosition.y.CompareTo(this.rectTransform.anchoredPosition.y);
    }

    #endregion
}
