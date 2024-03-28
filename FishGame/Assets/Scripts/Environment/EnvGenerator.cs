using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvGenerator : MonoBehaviour
{
    #region variables

    [Tooltip("The minimum seconds to wait before spawning another prefab"), Range(0, 500)]
    public float minTime = 30;
    [Tooltip("The maximum seconds to wait before spawning another prefab"), Range(0, 500)]
    public float maxTime = 120;
    [Tooltip("The table script")]
    public Table table;
    [Tooltip("All the states that the spawner shouldnt spawn")]
    public stateOfUI[] dontSpawnStates;

    [Tooltip("All the environment prefabs that can be randomily generated")]
    public IslandInfo[] prefabs;

    //privates
    private float timer;
    private bool canSpawn;

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

    #region can spawn check

    public void CheckSpawning()
    {
        foreach (var state in dontSpawnStates)
        {
            if (state == table.state)
            {
                canSpawn = false;
                return;
            }
        }

        canSpawn = true;
    }

    #endregion

    #region generator

    public void Generator()
    {
        if (!canSpawn)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            float time = UnityEngine.Random.Range(minTime, maxTime);
            timer = time;
        }
        else
        {
            return;
        }

        int prefabNr = UnityEngine.Random.Range(0, prefabs.Length);
        IslandInfo current = prefabs[prefabNr];
        float randomRotation = UnityEngine.Random.Range(current.minRotation, current.maxRotation);
        Vector3 rotation = new Vector3(0, randomRotation, 0);

        if (!current.bothSides)
        {
            int chance = UnityEngine.Random.Range(0, 2);
            float distance = UnityEngine.Random.Range(current.minDistance, current.maxDistance);

            if (chance == 0)
            {
                Vector3 place = new Vector3(distance, transform.position.y, transform.position.z);
                GenerateIsland(current.prefab, place, rotation);
            }
            else
            {
                Vector3 place = new Vector3(-distance, transform.position.y, transform.position.z);
                GenerateIsland(current.prefab, place, rotation);
            }
        }
        else
        {
            GenerateIsland(current.prefab, transform.position, rotation);
        }
    }

    public void GenerateIsland(GameObject island, Vector3 position, Vector3 rotation)
    {
        GameObject a = Instantiate(island, position, Quaternion.identity);

        if (a.transform.GetComponent<EnvPrefab>().islandTransform != null)
        {
            a.transform.GetComponent<EnvPrefab>().islandTransform.Rotate(rotation);
        }
    }

    #endregion
}

[Serializable]
public class IslandInfo
{
    [Tooltip("The prefab of the island")]
    public GameObject prefab;

    [Header("Rotate info")]
    [Tooltip("The minimum rotation that the prefab is instantiated with"), Range(0, 360)]
    public float minRotation = 0;
    [Tooltip("The maximum rotation that the prefab is instantiated with"), Range(0, 360)]
    public float maxRotation = 360;

    [Header("Distance info")]
    [Tooltip("Decides if the model should spawn in the middle")]
    public bool bothSides;
    [Tooltip("The minimum distance to have from the ship to the instantiated prefabs"), Range(15, 300)]
    public float minDistance = 30;
    [Tooltip("The maximum distance to have from the ship to the instantiated prefabs"), Range(15, 300)]
    public float maxDistance = 100;
}
