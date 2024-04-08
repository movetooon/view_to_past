using System;
using System.Collections.Generic;
using UnityEngine;

public class MonologHandler : Location
{
    [SerializeField] string monologName;
    [SerializeField] public bool playOnEnable;
    [SerializeField] public bool playOnce;
     
    public EventHandler eventHandler;
    

    private bool played;
    public Action<Monolog,EventHandler> onMonologDisplayRequested;
    public Action<List<NearLocation>> onArrowUpdateRequest;
    

    public void Init(Player player, MonologDisplayer monolog, ArrowsManager arrowsManager)
    { 
        onMonologDisplayRequested += monolog.StartShowingMonolog;
        onSelected += player.GetState<IdleState>().MoveToNextLocation;
        //onDisableClickingRequested += arrowsManager.DisableClickingAllArrows;

        onEntered += player.EnterIn<InactionState>;
        onEntered += arrowsManager.DisableAllArrows;
        onArrowUpdateRequest += arrowsManager.UpdateArrowsCache;
         
         

        TryGetComponent<EventHandler>(out eventHandler);
    }

    public override void Enter()
    {
         selected = true;
          

        onArrowUpdateRequest?.Invoke(nearLocations);
        Monolog newMonolog = DialogStorage.GetMonologByName(monologName);
        onMonologDisplayRequested?.Invoke(newMonolog,eventHandler); 
        played = true;
       
        if(playOnce==true)blocked = true;
          
        onEntered?.Invoke();


    }

     

}
