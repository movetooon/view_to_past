using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField] private Material branchesMat;
    [SerializeField] private float distortionMultiplier;
    [SerializeField] private Vector2 brancesMatSpeed; 
    [SerializeField] private Material skyMat;
    [SerializeField] private Terrain terrain;

    
    [SerializeField,Range(0,1)] float weatherBadness;

    public void Init()
    {
        skyMat = RenderSettings.skybox;
    }

    private void FixedUpdate()
    {
        terrain.terrainData.wavingGrassSpeed = weatherBadness+0.05f;
        terrain.terrainData.wavingGrassStrength = weatherBadness;
        terrain.terrainData.wavingGrassAmount = weatherBadness+0.05f;
        

        skyMat.SetFloat("_Exposure",(1.2f-weatherBadness));
        branchesMat.SetFloat("_distortionForce", weatherBadness *distortionMultiplier+0.01f);
        branchesMat.SetVector("_wobleSpeed", brancesMatSpeed*weatherBadness);
    }
}
