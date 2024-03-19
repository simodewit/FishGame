using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public ColliderSystem colliderScript;
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

    private void OnTriggerStay(Collider other)
    {
        boss.HasCollided();
    }

    private void OnTriggerExit(Collider other)
    {
        boss.StoppedColliding();
    }
}
