using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class State 
{
    protected StateMachine SM; 

    public State(StateMachine _SM)
    {
        SM = _SM;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

}
