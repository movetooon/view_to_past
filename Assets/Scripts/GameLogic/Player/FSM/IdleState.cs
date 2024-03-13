using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;


public class IdleState : State<Player>
{
    private Transform currentTransform;
    private Location currentLocation;
      

    public IdleState(Player SM, Location _currentLocation) : base(SM)
    {
        currentLocation = _currentLocation;
         
    }
    public override void Enter()
    {
        Debug.Log("Enter idle");
        currentTransform = SM.transform;  
        
    }

    public override void Update()
    {
        //Debug.Log("Update idle");  
        var hit = UserInput.GetMouseHitOnScreen();

        if (UserInput.GetMouseClick())
            hit.transform?.GetComponent<Selectable>()?.Select();

        hit.transform?.GetComponent<Selectable>()?.EnableOutline();
        //TryGetItem(hit.transform);
        //TryStartMoving(hit.transform);
          
    }

    /*
    private void TryGetItem(Transform hit)
    {
        bool isItem = hit.transform.tag == "Item" && UserInput.GetMouseClick();
        if (isItem == false) return;

        hit.GetComponent<Item>().Select();
    }
  

    private void TryStartMoving(Transform hit)
    {
        bool notCurrentLocation = Vector3.Distance(hit.position, currentTransform.position) > 0.01f;
        bool canStartMoving = UserInput.GetMouseClick() &&
            hit.transform.tag == "Location" &&
            notCurrentLocation;

        if (canStartMoving == false) return;

        currentLocation.Unselect();
         
        var nextLocation = hit.GetComponent<Location>();
        UpdateCurrentLocation(nextLocation);
        nextLocation.Select();

        //currentTransform.GetComponent<Player>().EnterMovingState(nextLocation);
        SM.EnterMovingState(nextLocation);
         

    }
    */

    public void MoveToNextLocation(Location loc)
    {
        //if (currentLocation != loc) 
        currentLocation.Unselect();
        if (Vector3.Distance(currentLocation.transform.position,SM.transform.position)>0.1) 
            SM.EnterMovingState(loc); 
    }

    public override void Exit() 
    {
       Debug.Log("Exit Idle");

    }

    public void UpdateCurrentLocation(Location newLoc)
    {
        currentLocation = newLoc;
    }
}
