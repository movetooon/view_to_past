using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public List<AudioClip> levelMusic;
    private int currentMusicIndex;

    [SerializeField] float loudness=0.1f;

    AudioSource musicPlayer;
     
    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }


    public void SetLoudness(float loudness)
    {
        this.loudness = loudness;
        //musicPlayer.volume = loudness;
    }
    public void StartTransition(int musicIndex)
    {

        if (currentMusicIndex == musicIndex) return;
        StartCoroutine(ChangeMusic(musicIndex));
    }


    public IEnumerator ChangeMusic(int musicIndex)
    { 
         currentMusicIndex = musicIndex;
        
        while (musicPlayer.volume != 0)
        {
            musicPlayer.volume -= 0.001f;
            yield return null;
        }

        musicPlayer.clip = levelMusic[musicIndex];
        musicPlayer.Play();

        while (musicPlayer.volume <= loudness)
        {
            musicPlayer.volume += 0.001f;
            yield return null;
        }

    }
}
