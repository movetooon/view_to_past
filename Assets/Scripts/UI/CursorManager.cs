using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class CursorManager : MonoBehaviour
{
    [SerializeField] private Sprite texture;
    [SerializeField] private Vector3 hotSpot;
    private Image cursorDisplay;
    private RectTransform cursorTransform;


    private void Start()
    {
        cursorDisplay = GetComponent<Image>(); 
        Cursor.lockState = CursorLockMode.None;
    
        cursorTransform = GetComponent<RectTransform>();
        cursorDisplay.sprite = texture;
    }


    private void Update()
    {
        transform.position = Input.mousePosition+(Vector3)cursorTransform.sizeDelta/2;
    }
}
