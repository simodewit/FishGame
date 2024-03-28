using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    moving,
    notMoving,
    turning,
    canAttack,
    attackingHor,
    attackingVer,
    attackingDia,
    tryEscaping,
    escaping,
    turningBack
}

public class Boss : MonoBehaviour
{
    //main functions
    #region variables

    [Header("Refrences")]
    [Tooltip("The navmesh agent of the boss")]
    public NavMeshAgent agent;
    [Tooltip("The empty that moves towards the player at all times")]
    public Transform rotatingObject;

    [Header("General info")]
    [Tooltip("The total hp of the boss")][Range(0,5000)]
    public int hp = 500;
    [Tooltip("The max distance it can have to a point to decide if it is close enough to stop moving")]
    public float pointDistance;
    [Tooltip("The normal swimming animation")]
    public Animation swimming;
    [Tooltip("The standard speed of the boss")]
    public float normalSpeed;
    [Tooltip("The speed multiplier of the boss")]
    public float speedModifier;

    [Header("Attack data")]
    [Tooltip("The places where the boss can attack")]
    public Transform[] attackPlaces;
    [Tooltip("All the attacks the boss can do")]
    public Attack[] attacks;
    [Tooltip("All the run aways the boss can do")]
    public Attack[] escapeAttacks;
    [Tooltip("Has to hit this many slow objects to keep boss here")]
    public int MinSlowHits;
    [Tooltip("The time that the boss waits before deciding to leave or stay")]
    public float slowHitsTime;
    [Tooltip("The speed at wich the boss rotates towards a position where it is able to attack")]
    public float rotateSpeed;
    [Tooltip("The total angle needed before snapping towards the end rotation")]
    public float rotateSnapDistance;

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
    [Tooltip("The seconds you have to wait before a attack damages you")]
    public float attackAvoidTime;

    [Header("Code refrences dont change")]
    [Tooltip("The speed of the boss")]
    public float speed;
    [Tooltip("The total hits from slow items")]
    public int slowHits;
    [Tooltip("The table script")]
    public Table table;
    [Tooltip("the place to go when escaping")]
    public Transform escapePlace;
    [Tooltip("The boat collider script")]
    public ColliderSystem colliderScript;

    //privates
    private GameObject horCollider;
    private GameObject leftVerCollider;
    private GameObject rightVerCollider;
    private GameObject leftDiaCollider;
    private GameObject rightDiaCollider;

    private float attackTimer;
    private float attackAvoidTimer;
    private float runTimer;
    private float pointTimer;
    private float escapeTimer;

    private int currentLocation;
    private bool isHit;

    private BossState state;

    private Attack currentAttack;
    private Transform nextPlaceToBe;
    private Queue<Attack> attackQueue = new Queue<Attack>();

    #endregion

    #region start and update

    public void Start()
    {
        Timers();
        GetColliders();

        int index = Random.Range(0, attackPlaces.Length);
        nextPlaceToBe = attackPlaces[index];
        speed = speedModifier;
    }

    public void Timers()
    {
        attackTimer = Random.Range(minAttackTime, maxAttackTime);
        runTimer = Random.Range(minRunTime, maxRunTime);
        pointTimer = Random.Range(minWaitTime, maxWaitTime);
        attackAvoidTimer = attackAvoidTime;
        escapeTimer = slowHitsTime;
    }

    public void GetColliders()
    {
        horCollider = colliderScript.colliderHor;
        leftDiaCollider = colliderScript.colliderDiaLeft;
        rightDiaCollider = colliderScript.colliderDiaRight;
        leftVerCollider = colliderScript.colliderVerLeft;
        rightVerCollider = colliderScript.colliderVerRight;
    }

    public void Update()
    {
        DecideNextMove();
        Queue();
        Turning();
        Attack();
        Escaping();
        SpeedModifier();
        Moving();
    }

    #endregion

    //support functions
    #region HP

    public void Health(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            table.state = stateOfUI.inUi;
            Destroy(gameObject);
        }
    }

    #endregion

    #region reset navmesh
    public void ResetNavmesh()
    {
        agent.transform.position = nextPlaceToBe.position;
        agent.isStopped = true;
        agent.ResetPath();
    }

    #endregion

    #region speed

    public void SpeedModifier()
    {
        agent.speed = normalSpeed * speedModifier;
    }

    #endregion

    #region collision check of damage system

    public void HasCollided()
    {
        isHit = true;
    }

    public void StoppedColliding()
    {
        isHit = false;
    }

    #endregion

    #region animations

    public void Animations()
    {
        
    }

    #endregion

    //main functions
    #region attack decider

    public void DecideNextMove()
    {
        runTimer -= Time.deltaTime;

        if (runTimer <= 0)
        {
            runTimer = Random.Range(minRunTime, maxRunTime);

            int index = Random.Range(0, escapeAttacks.Length);
            attackQueue.Enqueue(escapeAttacks[index]);
        }

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            attackTimer = Random.Range(minAttackTime, maxAttackTime);

            int index = Random.Range(0, attacks.Length);
            attackQueue.Enqueue(attacks[index]);
        }
    }

    #endregion

    #region turning

    public void Turning()
    {
        if (state == BossState.notMoving && attackQueue.Count != 0)
        {
            state = BossState.turning;
        }

        if (state == BossState.turning)
        {
            Quaternion rotateTo = rotatingObject.transform.rotation;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, rotatingObject.transform.rotation) <= rotateSnapDistance)
            {
                transform.rotation = rotatingObject.transform.rotation;

                state = BossState.canAttack;
            }
        }

        if (state == BossState.turningBack)
        {
            Quaternion rotateTo = Quaternion.identity;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, Quaternion.identity) <= rotateSnapDistance)
            {
                transform.rotation = Quaternion.identity;

                state = BossState.notMoving;
            }
        }
    }

    #endregion

    #region queue

    public void Queue()
    {
        if (state != BossState.canAttack)
        {
            return;
        }

        if (attackQueue.Count > 0)
        {
            currentAttack = attackQueue.Dequeue();

            if (currentAttack.attackSort == attackSort.escape)
            {
                state = BossState.tryEscaping;
            }
            else if (currentAttack.attackSort == attackSort.horizontal)
            {
                state = BossState.attackingHor;
            }
            else if (currentAttack.attackSort == attackSort.vertical)
            {
                state = BossState.attackingVer;
            }
            else if (currentAttack.attackSort == attackSort.diagonal)
            {
                state = BossState.attackingDia;
            }
        }
    }

    #endregion

    #region attack

    public void Attack()
    {
        if (state == BossState.attackingHor)
        {
            CanDamage(horCollider);
        }
        else if (state == BossState.attackingVer)
        {
            if (currentAttack.side == attackPlace.left)
            {
                CanDamage(leftVerCollider);
            }
            if(currentAttack.side == attackPlace.right)
            {
                CanDamage(rightVerCollider);
            }
        }
        else if (state == BossState.attackingDia)
        {
            if (currentAttack.side == attackPlace.left)
            {
                CanDamage(leftDiaCollider);
            }
            if (currentAttack.side == attackPlace.right)
            {
                CanDamage(rightDiaCollider);
            }
        }
    }

    #endregion

    #region attack damage

    public void CanDamage(GameObject colliderToTurnOn)
    {
        colliderToTurnOn.SetActive(true);
        attackAvoidTimer -= Time.deltaTime;

        if (attackAvoidTimer <= 0)
        {
            if (isHit)
            {
                table.state = stateOfUI.inUi;
                Destroy(gameObject);
            }

            colliderToTurnOn.SetActive(false);
            attackAvoidTimer = attackAvoidTime;
            state = BossState.turningBack;
        }
    }

    #endregion

    #region escaping

    public void Escaping()
    {
        if (state == BossState.tryEscaping)
        {
            escapeTimer -= Time.deltaTime;

            if (escapeTimer <= 0)
            {
                escapeTimer = slowHitsTime;

                if (slowHits <= MinSlowHits)
                {
                    state = BossState.escaping;
                }
                else
                {
                    state = BossState.notMoving;
                }
            }
        }
        else
        {
            slowHits = 0;
        }

        if (state == BossState.escaping)
        {
            agent.destination = escapePlace.position;

            float d = Vector3.Distance(agent.destination, escapePlace.position);

            if (d <= pointDistance)
            {
                table.state = stateOfUI.inUi;
                Destroy(gameObject);
            }
        }
    }

    #endregion

    #region move

    public void Moving()
    {
        transform.position = agent.transform.position;

        if (state != BossState.notMoving && state != BossState.moving)
        {
            if (!agent.isStopped)
            {
                ResetNavmesh();
            }

            return;
        }

        float distance = Vector3.Distance(transform.position, nextPlaceToBe.position);

        if (distance <= pointDistance)
        {
            if (state == BossState.moving)
            {
                state = BossState.notMoving;
            }

            if (!agent.isStopped)
            {
                ResetNavmesh();
            }

            pointTimer -= Time.deltaTime;

            if (pointTimer <= 0)
            {
                int index = Random.Range(0, attackPlaces.Length);

                if (index == currentLocation)
                {
                    return;
                }

                nextPlaceToBe = attackPlaces[index];
                currentLocation = index;

                agent.destination = nextPlaceToBe.position;
                pointTimer = Random.Range(minWaitTime, maxWaitTime);
            }
        }
        else
        {
            agent.destination = nextPlaceToBe.position;

            if (state != BossState.moving)
            {
                state = BossState.moving;
            }
        }
    }

    #endregion
}
