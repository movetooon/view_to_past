using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    [SerializeField] private NearLocation.Direction direction;
    private Location nextLoc;  
    private Action<Location> onPressed;

    private void Start()
    { 
        onPressed+=(FindObjectOfType<Player>().EnterMovingState);
 
    }


    private void OnEnable()
    {
        //Debug.Log("from arrow " + name + " | " + nextLoc.transform.position.ToString());
    }

    public void Press() => onPressed?.Invoke(nextLoc);


    public void SetNextLocation(Location next)
    {
        nextLoc = next;
    }

    public NearLocation.Direction GetDirection()=> direction;

}
