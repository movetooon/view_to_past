using System;
using System.Collections;
using UnityEngine; 
using UnityEngine.UI;
using System.Threading.Tasks;

public class Arrow : MonoBehaviour
{
    [SerializeField] private NearLocation.Direction direction;
    private Location nextLoc;
    private Animator anim;
    private Image selfImage;

    public void Init()
    {
        selfImage = GetComponent<Image>();
        anim=GetComponent<Animator>(); 
         
    }

    private void OnValidate()
    {
        selfImage = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    public void Disable()
    { 
        gameObject.SetActive(false);
    }

    public void Enable()
    {  
        gameObject.SetActive(true);
        
    }

    public void DisableAnim()
    {
        DisableClicking(); 
        if(gameObject.activeSelf==true)
        anim.SetTrigger("disapear"); 
    }
    public void DisableClicking()
    {
        selfImage.raycastTarget = false;
         
    }

    public void EnableClicking()
    { 
        selfImage.raycastTarget = true;

    }



    public void MoveToNextLocation() => nextLoc.Select();


    public void SetNextLocation(Location next)
    {
        nextLoc = next;
    }

    public NearLocation.Direction GetDirection()=> direction;

}
