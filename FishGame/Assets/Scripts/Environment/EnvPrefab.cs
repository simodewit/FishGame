using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvPrefab : MonoBehaviour
{
    [Tooltip("The speed at wich the prefab is going to move")][Range(1,100)]
    public float speed;

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(0,0,speed * Time.deltaTime);
    }
}
