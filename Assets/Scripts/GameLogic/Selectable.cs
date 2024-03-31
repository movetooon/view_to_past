 using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Selectable : MonoBehaviour
{
    protected bool selected;
    [SerializeField] protected GameEvent onSelectedEvent;

    public abstract void Select();
    public virtual void Unselect() => selected = false;

    public virtual void EnableOutline()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !selected) 
            gameObject.layer = LayerMask.NameToLayer("Outline");
    }
    public virtual void DisableOutline()
    {
        
         gameObject.layer = LayerMask.NameToLayer("Default");
        
    }
}
