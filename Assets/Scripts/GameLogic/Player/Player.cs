using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    StateMachine stateMachine; 
    [SerializeField] public AnimationCurve speedChange; 
    [SerializeField] private float speed;
    [SerializeField] private Location currentLocation;

    private void Start()
    {
        InitializeStateMachine(); 
    }

    private void Update()
    {
        stateMachine.Update(); 
    }

    private void InitializeStateMachine()
    {
        stateMachine = new StateMachine();
        stateMachine.AddState(new MovingState(stateMachine, transform, currentLocation, speedChange, speed));
        stateMachine.AddState(new IdleState(stateMachine,currentLocation));
        stateMachine.AddState(new InactionState(stateMachine));

        stateMachine.EnterIn<IdleState>();
    } 
    public T GetState<T>() where T : State
    {
        return stateMachine.GetState<T>();
    }

    public void EnterState<T>() where T : State
    { 
        if (stateMachine.currentState.GetType() != typeof(MovingState))
            stateMachine.EnterIn<T>();
    }

    public void EnterMovingState(Location newLoc) 
    {
        if (stateMachine.currentState.GetType() != typeof(InactionState))
        {
            stateMachine.GetState<MovingState>().UpdateNextLocation(newLoc);
            EnterState<MovingState>();
        }
    }


    /*
    public void EnterInactionState()
    { 
        if(stateMachine.currentState.GetType()!=typeof(MovingState))
            stateMachine.EnterIn<InactionState>();
    }
    */

}
