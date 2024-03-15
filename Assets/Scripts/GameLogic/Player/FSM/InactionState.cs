using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactionState : State<Player>
{

    public InactionState(Player SM) : base(SM) { }
    

    public override void Update()
    {
        base.Update();
    }

    public override void Enter()
    {
        //Debug.Log("enter inaction");
        base.Update();
    }

    public override void Exit()
    {
        //Debug.Log("enter inaction"); 
        base.Update();
    }
}
