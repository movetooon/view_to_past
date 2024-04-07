using TMPro; 
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image icon;

     
    public void Init(ItemData data)
    {
        nameText.text = data.GetName();
        icon.sprite = data.GetIcon();
    }
}
