using UnityEngine;


public class Player : StateMachine<Player>,IStateMachine
{
 
    [SerializeField] public AnimationCurve speedChange; 
    [SerializeField] private float speed;
    [SerializeField] private Location currentLocation;
    [SerializeField] private float lookingAroundStrength;

    public void Init()
    { 
        InitializeStateMachine();  
    }
    public void EnterStartLocation()
    {
        //currentLocation.Select();
    }

    private void FixedUpdate()
    {
         UpdateCurrent(); 
        
    }

    private void InitializeStateMachine()
    {
        AddState(new InactionState(this));
        AddState(new IdleState(this, currentLocation, lookingAroundStrength));
        AddState(new MovingState(this, currentLocation, speedChange, speed));
         
         
        base.EnterIn<InactionState>();
    }


    public override void EnterIn<T>() 
    { 
        base.EnterIn<T>();
    }

    public void EnterInInactionState()
    {
        base.EnterIn<InactionState>();
    }



    public void AddState(State<Player> T)
    {
        states.Add(T.GetType(), T); 
    }

    public void EnterMovingState(Location newLoc) 
    {
        //currentState.GetType() != typeof(InactionState)
        if ( currentState.GetType() != typeof(MovingState))
        {
            GetState<MovingState>().UpdateNextLocation(newLoc);
            currentLocation.Unselect(); 
            currentLocation = newLoc;
            base.EnterIn<MovingState>();
        }
    }
    public void SelectNextLocation(Location newLoc)
    {
        //currentState.GetType() != typeof(InactionState)
        if (currentState.GetType() != typeof(MovingState))
        {
            GetState<MovingState>().UpdateNextLocation(newLoc);
            currentLocation.Unselect();
            currentLocation = newLoc;
            newLoc.Select();
        }
    }


}
