using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Location : Selectable
{
    [SerializeField] private Transform view;
    [SerializeField] private List<NearLocation> nearLocations;
  
    public Action<List<NearLocation>> onEntered;

    private bool selected;

    private void Start()
    {
        onEntered+=FindObjectOfType<ArrowsManager>().UpdateArrows;
        
    }

    public Transform GetView()
    {
        return view; 
    }

    public List<NearLocation> GetNearLocations() 
    {
        return nearLocations;    
    }


    public override void Select()
    {
        Debug.Log("Selected"); 
        selected = true;
        gameObject.layer = LayerMask.NameToLayer("Default"); 
    }

    public override void Unselect()
    {
        Debug.Log("Unselect");
        selected = false; 
    }


    public override void OnMouseEnter()
    {
        if (selected == false)
            gameObject.layer = LayerMask.NameToLayer("Outline");
    }
   
    public override void OnMouseExit()
    {
        if (selected == false) 
            gameObject.layer = LayerMask.NameToLayer("Default");

    }

}

[System.Serializable]
public class NearLocation
{
    [SerializeField] Location location;
    [SerializeField] Direction direction;
     
    public Location GetLocation()
    {
        return location;
    }
    public Direction GetDirection()
    {
        return direction;
    }

    public enum Direction
    {
        up,
        right,
        down,
        left
    }
}
