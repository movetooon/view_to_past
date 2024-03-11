using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class View : MonoBehaviour
{
    [SerializeField] private bool checkVeiw;
    [SerializeField] private CameraInfo camInfo;
    private MeshRenderer mesh;

    private void OnValidate()
    {
        mesh= GetComponent<MeshRenderer>();
        camInfo=Camera.main.GetComponent<CameraInfo>();

        if (Application.isPlaying==false)
        {
            mesh.enabled = true;
        }
    }

    private void Start()
    {
        mesh.enabled = false;
    }

    private void Update()
    {
        if (Application.isPlaying == false)
        {
            if (checkVeiw)
            {
                Camera.main.transform.position = transform.position;
                Camera.main.transform.rotation = transform.rotation;
                camInfo.busy = true;
            }
            else if(camInfo.busy)
            {
                Camera.main.transform.position = camInfo.origPos;
                Camera.main.transform.rotation = camInfo.origRot;
                camInfo.busy = false;

            }
        }
    }
}
