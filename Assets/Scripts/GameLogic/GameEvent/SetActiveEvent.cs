using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveEvent : GameEvent
{
    [SerializeField] private GameObject objectToSetActive;
    [SerializeField] private bool active;

    public override void Init() { }
     

    public override void Invoke()
    { 
        objectToSetActive.SetActive(active);
    }


}
