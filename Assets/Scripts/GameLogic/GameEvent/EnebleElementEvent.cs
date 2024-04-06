using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnebleElementEvent : GameEvent
{
    [SerializeField] UnityEvent action;

    public override void Init()
    {
         
    }

    public override void Invoke()
    {
        action.Invoke();
    }
}
