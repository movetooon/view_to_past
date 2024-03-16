using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogCloud : MonoBehaviour
{
    [SerializeField] Transform[] smallClouds;  
    [SerializeField] float directionMultiplier;
 

    public void SetSmallCloudTransforms(Vector3 npcPosition,float height)
    {
        for(int i = 0; i < smallClouds.Length; i++)
        {
            Vector3 npcHeadPosition = new Vector3(
                npcPosition.x,
                npcPosition.y+height/2,
                npcPosition.z);

            Vector3 newCloudPosition = npcHeadPosition -
                (npcHeadPosition - transform.position) *
                directionMultiplier * ((float)(i+1)/smallClouds.Length );
 

            smallClouds[i].position = newCloudPosition;
        }
    }
}
