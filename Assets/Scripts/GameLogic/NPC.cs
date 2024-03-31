using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Location
{ 
    private int nextDialogNumber;
    [SerializeField] private float height; 
    [SerializeField] public Vector3 cloudPosition;

    public TaskHandler taskHandler;
    public EventHandler eventHandler;
    [SerializeField]bool waitingForTaskDone;
 
    public  Action onEntered; 
    public Action<List<NearLocation>> onArrowUpdateRequest;
    public Action<Dialog,EventHandler> onDialogDisplayRequested;
    public Action<Transform, Vector3, float> onCloudUpdateRequested;
     

    public void Init(Player player,ArrowsManager arrowsManager, DialogDisplayer dialog, DialogCloud dialogCloud)
    {
        onSelected += player.GetState<IdleState>().MoveToNextLocation;

        onEntered += player.EnterIn<InactionState>;
        onEntered += arrowsManager.DisableAllArrows;
        onArrowUpdateRequest += arrowsManager.UpdateArrowsCache;

        onDialogDisplayRequested += dialog.StartDialog;
        onCloudUpdateRequested += dialogCloud.SetPositions;

        TryGetComponent<TaskHandler>(out taskHandler);
        TryGetComponent<EventHandler>(out eventHandler);
         
    }

      

    public override void Enter()
    { 
        onEntered?.Invoke();
        onArrowUpdateRequest?.Invoke(nearLocations);

        Dialog newDialog = DialogStorage.getCharachterDialog(name, nextDialogNumber); 
         

        if (newDialog.countOfTasks > 0)
        { 
            taskHandler?.SetCurrentTasksCount(newDialog.countOfTasks);

            Debug.Log("there are " + newDialog.countOfTasks + " at " + name + " charachter");
            StartDisplayingDialog(newDialog,false);
            nextDialogNumber += 2;


            waitingForTaskDone = true;
        }
        else if (waitingForTaskDone == true &&taskHandler?.checkTasksDone()==false)
        {
            Debug.Log("tasks are still not done!");

            Dialog oldDialog = DialogStorage.getCharachterDialog(name, nextDialogNumber-1);
            StartDisplayingDialog(oldDialog,false);
        } 
        else
        {
            StartDisplayingDialog(newDialog, true);
        }

    }

    

    public void StartDisplayingDialog(Dialog dialog,bool setNext)
    {
        onDialogDisplayRequested?.Invoke(dialog, eventHandler);
        onCloudUpdateRequested?.Invoke(transform, transform.rotation * cloudPosition + transform.position, height);

        if (setNext) nextDialogNumber++;
    }

    public override void Select()
    { 
        base.Select();
    }

    public float Height() => height;


}
