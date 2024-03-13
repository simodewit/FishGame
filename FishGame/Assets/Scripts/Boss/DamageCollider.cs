using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public LookAt colliderScript;
    public Boss boss;

    public void Update()
    {
        if (boss == null && colliderScript.boss != null)
        {
            boss = colliderScript.boss;
        }

        if (colliderScript.boss == null)
        {
            boss = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        boss.isHit = true;
    }

    private void OnTriggerExit(Collider other)
    {
        boss.isHit = false;
    }
}
