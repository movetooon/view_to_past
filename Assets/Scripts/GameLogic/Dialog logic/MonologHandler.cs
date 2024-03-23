using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologHandler : Location
{
    [SerializeField] string monologName;
    private Action<Monolog> onMonologDisplayRequested;

    private void Start()
    {
        SetListeners();   
    }

    public override void SetListeners()
    {
        onMonologDisplayRequested += FindObjectOfType<MonologDisplayer>().StartShowingMonolog;
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
        onDisableClickingRequested += FindObjectOfType<ArrowsManager>().DisableClickingAllArrows;
        onLocationsUpdateRequested += FindObjectOfType<ArrowsManager>().UpdateArrows;

        onEnded += FindObjectOfType<Player>().EnterIn<InactionState>;
        onEnded += FindObjectOfType<ArrowsManager>().DisableAllArrows;
    }

    public override void Enter()
    {
        Monolog newMonolog = DialogStorage.getMonologByName(monologName);
        onMonologDisplayRequested?.Invoke(newMonolog);

        Debug.Log(newMonolog.replics[0]);

        base.Enter();   
         
    }

     

}
