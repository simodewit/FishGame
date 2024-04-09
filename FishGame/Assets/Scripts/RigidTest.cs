using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidTest : MonoBehaviour
{
    public Rigidbody rb;
    bool was;
    bool kin;

    public void Start()
    {
        was = rb.useGravity;
        kin = rb.isKinematic;

        print(rb.useGravity);
        print(rb.isKinematic);
        print("  ");
    }

    public void Update()
    {
        if (was != rb.useGravity || kin != rb.isKinematic)
        {
            was = rb.useGravity;
            kin = rb.isKinematic;

            print(rb.useGravity);
            print(rb.isKinematic);
            print("  ");
        }
    }
}
