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
<<<<<<< Updated upstream
    [Tooltip("The seconds that the throwable has before destroyed after impact")][Range(0,60)]
    public float destroyTime = 10;
    [Tooltip("Decides wether the throwable stops on impact or not")]
    public bool stayOnImpact;

    private bool activateTimer;
    private bool isPickedUp;
    private bool hasHit;
    private float timer;
    private float timer2;
=======
    [Tooltip("The model of the throwable")]
    public GameObject model;

    private bool enableCollider;
    private float timer;
    private Vector3 startPos;
>>>>>>> Stashed changes

    #endregion

    #region start and update

    public void Start()
    {
        startPos = transform.position;
        timer = time;
<<<<<<< Updated upstream
        rb.useGravity = false;
        timer2 = destroyTime;
=======
>>>>>>> Stashed changes
    }

    public void Update()
    {
        EnableCollider();
        FaceDirection();
        DoesntHit();
        HitSomething();
    }

    #endregion

    #region timer

    public void EnableCollider()
    {
        if (enableCollider)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                enableCollider = false;
                col.enabled = true;
                timer = time;
            }
        }
    }

    #endregion

    #region direction code

    public void FaceDirection()
    {
        if (!hasFront)
        {
<<<<<<< Updated upstream
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
=======
            return;
        }

        if (!rb.useGravity)
        {
            return;
        }

        if (rb.velocity.magnitude >= turnSpeed)
        {
            Vector3 look = rb.velocity + transform.position;
            transform.LookAt(look);
>>>>>>> Stashed changes
        }
    }

    #endregion

    #region missed code

    public void DoesntHit()
    {
        float distance = Vector3.Distance(transform.position, startPos);

        if (distance >= missedDistance)
        {
            Destroy(gameObject);
        }
    }

    #endregion

<<<<<<< Updated upstream
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
=======
    #region inputs
>>>>>>> Stashed changes

    public void PickupThrowable()
    {
        col.enabled = false;
<<<<<<< Updated upstream
        isPickedUp = true;
=======
>>>>>>> Stashed changes
    }

    public void DropThrowable()
    {
<<<<<<< Updated upstream
        activateTimer = true;
        isPickedUp = false;
=======
        enableCollider = true;
>>>>>>> Stashed changes
        rb.useGravity = true;
    }

    #endregion

    #region collision

    public void OnCollisionEnter(Collision collision)
    {
        XRDirectInteractor interactor = collision.transform.GetComponent<XRDirectInteractor>();
        Boss boss = collision.transform.GetComponent<Boss>();

        if (boss != null)
        {
            boss.Health(damage);
<<<<<<< Updated upstream
            hasHit = true;
        }
        else if(interactor == null)
        {
            hasHit = true;
=======
            Destroy(gameObject);
>>>>>>> Stashed changes
        }
    }

    #endregion
}
