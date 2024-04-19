using System;
using System.Collections.Generic;
using UnityEngine;

public class MonologHandler : Location
{
    [SerializeField] string monologName; 
    [SerializeField] public bool playOnce;
     
    [HideInInspector] public EventHandler eventHandler;
    

    private bool played;
    public Action<Monolog,EventHandler> onMonologDisplayRequested;
    public Action<List<NearLocation>> onArrowsCacheUpdateRequest;
    public Action<List<NearLocation>> onArrowsUpdateRequest;
 
    public Action onReturnToNormal;
    

    public void Init(Player player, MonologDisplayer monolog, ArrowsManager arrowsManager, Book book)
    { 
        onMonologDisplayRequested += monolog.StartShowingMonolog;
        onSelected += player.GetState<IdleState>().MoveToNextLocation;
        //onDisableClickingRequested += arrowsManager.DisableClickingAllArrows;
        onReturnToNormal += player.EnterIn<IdleState>;
        onReturnToNormal += arrowsManager.EnableClickingAllArrows;
        onReturnToNormal += book.EnableDiaryButton;

        onEntered += player.EnterIn<InactionState>;
        onEntered += arrowsManager.DisableAllArrows;
        onArrowsCacheUpdateRequest += arrowsManager.UpdateArrowsCache;
        onArrowsUpdateRequest += arrowsManager.UpdateArrows;
         
          
        TryGetComponent<EventHandler>(out eventHandler);
    }

    public override void Enter()
    {
        selected = true;
        Debug.Log("Enter in " + name);

        if (playOnce && played)
        {
            onArrowsUpdateRequest?.Invoke(nearLocations);
            onReturnToNormal?.Invoke();
            
            return;
        }
        else
        {
            onArrowsCacheUpdateRequest?.Invoke(nearLocations);
            Monolog newMonolog = DialogStorage.GetMonologByName(monologName);
            onMonologDisplayRequested?.Invoke(newMonolog, eventHandler);
            played = true;

            onEntered?.Invoke();
        }

       


    }

     

}
