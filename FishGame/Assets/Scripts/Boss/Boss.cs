using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    #region variables

    [Header("General info")]
    [Tooltip("The total hp of the boss")][Range(0,5000)]
    public int hp = 500;
    [Tooltip("The max distance it can have to a point to decide if it is close enough to stop moving")]
    public float pointDistance;
    [Tooltip("The tag that the boat is using")]
    public string boatTag = "Boat";
    [Tooltip("The places where the boss can attack")]
    public Transform[] attackPlaces;
    [Tooltip("The navmesh agent of the boss")]
    public NavMeshAgent agent;
    [Tooltip("All the attacks the boss can do")]
    public Attack[] attacks;
    [Tooltip("All the run aways the boss can do")]
    public Attack[] runaway;
    [Tooltip("All the attack combo's the boss can do")]
    public AttackCombo[] attackCombos;
    [Tooltip("The normal swimming animation")]
    public Animation swimming;
    [Tooltip("The standard speed of the boss")]
    public float normalSpeed;
    [Tooltip("The speed multiplier of the boss")]
    public float speedModifier;

    [Header("Wait times")]
    [Tooltip("The minimum amount of seconds to start another attack combo")]
    public float minAttackTime = 10;
    [Tooltip("The maximum amount of seconds to start another attack combo")]
    public float maxAttackTime = 30;
    [Tooltip("The minimum amount of seconds before running away")]
    public float minRunTime = 30;
    [Tooltip("The maximum amount of seconds before running away")]
    public float maxRunTime = 60;
    [Tooltip("The minimum amount of seconds to wait before moving")]
    public float minWaitTime = 5;
    [Tooltip("The maximum amount of seconds to wait before moving")]
    public float maxWaitTime = 15;

    [Header("Code refrences")]
    public float speedTimer;

    //privates
    private float attackTimer;
    private float runTimer;
    private float pointTimer;
    private bool isAttacking;
    private bool canAttack;

    private Animation currentAnimation;
    private Transform nextPlaceToBe;
    private Queue<Attack> attackQueue;

    #endregion

    #region start and update

    public void Start()
    {
        attackTimer = Random.Range(minAttackTime, maxAttackTime);
        runTimer = Random.Range(minRunTime, maxRunTime);
    }

    public void Update()
    {
        DecideNextMove();
        Queue();
    }

    #endregion

    #region HP

    public void Health(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            //game won
        }
    }

    #endregion

    #region attack decider

    public void DecideNextMove()
    {
        runTimer -= Time.deltaTime;

        if (runTimer <= 0)
        {
            int index = Random.Range(0, runaway.Length);

            attackQueue.Enqueue(runaway[index]);
        }

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            int index = Random.Range(0, attacks.Length);

            attackQueue.Enqueue(attacks[index]);
        }
    }

    #endregion

    #region move

    public void Moving()
    {
        if (isAttacking)
        {
            return;
        }

        if (!currentAnimation.isPlaying)
        {
            currentAnimation = swimming;
            swimming.Play();
        }

        if (speedTimer <= 0)
        {
            speedTimer = 0;

            if (speedModifier != 1)
            {
                speedModifier = 1;
            }
        }
        else
        {
            speedTimer -= Time.deltaTime;
        }

        agent.speed = normalSpeed * speedModifier;

        float distance = Vector3.Distance(transform.position, nextPlaceToBe.position);

        if (distance <= pointDistance)
        {
            agent.isStopped = true;
            agent.ResetPath();
            canAttack = true;

            pointTimer -= Time.deltaTime;

            if (pointTimer <= 0)
            {
                int index = Random.Range(0, attackPlaces.Length);
                nextPlaceToBe = attackPlaces[index];
                agent.destination = nextPlaceToBe.position;

                pointTimer = Random.Range(minWaitTime, maxWaitTime);

                canAttack = false;
            }
        }
        else
        {
            agent.destination = nextPlaceToBe.position;
        }
    }

    #endregion

    #region queue

    public void Queue()
    {
        if (!canAttack)
        {
            return;
        }

        if (currentAnimation.isPlaying)
        {
            return;
        }

        if (attackQueue.Count > 0)
        {
            isAttacking = true;

            Attack attack = attackQueue.Dequeue();
            currentAnimation = attack.animation;
            //turn on animation;
        }
        else
        {
            isAttacking = false;
        }
    }

    #endregion
}
