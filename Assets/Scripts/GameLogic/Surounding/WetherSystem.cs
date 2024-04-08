using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField] private Material branchesMat;
    [SerializeField] private float distortionMultiplier;
    [SerializeField] private Vector2 branchesMatSpeed;
    [SerializeField] private float startGrassSpeed;
    [SerializeField] private Material skyMat;
    [SerializeField] private Terrain terrain;
    [SerializeField] Color fogNormal;
    [SerializeField] Color fogBad;

    
    [SerializeField,Range(0,1)] float weatherBadness;

    public void Init()
    {
        skyMat = RenderSettings.skybox;
    }

    private void OnValidate()
    {
        ChangeWeater(weatherBadness);
    }

    private void FixedUpdate()
    {
        ChangeWeater(weatherBadness);
    }

    public void ChangeWeater(float weatherBadness)
    {
        float grassLerp= startGrassSpeed * (1 - weatherBadness) + weatherBadness;

        terrain.terrainData.wavingGrassSpeed = grassLerp;
        terrain.terrainData.wavingGrassStrength = grassLerp;
        terrain.terrainData.wavingGrassAmount = grassLerp;


        skyMat.SetFloat("_Exposure", (1.2f - weatherBadness));
        branchesMat.SetFloat("_distortionForce", weatherBadness * distortionMultiplier + 0.1f);
        branchesMat.SetVector("_wobleSpeed", branchesMatSpeed * weatherBadness+Vector2.one/50f);

        RenderSettings.fogColor = Color.Lerp(fogNormal, fogBad, weatherBadness);
    }
}
