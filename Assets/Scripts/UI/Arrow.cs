using System; 
using UnityEngine; 

public class Arrow : MonoBehaviour
{
    [SerializeField] private NearLocation.Direction direction;
    private Location nextLoc;   
     

    public void MoveToNextLocation() => nextLoc.Select();


    public void SetNextLocation(Location next)
    {
        nextLoc = next;
    }

    public NearLocation.Direction GetDirection()=> direction;

}
