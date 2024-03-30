using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] Player player;
    Location[] locations;

    [SerializeField] ArrowsManager arrowsManager;
    [SerializeField] Book book;


    private void Awake()
    { 
        DialogStorage.setDialogsForCurrentLevel("BeforeRevolution");
        DialogStorage.setMonologsForCurrentLevel("BeforeRevolution");

        player.

    }
}
