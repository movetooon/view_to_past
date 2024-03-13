using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsManager : MonoBehaviour
{
    [SerializeField] private List<Arrow> arrows = new List<Arrow>();
    public void UpdateArrows(List<NearLocation> nearLocs)
    {
              
        foreach(Arrow arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
            foreach (NearLocation nearLoc in nearLocs)
            { 
                if (arrow.GetDirection() == nearLoc.GetDirection())
                {
                    //Debug.Log(arrow.name + " | " + nearLoc.GetLocation().transform.position.ToString());
                    arrow.SetNextLocation(nearLoc.GetLocation());
                    arrow.gameObject.SetActive(true);
                    
                    break;
                }
                 
            }
             
        }

        
    }
}
