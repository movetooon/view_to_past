using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Selectable : MonoBehaviour
{
    public abstract void Select();
    public virtual void Unselect() { }

    public virtual void OnMouseEnter()
    { 
        if(!EventSystem.current.IsPointerOverGameObject())
        gameObject.layer = LayerMask.NameToLayer("Outline");
    }
    public virtual void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
