using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvGenerator : MonoBehaviour
{
    [Tooltip("All the environment prefabs that can be randomily generated")]
    public GameObject[] prefabs;
    [Tooltip("The minimum seconds to wait before spawning another prefab")][Range(0, 500)]
    public float minTime = 30;
    [Tooltip("The maximum seconds to wait before spawning another prefab")][Range(0, 500)]
    public float maxTime = 120;
    [Tooltip("The minimum distance to have from the ship to the instantiated prefabs")][Range(15,300)]
    public float minDistance = 30;
    [Tooltip("The maximum distance to have from the ship to the instantiated prefabs")][Range(15, 300)]
    public float maxDistance = 100;

    private float timer;

    public void Start()
    {
        float time = Random.Range(minTime, maxTime);
        timer = time;
    }

    public void Update()
    {
        Generator();
    }

    public void Generator()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            float time = Random.Range(minTime, maxTime);
            timer = time;

            float distance = Random.Range(minDistance, maxDistance);

            int prefabNr = Random.Range(0, prefabs.Length);
            GameObject instPrefab = prefabs[prefabNr];

            int chance = Random.Range(0, 2);

            if (chance == 0)
            {
                Vector3 place = new Vector3(distance, transform.position.y, transform.position.z);
                Instantiate(instPrefab, place, Quaternion.identity);
            }
            else
            {
                Vector3 place = new Vector3(-distance, transform.position.y, transform.position.z);
                Instantiate(instPrefab, place, Quaternion.identity);
            }
        }
    }
}
