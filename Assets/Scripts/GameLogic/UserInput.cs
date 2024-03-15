 using UnityEngine;
using UnityEngine.EventSystems;

public static class UserInput 
{
    public static bool busyMouse;

    public static Ray GetMouseRay() => Camera.main.ScreenPointToRay(Input.mousePosition);


    public static RaycastHit GetMouseHitOnScreen()
    {
        RaycastHit hit;
        Physics.Raycast(GetMouseRay(), out hit);  

        return hit; 
    }

    public static bool GetMouseClick()
    {
        return !EventSystem.current.IsPointerOverGameObject() 
            && Input.GetMouseButtonDown(0); 
    }

}
