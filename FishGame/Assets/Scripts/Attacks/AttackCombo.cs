using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCombo", menuName = "ScriptableObjects/AttackCombo", order = 1)]
public class AttackCombo : ScriptableObject
{
    public Attack[] attackCombo;
}