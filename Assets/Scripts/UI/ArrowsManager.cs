using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsManager : MonoBehaviour
{
    [SerializeField] private List<Arrow> arrows = new List<Arrow>();
    private bool[] cache=new bool[4];


    public void DisableAllArrows()
    {
        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
        }
    }

    public void EnableAllArrows()
    {
        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(true);
        }
    }

    public void UpdateArrows(List<NearLocation> nearLocs)
    { 
        foreach(Arrow arrow in arrows)
        {
            arrow.gameObject.SetActive(false);

            int count = 0;
            cache[count] = false;

            foreach (NearLocation nearLoc in nearLocs)
            { 
                if (arrow.GetDirection() == nearLoc.GetDirection())
                {
                    cache[count] = true;
                    arrow.SetNextLocation(nearLoc.GetLocation());
                    arrow.gameObject.SetActive(true);
                    
                    break;
                } 
            }

            count++;
        }  
    }

    public void ReUpdateArrows()
    {

        for(int i = 0; i < arrows.Count; i++)
        {
            arrows[i].gameObject.SetActive(false);
            if (cache[i])
            {
                arrows[i].gameObject.SetActive(true);
            }
        }
         
    }
}
