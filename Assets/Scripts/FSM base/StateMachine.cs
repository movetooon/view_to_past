using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachine 
{
    public State currentState;
    protected Dictionary<Type,State> states;

    public StateMachine()
    {
        states = new Dictionary<System.Type, State>();
    }

    public void EnterIn<T>() where T:State 
    { 
        if(states.TryGetValue(typeof(T), out State state))
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }
         
    }

    public void EnterIn(State state)
    { 
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
         
    }

    public void Update()
    {
        currentState?.Update();
    }

    public T GetState<T>() where T: State
    {
        return (T)states[typeof(T)];
    }

    public void AddState(State T) 
    {
        states.Add(T.GetType(), T);
    }
}
