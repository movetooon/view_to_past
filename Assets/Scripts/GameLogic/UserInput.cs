using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UserInput 
{
    public static bool busyMouse;

    public static Ray GetMouseRay()
    { 
        return UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public static RaycastHit GetMouseHitOnScreen()
    {
        RaycastHit hit;
        Physics.Raycast(GetMouseRay(), out hit);  

        return hit;

    }

    public static bool GetMouseClick()
    {
        return !EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0);
      
    }

}
