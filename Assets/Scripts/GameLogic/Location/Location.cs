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
    public Action<Location> onSelected;
     

    private void Start()
    {
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
        onEntered+=FindObjectOfType<ArrowsManager>().UpdateArrows;
        
    }

    public Transform GetView() => view; 
    public List<NearLocation> GetNearLocations() => nearLocations;



    public override void Select()
    {
        selected = true;
        onSelected?.Invoke(this);
        //Debug.Log("Selected"); 
        //selected = true;
        //gameObject.layer = LayerMask.NameToLayer("Default"); 
    }

    public override void Unselect() => base.Unselect();  
    public override void EnableOutline() => base.EnableOutline(); 
    public void OnMouseExit() => base.DisableOutline();


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
