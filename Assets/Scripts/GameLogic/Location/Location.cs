using System; 
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Events; 

[System.Serializable] 

public class Location : Selectable
{
    [SerializeField] private Transform view;
    public bool blocked;
    public bool disableDiaryButton;
    [SerializeField] protected List<NearLocation> nearLocations;
    [SerializeField] public List<Transform> mustIntersectDuringMoving=new List<Transform>();

    

    [SerializeField] protected UnityEvent onEnteredEvent; 
  
    public Action<List<NearLocation>> onLocationsUpdateRequested;
    public Action onDisableClickingRequested;
    public Action onEntered;
    public Action<Location> onSelected;
 
     /*
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
    */
     

     public void Init(Player player,ArrowsManager arrowsManager,Book book)
     {
        onSelected += player.GetState<IdleState>().MoveToNextLocation;
        onDisableClickingRequested += arrowsManager.DisableClickingAllArrows;
        onDisableClickingRequested += book.DisableDiaryButton;
        onLocationsUpdateRequested += arrowsManager.UpdateArrows;

        onEntered += player.EnterIn<IdleState>;
        onEntered += book.EnableDiaryButton;
        onEntered += arrowsManager.EnableClickingAllArrows;  

        if(disableDiaryButton)
        onEntered += book.DisableDiaryButton;
        else onEntered += book.EnableDiaryButton;
    }
    /*

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


    public override void Select(float distance = 0)
    {
        if (blocked == true) return;

        if (selected == false && distance < maxViewDistance)
        {
            EnableOutline();
            onDisableClickingRequested?.Invoke();
            onSelected?.Invoke(this);
            selected = true;
        }
    }
    public virtual void Enter()
    {
        onLocationsUpdateRequested?.Invoke(nearLocations); 
        onEntered?.Invoke();
        onEnteredEvent?.Invoke();
    }

    
    public void SetBlocked(bool block)
    {
        blocked = block;
    }

    public override void Unselect() => base.Unselect();
    public override void EnableOutline()
    {
        if (blocked == true) return;
        base.EnableOutline();
    }
    public virtual void OnMouseExit() => base.DisableOutline();


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
