using System; 
using UnityEngine; 

public class Item : Selectable
{
    [SerializeField] private ItemData data; 
    public Action <ItemData>onTaken;
    public Action <ItemData>onInfoShowRequested;
  //  public Action <ItemData>onItemAudioPlayerRequested;

    

    public void Init(Inventory inventory,ItemsMonologsDisplayer itemsMonologs,ItemSoundPlayer soundPlayer)
    {
        GetComponent<SpriteRenderer>().sprite = data.GetTexture();
        onTaken += inventory.AddItem;
        onInfoShowRequested += itemsMonologs.ShowItemInfo;
        onTaken += soundPlayer.PlaySound;
    }

    public override void Select(float distance = 0)
    {
        onTaken?.Invoke(data);
        onInfoShowRequested?.Invoke(data);
        onSelectedEvent?.Invoke();
        Destroy(gameObject);
    }

    public void SelectWithoutInfo(float distance = 0)
    {
        onTaken?.Invoke(data);
        onSelectedEvent?.Invoke();
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (data != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.GetTexture();
            GetComponent<BoxCollider>().size = new Vector3(data.GetTexture().bounds.size.x, data.GetTexture().bounds.size.y,0.2f);
        }

    }

    public override void InvokeEvent()
    {
        base.InvokeEvent();
    }

    public override void EnableOutline() => base.EnableOutline(); 

    public void OnMouseExit()=>base.DisableOutline();
     

}
