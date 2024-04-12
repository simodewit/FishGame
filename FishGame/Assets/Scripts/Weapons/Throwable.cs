using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum ThrowableState
{
    spawned,
    isHeld,
    isThrown,
    hasHit
}

public class Throwable : MonoBehaviour
{
    #region variables

    [Header("Refrences")]
    [Tooltip("The collider of this object")]
    public Collider col;
    [Tooltip("The rigidbody of this object")]
    public Rigidbody rb;
    [Tooltip("The particle")]
    public ParticleSystem particle;
    public bool useParticle;

    [Header("General data")]
    [Tooltip("The damage that the throwable does to the enemy")][Range(0, 100)]
    public int damage = 20;
    [Tooltip("The time that the collider of this throwable is dissabled after letting go of the object. you want this to be as low as possible without colliding with the player")][Range(0,3)]
    public float colliderTime = 1;
    [Tooltip("Decides if the object can directly damage the boss")]
    public bool canDamageBoss;
    [Tooltip("The sound when you pickup the throwable")]
    public AudioSource pickupSound;
    [Tooltip("The sound when you throw the throwable")]
    public AudioSource throwSound;

    [Header("Slow down data")]
    [Tooltip("Decides if the object can slow the boss down")]
    public bool canSlowDown;
    [Tooltip("The slowed speed multiplier")]
    public float speedModifier;
    [Tooltip("The time that the speed should be modified")]
    public float speedTime;

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
    private ThrowableState state;
    private float colliderTimer;
    private float stickTimer;
    private Boss boss;

    #endregion

    #region start and update

    public void Start()
    {
        rb.useGravity = false;
        colliderTimer = colliderTime;
        stickTimer = stickTime;
        col.enabled = true;
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
        state = ThrowableState.isHeld;

        col.enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;

        pickupSound.Play();
    }

    public void DropThrowable()
    {
        state = ThrowableState.isThrown;

        rb.isKinematic = false;
        rb.useGravity = true;

        throwSound.Play();
    }

    #endregion

    #region timer

    public void Timer()
    {
        if (state == ThrowableState.isThrown)
        {
            if (colliderTimer > 0)
            {
                colliderTimer -= Time.deltaTime;
            }
            else if(!col.enabled)
            {
                col.enabled = true;
            }
        }
    }

    #endregion

    #region direction

    public void FaceDirection()
    {
        if (hasFront && state == ThrowableState.isThrown)
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

    #region stick

    public void Collided()
    {
        if (state != ThrowableState.hasHit)
        {
            return;
        }

        if (!sticks)
        {
            if (canSlowDown && boss != null)
            {
                boss.slowHits += 1;
            }

            Destroy(gameObject);
        }

        if (!sticksInf)
        {
            stickTimer -= Time.deltaTime;

            if (stickTimer <= 0)
            {
                if (canSlowDown && boss != null)
                {
                    boss.slowHits += 1;
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

        state = ThrowableState.hasHit;

        BossDamage(collision);
        Sticks(collision);
        Explosion(collision);
    }

    public void BossDamage(Collision collision)
    {
        BossCollider bossCollider = collision.transform.GetComponent<BossCollider>();

        if (bossCollider != null)
        {
            boss = bossCollider.boss;

            if (canDamageBoss)
            {
                bossCollider.boss.Health(damage);
            }

            if (canSlowDown)
            {
                bossCollider.boss.ChangeSpeed(speedModifier, speedTime);
            }
        }
    }
    public void Sticks(Collision collision)
    {
        if (sticks)
        {
            transform.SetParent(collision.transform);

            col.enabled = false;
            rb.isKinematic = true;
        }
    }
    public void Explosion(Collision collision)
    {
        if (useParticle)
        {
            particle.Play();
            particle.transform.parent = null;
        }

        if (isExplosive)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange);

            foreach (Collider col in cols)
            {
                BossCollider spot = col.GetComponent<BossCollider>();

                if (spot != null)
                {
                    spot.Health(damage, isExplosive);
                }
            }
        }
        else
        {
            BossCollider spot = collision.transform.GetComponent<BossCollider>();

            if (spot != null)
            {
                spot.Health(damage, isExplosive);
            }
        }
    }

    #endregion
}
