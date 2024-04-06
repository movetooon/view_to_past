using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] Player player;
    Location[] locations;
    TaskHandler[] taskHandler;
    EventHandler[] eventHandler;
    NPC[] npcs;
    MonologHandler[] monologHandlers;

    [SerializeField] ArrowsManager arrowsManager;
    [SerializeField] Book book;
    [SerializeField] InventoryWindow inventoryPanel;
    [SerializeField] Inventory  inventory ;
    [SerializeField] DialogDisplayer dialog; 
    [SerializeField] MonologDisplayer monolog; 
    [SerializeField] DialogCloud dialogCloud;
    [SerializeField] public GameObject startScene; 



    private void Start()
    {
        book.gameObject.SetActive(true);

        taskHandler = FindObjectsByType<TaskHandler>(FindObjectsSortMode.None);
        eventHandler = FindObjectsByType<EventHandler>(FindObjectsSortMode.None);
        locations = FindObjectsByType<Location>(FindObjectsSortMode.None);
        npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);
        monologHandlers = FindObjectsByType<MonologHandler>(FindObjectsSortMode.None);
        DialogStorage.SetDialogsForCurrentLevel("BeforeRevolution");
        DialogStorage.SetMonologsForCurrentLevel("BeforeRevolution");

        player.Init();
        InitBook();

        InitLocations();
        InitNPCs();
        InitMonologhandlers();

        //InitEventHandlers();
        InitTaskHandlers();
        
        player.EnterStartLocation();

        book.gameObject.SetActive(false);
        startScene.SetActive(true);
    }

    private void InitTaskHandlers()
    {
        foreach (var handler in taskHandler)
        {
            handler.Init();
        }
    }

    /*
    private void InitEventHandlers()
    {
        foreach (var handler in eventHandler)
        {
            handler.Init();
        }
    }
    */

    private void InitBook()
    { 
        book.Init(player, arrowsManager);

    }
    private void InitLocations()
    {
        foreach (Location loc in locations)
        {
            loc.onSelected += player.GetState<IdleState>().MoveToNextLocation;
            loc.onDisableClickingRequested += arrowsManager.DisableClickingAllArrows;
            loc.onLocationsUpdateRequested += arrowsManager.UpdateArrows;

            loc.onEntered += player.EnterIn<IdleState>;
            loc.onEntered += arrowsManager.EnableClickingAllArrows;
        }
    }

    private void InitNPCs()
    {
        foreach (NPC npc in npcs)
        {
            npc.Init(player, arrowsManager, dialog, dialogCloud);
        }
    }

    private void InitMonologhandlers()
    {
        foreach (MonologHandler mh in monologHandlers)
        {

            mh.Init(player, monolog, arrowsManager);
             
        }
    }
}
