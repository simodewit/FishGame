using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderSystem : MonoBehaviour
{
    [Tooltip("The place that it has to look towards")]
    public Transform placeToLook;
    [Tooltip("The speed at wich the system turns towards the boss")]
    public float rotateSpeed;

    [Header("All the colliders")]
    public GameObject colliderHor;
    public GameObject colliderVerRight;
    public GameObject colliderVerLeft;
    public GameObject colliderDiaRight;
    public GameObject colliderDiaLeft;

    public Boss boss;

    public void Update()
    {
        LookTowards();
    }

    public void LookTowards()
    {
        if (placeToLook == null)
        {
            return;
        }

        transform.LookAt(placeToLook);

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
