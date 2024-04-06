using System; 
using UnityEngine; 

public class Item : Selectable
{
    [SerializeField] private ItemData data; 
    public Action <ItemData>onTaken;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.Texture();
        onTaken+=(FindObjectOfType<Inventory>().AddItem);
        onTaken+= FindObjectOfType<ItemsMonologsDisplayer>().ShowItemInfo;
    }

    public override void Select(float distance = 0)
    {
        onTaken?.Invoke(data);
        onSelectedEvent?.Invoke();
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (data != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.Texture();
            GetComponent<BoxCollider>().size = new Vector3(data.Texture().bounds.size.x, data.Texture().bounds.size.y,0.2f);
        }

    }

    public override void InvokeEvent()
    {
        base.InvokeEvent();
    }

    public override void EnableOutline() => base.EnableOutline(); 

    public void OnMouseExit()=>base.DisableOutline();
     

}
