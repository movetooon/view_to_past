using System; 
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Location : Selectable
{
    [SerializeField] private Transform view;
    [SerializeField] protected List<NearLocation> nearLocations;
    [SerializeField] public List<Transform> mustIntersectDuringMoving=new List<Transform>();
    
  
    public Action<List<NearLocation>> onLocationsUpdateRequested;
    public Action onDisableClickingRequested;
    public Action onEnded;
    public Action<Location> onSelected;
 

    private void OnValidate()
    { 
        if (view == null)
        {
            view = ((GameObject)Instantiate(Resources.Load("view"),transform.position,transform.rotation)).transform;
            
            view.parent = transform;
        }
        if (mustIntersectDuringMoving.Count==0)
        {
            mustIntersectDuringMoving=new List<Transform>() { view };
        }
    }
    /*

    private void Init()
    {
        SetListeners(); 
    }

    public virtual void SetListeners()
    {
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
        onDisableClickingRequested += FindObjectOfType<ArrowsManager>().DisableClickingAllArrows;
        onLocationsUpdateRequested += FindObjectOfType<ArrowsManager>().UpdateArrows;

        onEnded += FindObjectOfType<Player>().EnterIn<IdleState>;
        onEnded += FindObjectOfType<ArrowsManager>().EnableClickingAllArrows;
    }
    */


    public Transform GetView() => view; 
    public List<NearLocation> GetNearLocations() => nearLocations;

    public virtual void Enter()
    {
        onLocationsUpdateRequested?.Invoke(nearLocations); 
        onEnded?.Invoke();
        
    }

    public override void Select(float distance = 0)
    {
        if (selected == false&&distance<maxViewDistance)
        {
            onDisableClickingRequested?.Invoke();
            onSelected?.Invoke(this);
            selected = true;
        }
    }

    public override void Unselect() => base.Unselect();  
    public override void EnableOutline() => base.EnableOutline(); 
    public void OnMouseExit() => base.DisableOutline();


}

[System.Serializable]
public class NearLocation
{
    [SerializeField] Location location;
    [SerializeField] Direction direction;

    public Location GetLocation() => location; 
    public Direction GetDirection() => direction; 

    public enum Direction
    {
        up,
        right,
        down,
        left
    }
}
