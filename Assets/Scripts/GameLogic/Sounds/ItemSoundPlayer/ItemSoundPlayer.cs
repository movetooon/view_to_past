using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemSoundPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> soundsTypes;
    public AudioSource audioPlayer;

    public void PlaySound(ItemData data)
    {
        audioPlayer.clip = soundsTypes[((int)data.GetSoundType())];
        audioPlayer.Play();
    }
}


