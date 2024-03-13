using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvGenerator : MonoBehaviour
{
    #region variables

    [Tooltip("All the environment prefabs that can be randomily generated")]
    public IslandInfo[] prefabs;
    [Tooltip("The minimum seconds to wait before spawning another prefab")][Range(0, 500)]
    public float minTime = 30;
    [Tooltip("The maximum seconds to wait before spawning another prefab")][Range(0, 500)]
    public float maxTime = 120;
    [Tooltip("The minimum distance to have from the ship to the instantiated prefabs")][Range(15,300)]
    public float minDistance = 30;
    [Tooltip("The maximum distance to have from the ship to the instantiated prefabs")][Range(15, 300)]
    public float maxDistance = 100;
    [Tooltip("The minimum rotation that the prefab is instantiated with")][Range(0, 360)]
    public float minRotation = 0;
    [Tooltip("The maximum rotation that the prefab is instantiated with")][Range(0, 360)]
    public float maxRotation = 360;

    //privates
    private float timer;

    #endregion

    #region start and update

    public void Start()
    {
        float time = UnityEngine.Random.Range(minTime, maxTime);
        timer = time;
    }

    public void Update()
    {
        Generator();
    }

    #endregion

    #region generator

    public void Generator()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            return;
        }

        float time = UnityEngine.Random.Range(minTime, maxTime);
        timer = time;

        float distance = UnityEngine.Random.Range(minDistance, maxDistance);

        int prefabNr = UnityEngine.Random.Range(0, prefabs.Length);
        GameObject instPrefab = prefabs[prefabNr].prefab;

        float randomRotation = UnityEngine.Random.Range(minRotation, maxRotation);
        Quaternion rotation = new Quaternion(0, randomRotation, 0, 0);

        if (!prefabs[prefabNr].shouldRotate)
        {
            rotation = Quaternion.identity;
        }

        if (prefabs[prefabNr].bothSides)
        {
            Instantiate(instPrefab, transform.position, rotation);

            return;
        }

        int chance = UnityEngine.Random.Range(0, 2);

        if (chance == 0)
        {
            Vector3 place = new Vector3(distance, transform.position.y, transform.position.z);
            Instantiate(instPrefab, place, rotation);
        }
        else
        {
            Vector3 place = new Vector3(-distance, transform.position.y, transform.position.z);
            Instantiate(instPrefab, place, rotation);
        }
    }

    #endregion
}

[Serializable]
public class IslandInfo
{
    [Tooltip("The prefab of the island")]
    public GameObject prefab;
    [Tooltip("Decides if the model should spawn in the middle")]
    public bool bothSides;
    [Tooltip("Should have different rotations every time it spawns")]
    public bool shouldRotate;
}