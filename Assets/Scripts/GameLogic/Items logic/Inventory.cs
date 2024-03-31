using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{ 
    [SerializeField]private List<ItemData> items = new List<ItemData>();
    //UnityEvent onUpdated;
    public Action<List<ItemData>> onUpdated;


    public List<ItemData> GetItems() => items;

    public void AddItem(ItemData newItem)
    { 
        items.Add(newItem);
        onUpdated?.Invoke(items);
    }

    public void RemoveItem(ItemData itemToRemove)
    {
        items.Remove(itemToRemove);
        onUpdated?.Invoke(items);
    }

     
}
