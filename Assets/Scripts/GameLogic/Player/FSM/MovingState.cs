using UnityEngine;
using UnityEngine.Events;

public class MovingState : State<Player>
{  
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Location nextLocation;
    private Transform nextTransform;

    //public delegate void OnEteredHandle(Location nextLoc);
    //public OnEteredHandle onEntered;

    private AnimationCurve speedChange;
    private float speed;

    private float lerp = 0;
     

    public MovingState(Player SM , Location _nextLocation , AnimationCurve _speedChange,float _speed):base(SM)
    {
        speed = _speed; 
        nextLocation = _nextLocation; 
        speedChange = _speedChange; 
    }
     

    public override void Enter()
    { 
        currentPos = SM.transform.position; 
        currentRot = SM.transform.rotation;
        nextTransform = nextLocation.GetView();

        Debug.Log("Enter moving ");
    }

    public override void Exit()
    {
         Debug.Log("Exit moving"); 
        SM.transform.position = nextTransform.position;
        SM.transform.rotation = nextTransform.rotation;
        lerp = 0;
    }

    public override void Update()
    {
        //Debug.Log("Update moving");
         
        lerp += speed * Time.fixedDeltaTime ;

        SM.transform.position = Vector3.Lerp(currentPos, nextTransform.position, speedChange.Evaluate(lerp));
        SM.transform.rotation = Quaternion.Lerp(currentRot, nextTransform.rotation, speedChange.Evaluate(lerp)); 

        if (lerp >= 1)
        {
            //Debug.Log("End transform");
            lerp = 0;
            
            nextLocation.onEntered(nextLocation.GetNearLocations()); 
            SM.EnterIn<IdleState>();
             
        }
          
    }

    public void UpdateNextLocation(Location newLoc)
    {
        nextLocation = newLoc;
    }
}
