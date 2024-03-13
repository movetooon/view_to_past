using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class Book : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private UnityEvent onClosed;
    [SerializeField] private UnityEvent onOpened;
    [SerializeField] private InventoryWindow inventoryPanel;

    private void Start()
    {
        anim=GetComponent<Animator>();
        onOpened.AddListener(FindObjectOfType<Player>().EnterIn<InactionState>);
        onOpened.AddListener(inventoryPanel.UpdateItemsList);

        onClosed.AddListener(FindObjectOfType<Player>().EnterIn<IdleState>);
    }

    public async void Close()
    {
        anim.SetTrigger("Close");
         
        await Task.Delay((int)(anim.GetCurrentAnimatorStateInfo(0).length*1000));
        onClosed?.Invoke();
        gameObject.SetActive(false); 
             
    }

    public void Open()
    {
        onOpened?.Invoke();
    }



}
