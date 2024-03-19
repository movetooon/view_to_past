using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Location
{ 
    private int currentDialogNumber;
    [SerializeField] private float height; 
    public new Action onEntered;
    public Action<List<NearLocation>> onArrowUpdateRequest;
    public Action<string,int,Vector3,float> onDialogDisplayRequested;

    private bool waitingForTaskDone = false;

    private void Start()
    { 
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
       
        onEntered += FindObjectOfType<Player>().EnterIn<InactionState>;
        onEntered += FindObjectOfType<ArrowsManager>().DisableAllArrows;
        onArrowUpdateRequest += FindObjectOfType<ArrowsManager>().UpdateArrows;
        
        onDialogDisplayRequested += FindObjectOfType<DialogDisplayer>().StartDialog;

    }
    

    public override void Enter()
    {  
        onDialogDisplayRequested?.Invoke(name,currentDialogNumber,transform.position,height);
        onArrowUpdateRequest?.Invoke(nearLocations);
        onEntered?.Invoke();
        currentDialogNumber++;
    }

    public override void Select()
    { 
        base.Select();
    }


}
