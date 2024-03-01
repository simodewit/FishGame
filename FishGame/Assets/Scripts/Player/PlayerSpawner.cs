using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("The prefab of the player")]
    public GameObject playerPrefab;

    //[Header("Code refrence keep empty")]
    //public GameObject player;

    public void Awake()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
