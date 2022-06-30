using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLightSc : MonoBehaviour
{
    [SerializeField] AnimationCurve LightIntensity;
    float currentTime, totalTime;
    new Light light;
    void Start()
    {
        light = GetComponent<Light>();
        totalTime = LightIntensity.keys[LightIntensity.keys.Length - 1].time;
    }

    void Update()
    {
        light.intensity = LightIntensity.Evaluate(currentTime);
        currentTime += Time.deltaTime;
        if (currentTime >= totalTime)
          currentTime = 0;

    }
}
