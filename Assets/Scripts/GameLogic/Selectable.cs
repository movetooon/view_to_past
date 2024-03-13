using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Selectable : MonoBehaviour
{
    protected bool selected;

    public abstract void Select();
    public virtual void Unselect() => selected = false;

    public virtual void EnableOutline()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !selected) 
            gameObject.layer = LayerMask.NameToLayer("Outline");
    }
    public virtual void DisableOutline()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        { 
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
