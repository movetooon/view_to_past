using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GiveItBringItTask : GameTask
{
    [SerializeField] private List<ItemData> itemsToBring;  
    Inventory checkingInventory;
    Action<string> onRemoveTaskRequested; 

    //kostil'
    public override void Init(Book book)
    {
        checkingInventory = GameObject.FindObjectOfType<Inventory>();
        onRemoveTaskRequested += book.RemoveTask;
    }

    public override bool CheckDone()
    {
        int intersections = 0;
        foreach (ItemData inventoryItem in checkingInventory.GetItems()) 
        {
            foreach (ItemData bringItem in itemsToBring)
            {
                if (bringItem == inventoryItem)
                {
                    intersections++; 
                    break;
                }
            }

            if (intersections == itemsToBring.Count) return true;

        }
        return false;
         
    }

    public override void Complete()
    {
       foreach(var item in itemsToBring)
       {
           checkingInventory.RemoveItem(item);
       }
       onRemoveTaskRequested?.Invoke(taskName);
    }
}
