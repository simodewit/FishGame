using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickupWeapon : MonoBehaviour
{
    [Tooltip("Place where the spear should be instantiated")]
    public Transform spawnPlace;
    [Tooltip("The item to instantiate")]
    public GameObject item;
    [Tooltip("The distance that the item has to go before spawning a new one")]
    public float spawnDistance;

    private GameObject currentItem;

    public void Start()
    {
        currentItem = Instantiate(item, spawnPlace.position, Quaternion.identity);
    }

    public void Update()
    {
        float distance = Vector3.Distance(currentItem.transform.position, spawnPlace.position);

        if (distance >= spawnDistance)
        {
            currentItem = Instantiate(item, spawnPlace.position, Quaternion.identity);
        }
    }
}
