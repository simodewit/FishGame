using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [Tooltip("The place that it has to look towards")]
    public Transform placeToLook;

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
        transform.LookAt(placeToLook);
    }
}
