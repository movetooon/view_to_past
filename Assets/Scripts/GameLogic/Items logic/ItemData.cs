  
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ItemData",menuName ="Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName; 
    [SerializeField] private Monolog playerDescription;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite texture;
    [SerializeField] private soundType sound;

    public soundType GetSoundType() => sound;
    public Monolog GetPlayerDescription() => playerDescription;
    public string GetName() => itemName;
    public Sprite GetIcon() => icon;
    public Sprite GetTexture() => texture;
     

}

public enum soundType 
{
    defaultType=0,
    metal=1,
    wood=2,
    fabric=3,
    water=4,
    grass=5

}
