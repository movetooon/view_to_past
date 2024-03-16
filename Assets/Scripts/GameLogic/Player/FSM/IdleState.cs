using System;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;


public class IdleState : State<Player>
{ 
    private Location currentLocation;
    private Quaternion origRot;
    
    Vector3 offset;
    private float lookingAroundStrength;

    public IdleState(Player SM, Location _currentLocation, float _lookingAroundStrength) : base(SM)
    {
        currentLocation = _currentLocation;
        lookingAroundStrength = _lookingAroundStrength;
         
    }
    public override void Enter()
    {
        origRot= SM.transform.rotation;
         
        //Debug.Log("Enter idle");  
    }

    public override void Update()
    { 
        offset = (Input.mousePosition / new Vector2(Screen.width, Screen.height)) - Vector2.one*0.5f;
        float x = offset.x;
        offset.x = -offset.y;
        offset.y = x;


        SM.transform.rotation = Quaternion.Lerp(
            SM.transform.rotation,
            Quaternion.Euler(origRot.eulerAngles + offset * lookingAroundStrength)
            ,0.01f);

        var hit = UserInput.GetMouseHitOnScreen();
        hit.transform?.GetComponent<Selectable>()?.EnableOutline();

        if (UserInput.GetMouseClick())
            hit.transform?.GetComponent<Selectable>()?.Select();  
    }
 

    public void MoveToNextLocation(Location loc)
    { 
        currentLocation.Unselect();
        if (Vector3.Distance(currentLocation.transform.position,SM.transform.position)>0.1) 
            SM.EnterMovingState(loc); 
    }

    public override void Exit() 
    {
       //Debug.Log("Exit Idle"); 
    }

    public void UpdateCurrentLocation(Location newLoc)
    {
        currentLocation = newLoc;
    }
}
