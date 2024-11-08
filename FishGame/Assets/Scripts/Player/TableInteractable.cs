using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteractable : MonoBehaviour
{
    [Tooltip("The script placed on the table")]
    public Table table;
    [Tooltip("The state that the ui has to go in after interacting with this interactable")]
    public stateOfUI state;
    [Tooltip("The normal material of this object")]
    public Material baseMaterial;
    [Tooltip("The material if the object is selected")]
    public Material selectedMaterial;

    private Renderer ren;

    public void Start()
    {
        ren = GetComponent<Renderer>();
    }

    public void IfHoveredEntered()
    {
        ren.material = selectedMaterial;
    }

    public void IfHoveredExit()
    {
        ren.material = baseMaterial;
    }

    public void TriggerInteractable()
    {
        table.state = state;
    }
}
