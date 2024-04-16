using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 [ExecuteInEditMode]
public class Debuging : MonoBehaviour
{
    [SerializeField] Location[] locations;

 
    private void Update()
    {

        locations = FindObjectsOfType<Location>();

        foreach (var location in locations)
        {
            foreach (var nearLoc in location.GetNearLocations())
            {
                try { 

                    Debug.DrawLine(location.GetView().position, nearLoc.GetLocation().GetView().position);
                }
                catch(Exception e)
                {

                }
               
            }
        }
    }
}
