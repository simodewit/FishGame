using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Throwable : MonoBehaviour
{
    #region variables

    [Header("Refrences")]
    [Tooltip("The collider of this object")]
    public Collider col;
    [Tooltip("The rigidbody of this object")]
    public Rigidbody rb;

    [Header("General data")]
    [Tooltip("The damage that the throwable does to the enemy")][Range(0, 100)]
    public int damage = 20;
    [Tooltip("The time that the collider of this throwable is dissabled after letting go of the object. you want this to be as low as possible without colliding with the player")][Range(0,3)]
    public float colliderTime = 1;
    [Tooltip("The tag of the player")]
    public string playerTag;
    [Tooltip("Decides if the object can directly damage the boss")]
    public bool canDamageBoss;

    [Header("Slow down data")]
    [Tooltip("Decides if the object can slow the boss down")]
    public bool canSlowDown;
    [Tooltip("The slowed speed multiplier")]
    public float speedModifier;

    [Header("Explosive data")]
    [Tooltip("decides if this object is explosive")]
    public bool isExplosive;
    [Tooltip("The range of the explosion")]
    public float explosionRange;

    [Header("Throw data")]
    [Tooltip("If this throwable has one side that should be in front while thrown this box should be checked")]
    public bool hasFront;
    [Tooltip("The speed thats needed to turn the object if hasFront is enabled")][Range(0, 20)]
    public float turnSpeed = 3;
    [Tooltip("The amount of distance the throwable has to travel before destroyed if missed")][Range(0, 1000)]
    public float missedDistance = 100;

    [Header("Stick data")]
    [Tooltip("Decides if the throwable stays in place after impact")]
    public bool sticks;
    [Tooltip("Decides if the throwable stays in place for the rest of the game")]
    public bool sticksInf;
    [Tooltip("Decides for how long the throwable stays in place for")]
    public float stickTime;

    //private variables
    private bool activateTimer;
    private bool flies;
    private bool hasHit;
    private float colliderTimer;
    private float stickTimer;
    private Boss boss;

    #endregion

    #region start and update

    public void Start()
    {
        colliderTimer = colliderTime;
        stickTimer = stickTime;
    }

    public void Update()
    {
        Timer();
        FaceDirection();
        DoesntHit();
        Collided();
    }

    #endregion

    #region inputs

    public void PickupThrowable()
    {
        col.enabled = false;
    }

    public void DropThrowable()
    {
        flies = true;
        activateTimer = true;
        rb.useGravity = true;
    }

    #endregion

    #region timer

    public void Timer()
    {
        if (activateTimer)
        {
            colliderTimer -= Time.deltaTime;

            if (colliderTimer <= 0)
            {
                activateTimer = false;
                col.enabled = true;
            }
        }
    }

    #endregion

    #region direction

    public void FaceDirection()
    {
        if (hasFront && flies)
        {
            Vector3 lookTowards = transform.position + rb.velocity;
            transform.LookAt(lookTowards);
        }
    }

    #endregion

    #region missed

    public void DoesntHit()
    {
        float distance = Vector3.Distance(transform.position, Vector3.zero);

        if (distance >= missedDistance)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region hit

    public void Collided()
    {
        if (!hasHit)
        {
            return;
        }

        if (!sticks)
        {
            if (canSlowDown)
            {
                boss.speedModifier = boss.speed;
            }

            Destroy(gameObject);
        }

        if (!sticksInf)
        {
            stickTimer -= Time.deltaTime;

            if (stickTimer <= 0)
            {
                if (canSlowDown)
                {
                    boss.speedModifier = boss.speed;
                }

                Destroy(gameObject);
            }
        }
    }

    #endregion

    #region collision

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<XRGrabInteractable>() != null)
        {
            return;
        }

        flies = false;
        hasHit = true;

        if (sticks)
        {
            transform.SetParent(collision.transform);

            col.enabled = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        boss = collision.transform.GetComponent<Boss>();

        if (boss != null)
        {
            if (canDamageBoss)
            {
                boss.Health(damage);
            }

            if (canSlowDown)
            {
                boss.speedModifier = speedModifier;
            }
        }

        if (isExplosive)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange);

            foreach (Collider col in cols)
            {
                Weakspot spot = col.GetComponent<Weakspot>();

                if (spot != null)
                {
                    spot.Health(damage, isExplosive);
                }
            }
        }
        else
        {
            Weakspot spot = collision.transform.GetComponent<Weakspot>();

            if (spot != null)
            {
                spot.Health(damage, isExplosive);
            }
        }
    }

    #endregion
}
