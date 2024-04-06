using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemEvent : GameEvent
{
    [SerializeField] private List<ItemData> itemsToAdd;
    Inventory inventory;

    public override void Init()
    { 
        inventory = FindObjectOfType<Inventory>();
    }

    public override void Invoke()
    {
        foreach (ItemData item in itemsToAdd)
        {
            inventory.AddItem(item);
        }
    }
}
