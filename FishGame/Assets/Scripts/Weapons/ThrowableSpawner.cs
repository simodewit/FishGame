using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowableSpawner : MonoBehaviour
{
    [Tooltip("Place where the spear should be instantiated")]
    public Transform spawnPlace;
    [Tooltip("The item to instantiate")]
    public GameObject item;
    [Tooltip("The distance that the item has to go before spawning a new one")]
    public float spawnDistance;
    [Tooltip("The time before spawning another one")]
    public float respawnTime;

    private GameObject currentItem;
    private float timer;

    public void Start()
    {
        CreateItem();

        timer = respawnTime;
    }

    public void Update()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        if (currentItem == null)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = respawnTime;

                CreateItem();
            }

            return;
        }

        float distance = Vector3.Distance(currentItem.transform.position, spawnPlace.position);

        if (distance >= spawnDistance)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = respawnTime;

                CreateItem();
            }
        }
    }

    public void CreateItem()
    {
        currentItem = Instantiate(item, spawnPlace.position, Quaternion.identity);
    }
}
