using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardInteractable : MonoBehaviour
{
    [Tooltip("The things that should be turned off. Add the model of the interactable to this list")]
    public GameObject[] turnOffThese;
    [Tooltip("The things that should be turned on")]
    public GameObject[] turnOnThese;

    public void StartInteraction()
    {
        foreach (var turn in turnOffThese)
        {
            turn.SetActive(false);
        }

        foreach (var turn in turnOnThese)
        {
            turn.SetActive(true);
        }
    }
}
