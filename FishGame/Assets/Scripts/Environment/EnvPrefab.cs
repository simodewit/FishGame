using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvPrefab : MonoBehaviour
{
    [Tooltip("The speed at wich the prefab is going to move")][Range(1,100)]
    public float speed = 5;
    [Tooltip("The total distance before the prefab is deleted")][Range(1,1000)]
    public float maxDistance = 300;

    private Vector3 startPos;

    public void Start()
    {
        startPos = transform.position;
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(-transform.forward * speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, startPos);
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
