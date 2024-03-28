using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningEmpty : MonoBehaviour
{
    public Boss boss;

    private Vector3 placeToLook;

    public void Update()
    {
        Turning();
    }

    public void Turning()
    {
        if (placeToLook == Vector3.zero)
        {
            if (boss.colliderScript != null)
            {
                placeToLook = boss.colliderScript.transform.position;
            }

            return;
        }

        transform.LookAt(placeToLook);

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
