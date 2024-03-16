using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Location
{ 
    private int dialogNumber;
    [SerializeField] private float height;
    public new Action onEntered;
    public new Action<List<NearLocation>> onEnded;
    public Action<string,int,Vector3,float> onDialogDisplayRequested;

    private void Start()
    { 
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
       
        onEntered += FindObjectOfType<Player>().EnterIn<InactionState>;
        onEntered += FindObjectOfType<ArrowsManager>().DisableAllArrows;
        onEnded += FindObjectOfType<ArrowsManager>().UpdateArrows;
        
        onDialogDisplayRequested += FindObjectOfType<DialogManager>().StartDialog;

    }

    public override void Enter()
    { 
         
        onDialogDisplayRequested?.Invoke(name,dialogNumber,transform.position,height);
        onEnded?.Invoke(nearLocations);
        onEntered?.Invoke();
        //TODO исправить эту тупую хуйню
    }


}
