using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ItemData",menuName ="Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName; 
    [SerializeField] private Monolog playerDescription;
    [SerializeField] private Sprite icon;

    public Monolog PlayerDescription() => playerDescription;
    public string Name() => itemName;
    public Sprite Icon() => icon;
     

}
