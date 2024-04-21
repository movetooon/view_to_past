using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewsBlock : Selectable, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform arrows;

    public override void Select(float distance = 0)
    {
        DisableOutline();
    }

    public override void EnableOutline()
    {
        arrows.gameObject.SetActive(true);
    }
    public override void DisableOutline()
    {
        arrows.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnableOutline();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisableOutline();
    }
}
