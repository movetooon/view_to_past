using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsManager : MonoBehaviour
{
    [SerializeField] private List<Arrow> arrows = new List<Arrow>();
    private bool[] enebledCache=new bool[4];


    public void Init()
    {
        foreach(Arrow arrow in arrows)
        {
            arrow.gameObject.SetActive(true);
            arrow.Init();
            arrow.gameObject.SetActive(false);

        }
    }

    public void DisableAllArrows()
    {
        foreach (var arrow in arrows)
        {
            arrow.DisableAnim();
        }
    }

    public void DisableClickingAllArrows()
    {
        foreach (var arrow in arrows)
        {
            arrow.DisableClicking();
        }
    }
    public void EnableClickingAllArrows()
    {
        foreach (var arrow in arrows)
        {
            arrow.EnableClicking();
        }
    }


    public void UpdateArrows(List<NearLocation> nearLocs)
    {
        int count = 0;
        foreach (Arrow arrow in arrows)
        {


            bool active = false; 
            enebledCache[count] = false;

            foreach (NearLocation nearLoc in nearLocs)
            {
                if (nearLoc.GetLocation().blocked == true) continue;

                if (arrow.GetDirection() == nearLoc.GetDirection())
                {
                    enebledCache[count] = true;
                    arrow.SetNextLocation(nearLoc.GetLocation());
                    arrow.Enable();
                    active = true;
                     
                    break;
                }
                
            }
            if (active == false&&arrow.enabled==true)
                arrow.DisableAnim();

            count++;
        }  
    }

    public void UpdateArrowsCache(List<NearLocation> nearLocs)
    {
        int count = 0;
        foreach (Arrow arrow in arrows)
        {
             
            enebledCache[count] = false;

            foreach (NearLocation nearLoc in nearLocs)
            {
                if (arrow.GetDirection() == nearLoc.GetDirection())
                {
                    arrow.SetNextLocation(nearLoc.GetLocation());
                    enebledCache[count] = true; 

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
             
            if (enebledCache[i])
            {
                arrows[i].Enable();
            }
            else
            {
                arrows[i].DisableAnim();
            }
        }
         
    }
}
