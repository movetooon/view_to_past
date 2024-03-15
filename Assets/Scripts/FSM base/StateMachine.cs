using System; 
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class StateMachine<T>:MonoBehaviour where T : IStateMachine 
{
    public State<T> currentState;
    protected Dictionary<Type,State<T>> states;

    public StateMachine()
    {
        states = new Dictionary<System.Type, State<T>>();
    } 

    public virtual void EnterIn<S>() where S : State<T>
    { 
        if(states.TryGetValue(typeof(S), out State<T> state))
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }
         
    } 

    public virtual void UpdateCurrent() => currentState?.Update();  

    public S GetState<S>()where S : State<T>
    {
        return (S)states[typeof(S)];
    }

    public virtual void AddState<S>(State<T> state)where S:State<T> => states.Add(state.GetType(), state);

}

public interface IStateMachine
{
    //void EnterIn<S>();
    void UpdateCurrent();
    //State<IStateMachine> GetState(State<IStateMachine> state);
    // T GetState<T>() where T : State<IStateMachine>;
    //void AddState<T>(State<IStateMachine> state) where T : State<IStateMachine>;

}