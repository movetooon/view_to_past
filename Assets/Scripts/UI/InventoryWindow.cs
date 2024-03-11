using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] ItemInventory itemPrefab;
    [SerializeField] private RectTransform InventoryPanel; 
    [SerializeField] private List<ItemInventory> items;

    [SerializeField] private Inventory inventory;

    private void Start()
    {
       // inventory.onUpdated +=UpdateItemsList; 
    }

    public void UpdateItemsList()
    {
        var itemsData = inventory.GetItems();

        for (int i = 0; i < itemsData.Count; i++)
        {
            Debug.Log("BITCH YOU NOW IM SEXY "+i);
            if (i < items.Count)
            {
                Debug.Log("HI BABE");
                items[i].Init(itemsData[i]);
            }
            else
            {
                Debug.Log("NEW HI BABE");

                var newItem = Instantiate(itemPrefab,InventoryPanel);
                newItem.Init(itemsData[i]);
                items.Add(newItem);
            }
        }
    }
}
