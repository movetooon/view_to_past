using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : Selectable
{
    [SerializeField] private ItemData data;
    //UnityEvent onTaken;
    public Action <ItemData>onTaken;

    private void Start()
    {
        onTaken+=(FindObjectOfType<Inventory>().AddItem);
    }

    public override void Select()
    {
        onTaken?.Invoke(data);
        Destroy(gameObject);
    } 

    public override void EnableOutline() => base.EnableOutline(); 

    public void OnMouseExit()=>base.DisableOutline();
     

}
