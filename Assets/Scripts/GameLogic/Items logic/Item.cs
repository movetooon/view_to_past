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

    public override void Select()
    {
        onTaken?.Invoke(data);
        onSelectedEvent?.Invoke();
        Destroy(gameObject);
    } 
    
    public override void EnableOutline() => base.EnableOutline(); 

    public void OnMouseExit()=>base.DisableOutline();
     

}
