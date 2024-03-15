using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Location
{ 
    private int dialogNumber;
    public new Action onEntered;

    private void Start()
    {
       var a=DialogStorage.getCharachterDialog(name, dialogNumber);
        Debug.Log(a.replics[0].text);
        
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
        onEntered += FindObjectOfType<Player>().EnterIn<InactionState>;
        

    }


}
