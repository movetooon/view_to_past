using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float damp = 0.5f;
    public Transform target;


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, damp);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, damp);
    }
}
