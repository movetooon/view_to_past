using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestCloudPos : MonoBehaviour
{
    [SerializeField] private DialogCloud cloud;
    [SerializeField] Vector3 newPos;
    [SerializeField] bool test;
    [SerializeField] NPC npc;

    private void Start()
    {
        test = false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying==false)
        {
            if (npc == null) npc = GetComponent<NPC>();
            if (cloud == null) cloud = FindObjectOfType<DialogCloud>();
            if (test == true)
            {
                cloud.gameObject.SetActive(true);
                 
                cloud.SetPositions(transform, transform.position + transform.rotation * newPos, npc.Height());
                npc.cloudPosition = newPos;
            }
            else
            {
               // cloud.gameObject.SetActive(false);

            }
        }
    }

#endif
}
