using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    public void Start()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
