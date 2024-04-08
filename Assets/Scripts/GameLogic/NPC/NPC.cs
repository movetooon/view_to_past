using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC : Location,ITalkable
{ 
    private int nextDialogNumber;
    [SerializeField] private float height; 
    [SerializeField] public Vector3 cloudPosition;
    private SpriteRenderer sprite;

    private TaskHandler taskHandler;
    private EventHandler eventHandler;
    [SerializeField]bool waitingForTaskDone; 
    [SerializeField] private bool disableOnStart;

    [SerializeField] NPCSound sound;
  
    public Action<List<NearLocation>> onArrowUpdateRequest;
    public Action<Dialog,EventHandler, ITalkable,NPCSound> onDialogDisplayRequested;
    public Action<Transform, Vector3, float> onCloudUpdateRequested;
    public Action onDialogEnded;

    public void Init(Player player,ArrowsManager arrowsManager, DialogDisplayer dialog, DialogCloud dialogCloud)
    {
        onSelected += player.GetState<IdleState>().MoveToNextLocation;

        onEntered += player.EnterIn<InactionState>;
        onEntered += arrowsManager.DisableAllArrows; 
        onArrowUpdateRequest += arrowsManager.UpdateArrowsCache;

        onDialogDisplayRequested += dialog.StartDialog;
        onCloudUpdateRequested += dialogCloud.SetPositions;

        sprite = GetComponent<SpriteRenderer>(); 
        TryGetComponent<TaskHandler>(out taskHandler);
        TryGetComponent<EventHandler>(out eventHandler);
        disableDiaryButton = true;

        if (disableOnStart) gameObject.SetActive(false);
         
    }

      

    

    public void StartTalking()
    {
        Dialog newDialog;
        DialogStorage.TryGetCharachterDialog(name, nextDialogNumber, out newDialog);


        if (newDialog.countOfTasks > 0)
        {
            taskHandler?.SetCurrentTasksCount(newDialog.countOfTasks);

            Debug.Log("there are " + newDialog.countOfTasks + " at " + name + " charachter");
            StartDisplayingDialog(newDialog, false);
            nextDialogNumber += 2;


            waitingForTaskDone = true;
        }
        else if (waitingForTaskDone == true && taskHandler?.checkTasksDone() == false)
        {
            Debug.Log("tasks are still not done!");

            Dialog oldDialog = DialogStorage.GetCharachterDialog(name, nextDialogNumber - 1);
            StartDisplayingDialog(oldDialog, false);
        }
        else StartDisplayingDialog(newDialog, true);

    }
    public void EndTalking()=> selected = false;
    

    public void StartDisplayingDialog(Dialog dialog,bool setNext)
    {
        onDialogDisplayRequested?.Invoke(dialog, eventHandler,this,sound);
        onCloudUpdateRequested?.Invoke(transform, transform.rotation * cloudPosition + transform.position, height);

        if (setNext) nextDialogNumber++;
    }

    public override void Select(float distance = 0)
    {  
        if (IsDialogsEnded()||blocked==true) return; 
            base.Select();
    } 

   

    public override void Enter()
    {
        onEntered?.Invoke();
        onArrowUpdateRequest?.Invoke(nearLocations);

        StartTalking();

    }

    public override void EnableOutline()
    {
        if (IsDialogsEnded()||blocked==true) return;


        if (!EventSystem.current.IsPointerOverGameObject() && !selected)
            sprite.material.SetFloat("_OutlineStrength", 0.04f);
       // base.EnableOutline();
    }
    public override void DisableOutline()
    {  
            sprite.material.SetFloat("_OutlineStrength", 0.00f); 

    }

    public override void OnMouseExit()
    {
        DisableOutline();
    }

    public bool IsDialogsEnded()
    {
        Dialog newDialog;
        bool isDialogEnded = !DialogStorage.TryGetCharachterDialog(name, nextDialogNumber, out newDialog);

        return isDialogEnded;
    }

    public float Height() => height;


}
