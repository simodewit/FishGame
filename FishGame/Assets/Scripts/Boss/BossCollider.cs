using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    #region variables

    [Tooltip("The gameObject of the boss")]
    public Boss boss;
    [Tooltip("Decides if this is a weakspot or boss collider directly")]
    public bool isWeakspot;
    [Tooltip("The extra amount of damage the boss gets if the weakspot is hit")]
    public float damageMultiplier;
    [Tooltip("The damage that the throwables have to do before revealing the weakspot")]
    public int hp;
    [Tooltip("The model and hp for all the steps of damage it can take")]
    public Info[] layers;

    #endregion

    #region health

    public void Health(int damage, bool isExplosive)
    {
        if (isExplosive)
        {
            hp -= damage;

            foreach (var layer in layers)
            {
                if (layer.hp >= hp && !layer.hasLessHP)
                {
                    layer.model.SetActive(true);
                    layer.hasLessHP = true;
                    return;
                }

                if (layer.hp >= hp && layer.hasLessHP && layer.model != null)
                {
                    Destroy(layer.model);
                }
            }
        }

        if (hp <= 0)
        {
            hp = 0;

            if (!isExplosive)
            {
                int damageToDo = (int)(damage * damageMultiplier);
                boss.Health(damageToDo);
            }
        }
    }

    #endregion
}

#region info class

[Serializable]
public class Info
{
    [Header("Data")]
    [Tooltip("The model of the object")]
    public GameObject model;
    [Tooltip("The hp it has to have reached before this model shows")]
    public float hp;

    [Header("Code refrence")]
    [Tooltip("This is a code refrence keep it turned off unless you know why it should be on")]
    public bool hasLessHP;
}

#endregion
