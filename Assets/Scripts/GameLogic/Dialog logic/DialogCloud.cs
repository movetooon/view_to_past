using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[RequireComponent(typeof(Animator))]
public class DialogCloud : MonoBehaviour
{
    [SerializeField] Transform[] smallClouds;  
    SpriteRenderer[] smallCloudsRender;  
     
    [SerializeField] Transform  bigCloud;
    SpriteRenderer bigCloudRender;
    [SerializeField] float directionMultiplier;

    [SerializeField] ParticleSystem[] emotionEffects; 
    [SerializeField] Color [] emotionColors;
   

    public Animator anim { get; private set; }

    public void Init()
    {
        anim=GetComponent<Animator>();
        gameObject.SetActive(false);
        
        smallCloudsRender=new SpriteRenderer[smallClouds.Length];
        for(int i = 0; i < smallClouds.Length; i++)
        {
          smallCloudsRender[i] = smallClouds[i].GetComponent<SpriteRenderer>();
        }
        bigCloudRender=bigCloud.GetComponent<SpriteRenderer>();
    }

    public void SetPositions(Transform npcTransform,Vector3 newPosition, float height,Emotion emotion=Emotion.None)
    {
        transform.position = newPosition;
        transform.rotation = npcTransform.rotation;
        bigCloud.LookAt(Camera.main.transform);

        for(int i = 0; i < smallClouds.Length; i++)
        {
            Vector3 npcHeadPosition = new Vector3(
                npcTransform.position.x,
                npcTransform.position.y+height/2,
                npcTransform.position.z);

            Vector3 newCloudPosition = npcHeadPosition -
                (npcHeadPosition - transform.position) *
                directionMultiplier * ((float)(i+1)/smallClouds.Length );
 
           
            smallClouds[i].position = newCloudPosition;
        }

        SetEmotion(emotion);
    }

    public void SetEmotion(Emotion emotion)
    {
        foreach (SpriteRenderer small in smallCloudsRender)
        {
            small.color = emotionColors[(int)emotion];
        }
        bigCloudRender.color = emotionColors[(int)emotion];

        for(int i = 0; i < emotionEffects.Length; i++)
        {
            if (i != (int)emotion)
            {
                emotionEffects[i].gameObject.SetActive(false);
            }
            else
            {
                emotionEffects[i].gameObject.SetActive(true);
            }
        }

    }

    public enum Emotion 
    { 
        None=0,
        Joy = 1,
        Sad =2,
        Angry=3,
        
    }

}
