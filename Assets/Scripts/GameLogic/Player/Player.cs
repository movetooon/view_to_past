using UnityEngine;


public class Player : StateMachine<Player>,IStateMachine
{
 
    [SerializeField] public AnimationCurve speedChange; 
    [SerializeField] private float speed;
    [SerializeField] private Location currentLocation;
    [SerializeField] private float lookingAroundStrength;

    private void Awake()
    { 
        InitializeStateMachine(); 
         
    }

    private void Update()
    {
         UpdateCurrent(); 
        
    }

    private void InitializeStateMachine()
    { 
        AddState(new MovingState(this, currentLocation, speedChange, speed));
        AddState(new IdleState(this,currentLocation, lookingAroundStrength));
        AddState(new InactionState(this));

        base.EnterIn<IdleState>();
    }


    public override void EnterIn<T>() 
    { 
        base.EnterIn<T>();
    }


    public void AddState(State<Player> T)
    {
        states.Add(T.GetType(), T); 
    }

    public void EnterMovingState(Location newLoc) 
    {
        if (currentState.GetType() != typeof(InactionState)&& currentState.GetType() != typeof(MovingState))
        {
            GetState<MovingState>().UpdateNextLocation(newLoc);
            currentLocation.Unselect(); 
            currentLocation = newLoc;
            base.EnterIn<MovingState>();
        }
    }

 

}
