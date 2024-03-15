using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;


public class IdleState : State<Player>
{ 
    private Location currentLocation; 


    public IdleState(Player SM, Location _currentLocation) : base(SM)
    {
        currentLocation = _currentLocation;
         
    }
    public override void Enter()
    {
        //Debug.Log("Enter idle");  
    }

    public override void Update()
    { 
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
