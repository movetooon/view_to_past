using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Location
{ 
    private int currentDialogNumber;
    [SerializeField] private float height; 
    [SerializeField] public Vector3 cloudPosition; 
    public  Action onEntered;

    public Action<List<NearLocation>> onArrowUpdateRequest;
    public Action<Dialog> onDialogDisplayRequested;
    public Action<Transform, Vector3, float> onCloudUpdateRequested;

    private bool waitingForTaskDone = false;

    private void Start()
    { 
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
       
        onEntered += FindObjectOfType<Player>().EnterIn<InactionState>;
        onEntered += FindObjectOfType<ArrowsManager>().DisableAllArrows;
        onArrowUpdateRequest += FindObjectOfType<ArrowsManager>().UpdateArrowsCache;
         
        onDialogDisplayRequested += FindObjectOfType<DialogDisplayer>().StartDialog;
        onCloudUpdateRequested += FindObjectOfType<DialogCloud>().SetPositions;
    }
    

    public override void Enter()
    { 
        onEntered?.Invoke();
        onArrowUpdateRequest?.Invoke(nearLocations);

        Dialog newDialog = DialogStorage.getCharachterDialog(name, currentDialogNumber);
        
        onDialogDisplayRequested?.Invoke(newDialog);
         
        onCloudUpdateRequested?.Invoke(transform , transform.rotation*cloudPosition+transform.position, height);

        currentDialogNumber++;
        
    }

    public override void Select()
    { 
        base.Select();
    }

    public float Height() => height;


}
