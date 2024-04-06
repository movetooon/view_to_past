using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLocationEvent : GameEvent
{

    [SerializeField] Location enterLocation;
    public override void Init()
    {
       
    }

    public override void Invoke()
    {
        enterLocation.Select();
    }
}
