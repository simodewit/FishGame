using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Tooltip("The total hp of the boss")][Range(0,5000)]
    public int hp = 500;
    [Tooltip("The speed at wich the boss is moving away from the boat")]
    public float moveSpeed = .5f;
    [Tooltip("The minimum amount of seconds to start another attack combo")]
    public float minAttackTime = 10;
    [Tooltip("The maximum amount of seconds to start another attack combo")]
    public float maxAttackTime = 30;
    [Tooltip("The tag that the boat is using")]
    public string boatTag = "Boat";
    [Tooltip("The total distance the boss has to have traveled forward to escape")]
    public float maxDistance = 20;

    private Vector3 startPos;
    private float timer;
    private float speed;
    private bool isAttacking;

    public void Start()
    {
        startPos = transform.position;
        timer = Random.Range(minAttackTime, maxAttackTime);
        ChangeSpeed(moveSpeed);
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        float distance = Vector3.Distance(transform.position, startPos);

        if (distance > maxDistance)
        {
            //game over
        }

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
        else
        {

        }
    }

    public void Health(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            //game won
            Destroy(gameObject);
        }
    }

    public void ChangeSpeed(float nextSpeed)
    {
        speed = nextSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);

        if (collision.transform.tag == boatTag)
        {
            ChangeSpeed(moveSpeed);
        }
    }
}
