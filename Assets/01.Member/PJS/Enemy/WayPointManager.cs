using Assets._04.Core;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoSingleton<WayPointManager>
{
    [HideInInspector] public List<Transform> waypoints = new List<Transform>();
    void Awake()
    {
        CollectWaypoints();
    }
    private void CollectWaypoints()
    {
        waypoints.Clear(); 

        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }
    }
}
