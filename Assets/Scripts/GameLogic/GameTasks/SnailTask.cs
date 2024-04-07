using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTask : GameTask
{
    [SerializeField] SnailRace race;
    
    public override void Init()
    {  

    }

    public override bool CheckDone()
    {
        return race.checkWin();
    }

    public override void Complete()
    {
        
    }
}
