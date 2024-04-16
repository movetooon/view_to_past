using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[RequireComponent(typeof(Animator))]
public class DialogCloud : MonoBehaviour
{
    [SerializeField] Transform[] smallClouds;  
    [SerializeField] Transform  bigCloud;  
    [SerializeField] float directionMultiplier;
    public Animator anim { get; private set; }

    public void Init()
    {
        anim=GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void SetPositions(Transform npcTransform,Vector3 newPosition, float height)
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
    }
}
