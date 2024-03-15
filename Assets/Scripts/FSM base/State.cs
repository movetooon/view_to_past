using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class State<T> where T : IStateMachine
{
    protected T SM; 

    public State(T _SM)
    {
        SM = _SM;
    }
     
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

}
