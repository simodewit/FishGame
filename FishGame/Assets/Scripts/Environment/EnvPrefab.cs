using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvPrefab : MonoBehaviour
{
    [Tooltip("The speed at wich the prefab is going to move")][Range(1,100)]
    public float speed;
    [Tooltip("The total distance before the prefab is deleted")][Range(1,300)]
    public float maxDistance;

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
        transform.Translate(0,0,speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, startPos);
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
