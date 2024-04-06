using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTask : GameTask
{
    [SerializeField] snailRace race;
    
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
