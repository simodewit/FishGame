using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Throwable : MonoBehaviour
{
    #region variables

    [Tooltip("The damage that the throwable does to the enemy")][Range(0, 100)]
    public int damage = 20;
    [Tooltip("The collider of this object")]
    public Collider col;
    [Tooltip("The rigidbody of this object")]
    public Rigidbody rb;
    [Tooltip("The time that the collider of this throwable is dissabled after letting go of the object. you want this to be as low as possible without colliding with the player")][Range(0,3)]
    public float time = 1;
    [Tooltip("If this throwable has one side that should be in front while thrown this box should be checked")]
    public bool hasFront;
    [Tooltip("The speed thats needed to turn the object if hasFront is enabled")][Range(0, 20)]
    public float turnSpeed = 3;
    [Tooltip("The amount of distance the throwable has to travel before destroyed if missed")][Range(0,1000)]
    public float missedDistance = 100;

    private bool activateTimer;
    private float timer;
    private bool hasThrown;

    #endregion

    #region start and update

    public void Start()
    {
        timer = time;
    }

    public void Update()
    {
        Timer();
        FaceDirection();
        DoesntHit();
    }

    #endregion

    #region timer

    public void Timer()
    {
        if (activateTimer)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                activateTimer = false;
                col.enabled = true;
                timer = time;
            }
        }
    }

    #endregion

    #region direction code

    public void FaceDirection()
    {
        if (hasFront && hasThrown)
        {
            Vector3 lookTowards = transform.position + rb.velocity;
            transform.LookAt(lookTowards);
        }
    }

    #endregion

    #region missed code

    public void DoesntHit()
    {
        float distance = Vector3.Distance(transform.position, Vector3.zero);

        if (distance >= missedDistance)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region inputs

    public void PickupThrowable()
    {
        col.enabled = false;
    }

    public void DropThrowable()
    {
        hasThrown = true;
        activateTimer = true;
        rb.useGravity = true;
    }

    #endregion

    #region collision

    public void OnCollisionEnter(Collision collision)
    {
        Boss boss = collision.transform.GetComponent<Boss>();

        if (boss != null)
        {
            boss.Health(damage);
            Destroy(gameObject);
        }
    }

    #endregion
}
