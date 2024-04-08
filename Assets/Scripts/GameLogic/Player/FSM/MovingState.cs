using System.Collections.Generic; 
using UnityEngine; 

public class MovingState : State<Player>
{  
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Location nextLocation;
    private Transform nextTransform; 
    private List<Transform> way; 

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

        //Debug.Log("Enter moving ");
    }

    public override void Exit()
    {
        //Debug.Log("Exit moving"); 
        SM.transform.position = nextTransform.position;
        SM.transform.rotation = nextTransform.rotation;
        lerp = 0;
    }

    public override  void Update()
    { 
        if(nextTransform.transform.position==currentPos)nextLocation.Enter();

        int count; 
        lerp += speed * Time.fixedDeltaTime;
         
        count = (int)(speedChange.Evaluate(lerp) * (way.Count)); 

        if (count != 0)
        {
            currentPos = way[count - 1].position;
            currentRot = way[count - 1].rotation;
        }
        if (count == way.Count)
        {
            lerp = 0;
            nextLocation.Enter();
        }
        else
        { 
            float lerpEvaluate = (speedChange.Evaluate(lerp) * way.Count) % 1;

            SM.transform.position = Vector3.Lerp(currentPos, way[count].position, lerpEvaluate);
            SM.transform.rotation = Quaternion.Lerp(currentRot, way[count].rotation, lerpEvaluate);
        }

         

    }

    public void UpdateNextLocation(Location newLoc)
    {
        
        nextLocation = newLoc;
        way = nextLocation.mustIntersectDuringMoving;
    }
}
