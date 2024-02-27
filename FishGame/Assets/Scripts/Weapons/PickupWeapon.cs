using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickupWeapon : MonoBehaviour
{
    [Tooltip("The object it has to instantiate")]
    public GameObject objectToInst;

    //private GameObject colObject;

    //public void OnTriggerEnter(Collider other)
    //{
    //    XRGrabInteractable grab = other.transform.GetComponent<XRGrabInteractable>();

    //    if (grab != null)
    //    {
    //        colObject = other.gameObject;
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    colObject = null;
    //}

    public void InstObject(ActivateEventArgs args)
    {
        Vector3 a = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        GameObject obj = Instantiate(objectToInst, a, Quaternion.identity);
    }
}
