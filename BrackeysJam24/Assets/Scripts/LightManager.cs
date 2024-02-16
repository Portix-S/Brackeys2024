using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private GameObject lightSource;

    [SerializeField] private Light2D light;
    [SerializeField] private Light2D globalLight;


    public void ChangeLightParams(Color color, float intensity, float outerRadius, float fallOffStrength)
    {
        globalLight.intensity = 0.01f;
        lightSource.SetActive(true);
        light.color = color;
        light.intensity = intensity;
        light.pointLightOuterRadius = outerRadius;
        light.falloffIntensity = fallOffStrength;
    }

}
