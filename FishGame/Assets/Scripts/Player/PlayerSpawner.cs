using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("The prefab of the player")]
    public GameObject playerPrefab;

    [Header("Code refrence")]
    public GameObject player;

    public void Awake()
    {
        player = Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
