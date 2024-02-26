using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [Tooltip("The damage that the throwable does to the enemy")][Range(0, 100)]
    public int damage = 20;
    [Tooltip("The speed at wich the throwable is traveling")][Range(0, 100)]
    public int speed;

    public void OnCollisionEnter(Collision collision)
    {
        Boss boss = collision.transform.GetComponent<Boss>();

        if (boss != null)
        {
            boss.Health(damage);
            Destroy(gameObject);
        }
    }
}
