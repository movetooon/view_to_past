using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public string levelName = "BeforeRevolution";

    [SerializeField] Player player;
    Location[] locations;
    TaskHandler[] taskHandler;
    EventHandler[] eventHandler;
    NPC[] npcs;
    MonologHandler[] monologHandlers;
    Item[] items;

    [SerializeField] ArrowsManager arrowsManager;
    [SerializeField] Book book;
    [SerializeField] InventoryWindow inventoryPanel;
    
    [SerializeField] Inventory  inventory ;
   
    [SerializeField] DialogDisplayer dialog; 
    [SerializeField] MonologDisplayer monolog; 
    [SerializeField] DialogCloud dialogCloud;
    [SerializeField] ItemsMonologsDisplayer itemsMonolog;
    
    [SerializeField] public GameObject startScene; 
    [SerializeField] ItemSoundPlayer itemSoundPlayer;
    [SerializeField] WeatherSystem weatherSystem;


    private void Start()
    {
        Debug.Log("Hello from entry point");

        arrowsManager.Init();
        dialogCloud.Init();
        book.gameObject.SetActive(true);
        DialogStorage.SetDialogsForCurrentLevel(levelName);
        DialogStorage.SetMonologsForCurrentLevel(levelName);
        dialog.Init(arrowsManager, book, player);

        taskHandler = FindObjectsByType<TaskHandler>(FindObjectsSortMode.None);
        eventHandler = FindObjectsByType<EventHandler>(FindObjectsSortMode.None);
        
        locations = FindObjectsByType<Location>(FindObjectsSortMode.None);
        npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);
        monologHandlers = FindObjectsByType<MonologHandler>(FindObjectsSortMode.None);
        items = FindObjectsByType<Item>(FindObjectsSortMode.None);


        itemSoundPlayer.audioPlayer=itemSoundPlayer.GetComponent<AudioSource>();
        InitItems();

        player.Init();
        InitBook();

        InitLocations();
        InitNPCs();
        InitMonologhandlers();

        //InitEventHandlers();
        InitTaskHandlers();
        weatherSystem.Init();
        
        player.EnterStartLocation();

        book.gameObject.SetActive(false);
        startScene.SetActive(true);
    }

    private void InitTaskHandlers()
    {
        foreach (var handler in taskHandler)
        {
            handler.Init(book);
        }
    }

    private void InitItems()
    {
        foreach (var item in items)
        {
            item.Init(inventory,itemsMonolog,itemSoundPlayer);
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
            loc.Init(player, arrowsManager,book);
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

            mh.Init(player, monolog, arrowsManager,book);
             
        }
    }
}
