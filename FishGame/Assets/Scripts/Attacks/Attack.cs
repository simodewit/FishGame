using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/Attack", order = 1)]
public class Attack : ScriptableObject
{
    [Tooltip("The name of the attack")]
    public string animationName;
    [Tooltip("The animation of this attack")]
    public Animation animation;
    [Tooltip("If true this is the animation played when trying to escape")]
    public bool escapeAttack;
}
