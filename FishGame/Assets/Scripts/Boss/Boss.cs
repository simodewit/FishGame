using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Tooltip("The total hp of the boss")][Range(0,1000)]
    public int hp = 500;

    public void Health(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            //game over
            Destroy(gameObject);
        }
    }
}
