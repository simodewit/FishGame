using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSounds : MonoBehaviour
{
    public AudioSource[] sounds;
    public float minimumTime;
    public float maximumTime;

    private float timer;

    public void Start()
    {
        timer = UnityEngine.Random.Range(minimumTime, maximumTime);
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = UnityEngine.Random.Range(minimumTime, maximumTime);

            int index = UnityEngine.Random.Range(0, sounds.Length);
            sounds[index].Play();
        }
    }
}
