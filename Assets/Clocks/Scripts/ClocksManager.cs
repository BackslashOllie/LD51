using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClocksManager : Singleton<ClocksManager>
{
    public Clock[] clocks;

    private void Start()
    {
		clocks = FindObjectsOfType<Clock>();
    }

    private void Update()
    {
	    string timeString = "Next Event 00:" + (GameEventManager.Instance.secondsRemaining.ToString().Length == 1 ? "0" : "") + GameEventManager.Instance.secondsRemaining;
	    foreach (Clock clock in clocks)
	    {
		    if (clock.marker) clock.marker.ChangeText(timeString);
	    }
    }
}