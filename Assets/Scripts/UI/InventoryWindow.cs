using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] ItemInventory itemPrefab;
    [SerializeField] private RectTransform InventoryPanel; 
    [SerializeField] private List<ItemInventory> items;

    [SerializeField] private Inventory inventory;

    

    public void UpdateItemsList()
    {
        var itemsData = inventory.GetItems();
        int itemsDataCount = itemsData.Count; 

        for (int i = 0; i < itemsData.Count; i++)
        {   
            if (i < items.Count)
            { 
                items[i].Init(itemsData[i]);
            }
            else
            {  
                var newItem = Instantiate(itemPrefab,InventoryPanel);
                newItem.Init(itemsData[i]);
                items.Add(newItem);
            }
        }

        if (itemsDataCount < items.Count)
        {
            if (itemsDataCount == 0)
            {
                GameObject toDestroy = items[0].gameObject;
                items.Remove(items[0]);
                Destroy(toDestroy);
                return;
            }

            for (int i = (itemsDataCount-1); i < (items.Count-1); i++)
            {
                GameObject toDestroy = items[i].gameObject;
                items.Remove(items[i]);
                Destroy(toDestroy);
            }
        }
    }
}
