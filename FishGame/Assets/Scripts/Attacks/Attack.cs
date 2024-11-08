using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum attackSort
{
    horizontal,
    vertical,
    diagonal,
    escape
}

public enum attackPlace
{
    left,
    right,
    none
}

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/Attack", order = 1)]
public class Attack : ScriptableObject
{
    [Tooltip("The index of the attack in the animator")]
    public string triggerIndex;
    [Tooltip("Decides what kind of attack this is")]
    public attackSort attackSort;
    [Tooltip("Decides if the attack is left or right sided or both. only works with vertical and diagonal attacks")]
    public attackPlace side;
    [Tooltip("Length of the attack")]
    public float length;
}
