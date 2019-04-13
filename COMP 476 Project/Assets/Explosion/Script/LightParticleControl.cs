using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticleControl : MonoBehaviour
{
    public Light light;
    float time = 0;
    public float delay = 0.5f;
    public float down = 1;

    void Update()
    {
        time += Time.deltaTime;

        if (time > delay)
        {
            if (light.intensity > 0)
                light.intensity -= Time.deltaTime * down;

            if (light.intensity <= 0)
                light.intensity = 0;
        }
    }
}
