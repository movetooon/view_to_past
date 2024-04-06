using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCSoundData", menuName = "Sound")]
public class NPCSound : ScriptableObject
{
    [SerializeField] public AudioClip[] default_sounds; 
    [SerializeField] public AudioClip anger; 
    [SerializeField] public AudioClip consent; 
    [SerializeField] public AudioClip disagreement;
    [SerializeField] public AudioClip misunderstanding; 
    [SerializeField] public AudioClip surprise;

    public AudioClip GetSoundByName(string soundName)
    {
        FieldInfo field = GetType().GetField(soundName);
        if (field != null)
        {
            return (AudioClip)field.GetValue(this);
        }

        return null;
    }
}
