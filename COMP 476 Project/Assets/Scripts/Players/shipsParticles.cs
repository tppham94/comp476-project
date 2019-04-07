using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipsParticles : MonoBehaviour
{
    public ParticleSystem particleLauncher;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            particleLauncher.Emit(1);
        }
    }
}
