using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTask : GameTask
{
    [SerializeField] SnailRace race;
    Action<string> onRemoveTaskRequested; 

    public override void Init(Book book)
    {
        onRemoveTaskRequested += book.RemoveTask;
    }

    public override bool CheckDone()
    {
        return race.checkWin();
    }

    public override void Complete()
    {
        onRemoveTaskRequested?.Invoke(taskName);
    }
}
