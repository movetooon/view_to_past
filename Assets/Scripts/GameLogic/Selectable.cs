 using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Selectable : MonoBehaviour
{
    protected bool selected;
    [SerializeField] protected GameEvent onSelectedEvent;
    [SerializeField] protected float maxViewDistance=1000;
    public abstract void Select(float distance=0);
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
