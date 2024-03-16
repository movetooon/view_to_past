using System; 
using System.Collections.Generic; 
using UnityEngine; 

[System.Serializable]
public class Location : Selectable
{
    [SerializeField] private Transform view;
    [SerializeField] protected List<NearLocation> nearLocations;
  
    public Action<List<NearLocation>> onEntered;
    public Action onEnded;
    public Action<Location> onSelected;
     

    private void Start()
    {
        onSelected += FindObjectOfType<Player>().GetState<IdleState>().MoveToNextLocation;
        onEntered+=FindObjectOfType<ArrowsManager>().UpdateArrows;
        onEnded += FindObjectOfType<Player>().EnterIn<IdleState>;
        
    }

    public Transform GetView() => view; 
    public List<NearLocation> GetNearLocations() => nearLocations;

    public virtual void Enter()
    {
        onEntered?.Invoke(nearLocations);
        onEnded?.Invoke();
    }

    public override void Select()
    {
        if (selected == false)
        {
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
