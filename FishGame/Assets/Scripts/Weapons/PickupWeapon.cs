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
    [Tooltip("The rotation of the item when spawned in")]
    public Quaternion spawnRotation;

    private GameObject currentItem;

    public void Start()
    {
<<<<<<< Updated upstream
        CreateItem();
=======
        Spawn();
>>>>>>> Stashed changes
    }

    public void Update()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        float distance = Vector3.Distance(currentItem.transform.position, spawnPlace.position);

        if (distance >= spawnDistance)
        {
<<<<<<< Updated upstream
            CreateItem();
        }
    }

    public void CreateItem()
    {
        currentItem = Instantiate(item, spawnPlace.position, Quaternion.identity);
        currentItem.GetComponent<Rigidbody>().useGravity = false;
=======
            Spawn();
        }
    }

    public void Spawn()
    {
        currentItem = Instantiate(item, spawnPlace.position, spawnRotation);
>>>>>>> Stashed changes
    }
}
