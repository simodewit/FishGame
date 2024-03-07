using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [Tooltip("The minimum time before starting the water particles again")]
    public float minParticleTime;
    [Tooltip("The maximum time before starting the water particles again")]
    public float maxParticleTime;
    [Tooltip("All the water particles")]
    public ParticleSystem[] waterParticles;

    private float timer;

    public void Start()
    {
        timer = Random.Range(minParticleTime, maxParticleTime);
    }

    public void Update()
    {
        StartParticles();
    }

    public void StartParticles()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = Random.Range(minParticleTime, maxParticleTime);

            foreach (var particle in waterParticles)
            {
                particle.Play();
            }
        }
    }
}
