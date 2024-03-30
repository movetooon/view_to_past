using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [SerializeField] private Material skyMaterial;

    [SerializeField] bool sinRotChange;
    [SerializeField] bool constantRotChange;

    [SerializeField] float sinAmplitude;
    [SerializeField] float sinFrequency;
    
    [SerializeField] float rotSpeed;

    private void FixedUpdate()
    {
        if (sinRotChange)
            SinRotaion(sinAmplitude, sinFrequency);
        
        if (constantRotChange)
            Rotaion(rotSpeed);

    }

    private void SinRotaion(float amplitude,float frequency)
    {
        skyMaterial.SetFloat("_Rotation", Mathf.Sin(Time.time*frequency) * amplitude);
    }

    private void Rotaion(float speed)
    {
        skyMaterial.SetFloat("_Rotation", Time.time*speed);
    }
}
