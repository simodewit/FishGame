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
    [Tooltip("If this throwable has one side that should be in front this box should be checked")]
    public bool hasFront;
    [Tooltip("The speed thats needed to turn the object if hasFront is enabled")][Range(0, 20)]
    public float turnSpeed = 5;
    [Tooltip("The amount of distance the throwable has to travel before destroyed if missed")][Range(0,1000)]
    public float missedDistance = 100;
    [Tooltip("The seconds that the throwable has before destroyed after impact")][Range(0,60)]
    public float destroyTime = 10;
    [Tooltip("Decides wether the throwable stops on impact or not")]
    public bool stayOnImpact;

    private bool activateTimer;
    private bool isPickedUp;
    private bool hasHit;
    private float timer;
    private float timer2;

    #endregion

    #region start and update

    public void Start()
    {
        timer = time;
        rb.useGravity = false;
        timer2 = destroyTime;
    }

    public void Update()
    {
        Timer();
        FaceDirection();
        DoesntHit();
        HitSomething();
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
        if (hasFront)
        {
            if (isPickedUp)
            {
                transform.localRotation = new Quaternion(90, 0, 0, 0);
            }
            else
            {
                if (rb.velocity.magnitude >= turnSpeed)
                {
                    Vector3 lookTowards = transform.position + rb.velocity;
                    transform.LookAt(lookTowards);
                }
            }
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

    public void HitSomething()
    {
        if (hasHit)
        {
            timer2 -= Time.deltaTime;

            if (stayOnImpact)
            {
                transform.position = transform.position;
            }
            if (timer2 <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    #region extra code

    public void PickupThrowable()
    {
        col.enabled = false;
        isPickedUp = true;
    }

    public void DropThrowable()
    {
        activateTimer = true;
        isPickedUp = false;
        rb.useGravity = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        XRDirectInteractor interactor = collision.transform.GetComponent<XRDirectInteractor>();
        Boss boss = collision.transform.GetComponent<Boss>();

        if (boss != null)
        {
            boss.Health(damage);
            hasHit = true;
        }
        else if(interactor == null)
        {
            hasHit = true;
        }
    }

    #endregion
}
