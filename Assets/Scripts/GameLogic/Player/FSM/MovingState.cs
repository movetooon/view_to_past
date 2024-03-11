using UnityEngine;
using UnityEngine.Events;

public class MovingState : State
{
    private Transform transform;
      
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Location nextLocation;
    private Transform nextTransform;

    //public delegate void OnEteredHandle(Location nextLoc);
    //public OnEteredHandle onEntered;

    private AnimationCurve speedChange;
    private float speed;

    private float lerp = 0;
     

    public MovingState(StateMachine SM,Transform _transform, Location _nextLocation , AnimationCurve _speedChange,float _speed):base(SM)
    {
        speed = _speed;
        transform = _transform;
        nextLocation = _nextLocation; 
        speedChange = _speedChange; 
    }
     

    public override void Enter()
    { 
        currentPos = transform.position; 
        currentRot = transform.rotation;
        nextTransform = nextLocation.GetView();

        //Debug.Log("Enter moving \n " + nextLocation.transform.position);
    }

    public override void Exit()
    {
        //Debug.Log("Exit moving"); 
        transform.position = nextTransform.position;
        transform.rotation = nextTransform.rotation;
        lerp = 0;
    }

    public override void Update()
    {
        //Debug.Log("Update moving");
         
        lerp += speed * Time.fixedDeltaTime ;

        transform.position = Vector3.Lerp(currentPos, nextTransform.position, speedChange.Evaluate(lerp));
        transform.rotation = Quaternion.Lerp(currentRot, nextTransform.rotation, speedChange.Evaluate(lerp)); 

        if (lerp >= 1)
        {
            //Debug.Log("End transform");
            lerp = 0;
            nextLocation?.onEntered(nextLocation.GetNearLocations());
            SM.EnterIn<IdleState>(); 
        }
          
    }

    public void UpdateNextLocation(Location newLoc)
    {
        nextLocation = newLoc;
    }
}
