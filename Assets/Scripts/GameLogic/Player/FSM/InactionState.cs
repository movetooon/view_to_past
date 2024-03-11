using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactionState : State
{ 

    public InactionState (StateMachine SM):base (SM)
    {
         
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Enter()
    {
        Debug.Log("enter inaction");
        base.Update();
    }

    public override void Exit()
    {
        Debug.Log("enter inaction"); 
        base.Update();
    }
}
