using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Tooltip("The total hp of the boss")][Range(0,1000)]
    public int hp = 500;
    [Tooltip("The speed at wich the boss is moving away from the boat")]
    public float moveSpeed;
    [Tooltip("The minimum amount of seconds to start another attack combo")]
    public float minAttackTime;
    [Tooltip("The maximum amount of seconds to start another attack combo")]
    public float maxAttackTime;
    [Tooltip("The tag that the boat is using")]
    public string boatTag;

    private float timer;
    public float speed;
    public bool isAttacking;

    public void Start()
    {
        timer = Random.Range(minAttackTime, maxAttackTime);
        speed = moveSpeed;
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!isAttacking)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = Random.Range(minAttackTime, maxAttackTime);

                isAttacking = true;
                //start attack
            }
        }
    }

    public void Health(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            //game over
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == boatTag)
        {
            speed = moveSpeed;
        }
    }
}
